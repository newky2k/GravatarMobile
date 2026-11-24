using System;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;

namespace GravatarMobile.Maui
{
    /// <summary>
    /// A .NET MAUI view that shows the Gravatar image for an email address, either square or round, with an optional border.
    /// </summary>
    public class GravatarView : ContentView
    {
        private const int kDefaultSize = 80;

        private readonly Border mBorder;
        private readonly Image mImage;
        private int mRequestedSize;
        private int mLoadVersion;

        /// <summary>The Gravatar to show.</summary>
        public static readonly BindableProperty AvatarProperty = BindableProperty.Create(
            nameof(Avatar), typeof(Gravatar), typeof(GravatarView), null,
            propertyChanged: (b, o, n) => ((GravatarView)b).OnAvatarChanged());

        /// <summary>The email address to show the Gravatar for. Setting it replaces <see cref="Avatar"/>.</summary>
        public static readonly BindableProperty EmailProperty = BindableProperty.Create(
            nameof(Email), typeof(string), typeof(GravatarView), null,
            propertyChanged: (b, o, n) => ((GravatarView)b).OnEmailChanged((string)n));

        /// <summary>Square or round.</summary>
        public static readonly BindableProperty ViewStyleProperty = BindableProperty.Create(
            nameof(ViewStyle), typeof(GravatarViewStyle), typeof(GravatarView), GravatarViewStyle.Square,
            propertyChanged: (b, o, n) => ((GravatarView)b).UpdateShape());

        /// <summary>The border width.</summary>
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
            nameof(BorderWidth), typeof(double), typeof(GravatarView), 0d,
            propertyChanged: (b, o, n) => ((GravatarView)b).UpdateShape());

        /// <summary>The border colour.</summary>
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor), typeof(Color), typeof(GravatarView), Colors.Black,
            propertyChanged: (b, o, n) => ((GravatarView)b).UpdateShape());

        /// <summary>Gets or sets the Gravatar item.</summary>
        public Gravatar Avatar
        {
            get => (Gravatar)GetValue(AvatarProperty);
            set => SetValue(AvatarProperty, value);
        }

        /// <summary>Gets or sets the email address.</summary>
        public string Email
        {
            get => (string)GetValue(EmailProperty);
            set => SetValue(EmailProperty, value);
        }

        /// <summary>Gets or sets the view style.</summary>
        public GravatarViewStyle ViewStyle
        {
            get => (GravatarViewStyle)GetValue(ViewStyleProperty);
            set => SetValue(ViewStyleProperty, value);
        }

        /// <summary>Gets or sets the border width.</summary>
        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        /// <summary>Gets or sets the border colour.</summary>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>Initializes a new instance of the <see cref="GravatarView"/> class.</summary>
        public GravatarView()
        {
            mImage = new Image { Aspect = Aspect.AspectFill };
            mBorder = new Border { Content = mImage, Padding = 0, StrokeThickness = 0 };
            Content = mBorder;
            UpdateShape();
        }

        private void OnEmailChanged(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                if (Avatar != null)
                    Avatar = null;
                return;
            }

            if (Avatar == null || !string.Equals(Avatar.Email, email, StringComparison.Ordinal))
                Avatar = new Gravatar(email);
        }

        private void OnAvatarChanged()
        {
            mRequestedSize = 0;
            _ = LoadImageAsync();
        }

        /// <inheritdoc />
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            UpdateShape();

            if (Avatar == null || width <= 0 || height <= 0)
                return;

            // Reload only when the view has grown beyond the size that was requested.
            if (PixelSize() > mRequestedSize)
                _ = LoadImageAsync();
        }

        private int PixelSize()
        {
            var side = Math.Min(Width, Height);

            if (double.IsNaN(side) || side <= 0)
                return kDefaultSize;

            return Math.Max(kDefaultSize, (int)Math.Ceiling(side * DeviceDisplay.Current.MainDisplayInfo.Density));
        }

        private async Task LoadImageAsync()
        {
            var avatar = Avatar;
            var version = ++mLoadVersion;

            if (avatar == null)
            {
                mImage.Source = null;
                return;
            }

            var size = PixelSize();
            mRequestedSize = size;

            try
            {
                var data = await GravatarControl.GetImage(avatar.Hash, size);

                // Ignore the result if a newer load was started or the avatar changed meanwhile.
                if (version != mLoadVersion || avatar != Avatar)
                    return;

                mImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(data));
            }
            catch (Exception)
            {
                if (version == mLoadVersion)
                    mImage.Source = null;
            }
        }

        private void UpdateShape()
        {
            mBorder.StrokeThickness = BorderWidth;
            mBorder.Stroke = new SolidColorBrush(BorderColor ?? Colors.Black);

            if (ViewStyle == GravatarViewStyle.Round)
            {
                mBorder.StrokeShape = new Ellipse();
            }
            else
            {
                mBorder.StrokeShape = new Rectangle();
            }
        }
    }
}
