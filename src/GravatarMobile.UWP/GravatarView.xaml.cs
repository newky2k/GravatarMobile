using GravatarMobile.Core;
using GravatarMobile.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace GravatarMobile.UWP
{
    public sealed partial class GravatarView : UserControl
    {
        public static DependencyProperty IntitialStyleProperty = DependencyProperty.Register("IntitialStyle", typeof(Enums.GravatarViewStyle), typeof(GravatarView), null);

        #region Fields

        private Gravatar mItem;
        private Enums.GravatarViewStyle mStyle = Enums.GravatarViewStyle.Square;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Gravatar item.
        /// </summary>
        /// <value>The item.</value>
        public Gravatar Avatar
        {
            get
            {
                return mItem;
            }
            set
            {
                if (mItem != value)
                {
                    mItem = value;

                    LoadImageAysnc();
                }
            }
        }

        /// <summary>
        /// Get the initial style
        /// </summary>
        public Enums.GravatarViewStyle IntitialViewStyle
        {
            get
            {
                return (Enums.GravatarViewStyle)GetValue(IntitialStyleProperty);
            }
            set
            {
                SetValue(IntitialStyleProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the view style.
        /// </summary>
        /// <value>The view style.</value>
        public Enums.GravatarViewStyle ViewStyle
        {
            get
            {
                return mStyle;
            }
            set
            {
                if (mStyle != value)
                {
                    mStyle = value;

                    UpdateStyle();
                }
            }
        }

        private void UpdateStyle()
        {
            if (mStyle == Enums.GravatarViewStyle.Round)
            {
                imgAvatar.Visibility = Visibility.Collapsed;

                imgCircle.Visibility = Visibility.Visible;
                //throw new NotSupportedException("Round mode not supported on UWP");


                //imgAvatar.Clip = new EllipseGeometry()
                //{
                //    Center = new Point(this.Width / 2, this.Height / 2),
                //    RadiusX = this.Width / 2,
                //    RadiusY = this.Height / 2,
                //};
            }
            else
            {
                imgAvatar.Visibility = Visibility.Visible;

                imgCircle.Visibility = Visibility.Collapsed;
                // imgAvatar.Clip = null;
            }
        }

        #endregion


        public GravatarView()
        {
            this.InitializeComponent();
        }

        #region Private Methods

        private async void LoadImageAysnc()
        {
            var size = this.Width;

            await mItem.LoadImageAsync((int)size);

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {

                var image = await mItem.ImageAsBitmapAsync();
                imgAvatar.Source = image;
                imgAvatarRound.ImageSource = image;

            });

        }

        #endregion

        #region Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewStyle = IntitialViewStyle;
        }

        #endregion
    }
}
