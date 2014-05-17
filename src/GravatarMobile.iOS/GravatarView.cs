using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using GravatarMobile.Core.Interfaces;
using GravatarMobile.Core;
using GravatarMobile.Extensions;

namespace GravatarMobile.iOS
{
    [Register("GravatarView")]
    public class GravatarView : UIView, IGravatarView
    {
        #region Fields

        private Gravatar mItem;
        private UIImageView mImageView;

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

                    LoadImage();
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GravatarMobile.iOS.GravatarView"/> class.
        /// </summary>
        /// <param name="Frame">Frame.</param>
        public GravatarView(RectangleF Frame) : base(Frame)
        {
            Prepare();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GravatarMobile.iOS.GravatarView"/> class.
        /// </summary>
        public GravatarView()
        {
            Prepare();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GravatarMobile.iOS.GravatarView"/> class.
        /// </summary>
        /// <param name="Coder">Coder.</param>
        public GravatarView(NSCoder Coder) : base(Coder)
        {
            Prepare();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GravatarMobile.iOS.GravatarView"/> class.
        /// </summary>
        /// <param name="t">T.</param>
        public GravatarView(NSObjectFlag t) : base(t)
        {
            Prepare();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GravatarMobile.iOS.GravatarView"/> class.
        /// </summary>
        /// <param name="Handle">Handle.</param>
        public GravatarView(IntPtr Handle) : base(Handle)
        {
            Prepare();
        }

        #endregion

        #region Methods

        private void Prepare()
        {
            mImageView = new UIImageView(RectangleF.Empty);
            mImageView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
            mImageView.AutosizesSubviews = true;

            this.Add(mImageView);
        }

        private async void LoadImage()
        {
            var size = (this.Frame.Size.Width > 80) ? this.Frame.Size.Width : 80;

            size = size * UIScreen.MainScreen.Scale;
            
            await mItem.LoadImage((int)size);

            BeginInvokeOnMainThread(() =>
            {
                mImageView.Image = mItem.ImageAsUIImage();
            });

        }

        #endregion

        #region Overrides

        /// <Docs>Lays out subviews.</Docs>
        /// <summary>
        /// Layouts the subviews.
        /// </summary>
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            mImageView.Frame = this.Bounds;
        }

        #endregion
    }
}

