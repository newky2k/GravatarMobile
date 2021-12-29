using System;
using System.Drawing;
using System.ComponentModel;

using UIKit;
using Foundation;
using CoreAnimation;
using CoreGraphics;

namespace GravatarMobile
{
    [Register("GravatarView")]
    public class GravatarView : UIView, IGravatarView
    {
        #region Fields

        private Gravatar mItem;
        private UIImageView mImageView;
		private float mBorderWidth = 0;
        private GravatarViewStyle mStyle = GravatarViewStyle.Square;
        private CAShapeLayer mMaskLayer;
        private CAShapeLayer mBorderLayer;
		private UIColor mBorderColor;
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

                    this.SetNeedsLayout();
                }
            }
        }

		[Export("BorderWidth"), Browsable(true), DisplayNameAttribute("Border Width")]
        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public float BorderWidth
        {
            get { return mBorderWidth; }
            set
            { 
                if (mBorderWidth != value)
                {
                    mBorderWidth = value;
                    this.SetNeedsLayout();
                }
            }
        }
			

		[Export("ViewStyle"), Browsable(true), DisplayNameAttribute("Intial View Style")]
        /// <summary>
        /// Gets or sets the view style.
        /// </summary>
        /// <value>The view style.</value>
        public GravatarViewStyle ViewStyle
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

                    this.SetNeedsLayout();
                }
            }
        }

		/// <summary>
		/// Gets or sets the color of the border.
		/// </summary>
		/// <value>The color of the border.</value>
		public UIColor BorderColor
		{
			get
			{
				if (mBorderColor == null)
				{
					mBorderColor = UIColor.Black;
				}

				return mBorderColor;
			}
			set
			{
				if (mBorderColor != value)
				{
					mBorderColor = value;

					SetNeedsLayout();
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

        /// <summary>
        /// Prepare this instance.
        /// </summary>
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
            
			await mItem.LoadImageAsync((int)size);

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

            AddMask(this.Frame);
        }

        /// <summary>
        /// Adds the mask.
        /// </summary>
        /// <param name="MaskBounds">Mask bounds.</param>
        private void AddMask(CGRect MaskBounds)
        {
            //clear old layers
            if (mBorderLayer != null)
            {
                mBorderLayer.RemoveFromSuperLayer();
            }

            this.Layer.Mask = null;

            switch (mStyle)
            {
                case GravatarViewStyle.Square:
                    {
                        if ((int)this.BorderWidth > 0)
                        {
							var point = new CGPoint(MaskBounds.Size.Width / 2, MaskBounds.Size.Height / 2);
							var maskPath = CGPath.FromRect(MaskBounds);
							mBorderLayer = (CAShapeLayer)CAShapeLayer.Create();
							mBorderLayer.Bounds = MaskBounds;
							mBorderLayer.Path = maskPath;
							mBorderLayer.LineWidth = this.BorderWidth * 2.0f;
							mBorderLayer.StrokeColor = BorderColor.CGColor;
							mBorderLayer.FillColor = UIColor.Clear.CGColor;
							mBorderLayer.Position = point;

							this.Layer.AddSublayer(mBorderLayer);
                        }
                    }
                    break;
                case GravatarViewStyle.Round:
                    {
                        mMaskLayer = new CAShapeLayer();

                        var maskPath = CGPath.EllipseFromRect(MaskBounds, CGAffineTransform.MakeIdentity());

                        mMaskLayer.Bounds = MaskBounds;
                        mMaskLayer.Path = maskPath;
                        mMaskLayer.FillColor = UIColor.Blue.CGColor;

                        var point = new CGPoint(MaskBounds.Size.Width / 2, MaskBounds.Size.Height / 2);
                        mMaskLayer.Position = point;

                        this.Layer.Mask = mMaskLayer;


                        if ((int)this.BorderWidth > 0)
                        {
                            mBorderLayer = (CAShapeLayer)CAShapeLayer.Create();
                            mBorderLayer.Bounds = MaskBounds;
                            mBorderLayer.Path = maskPath;
                            mBorderLayer.LineWidth = this.BorderWidth * 2.0f;
							mBorderLayer.StrokeColor = BorderColor.CGColor;
                            mBorderLayer.FillColor = UIColor.Clear.CGColor;
                            mBorderLayer.Position = point;

                            this.Layer.AddSublayer(mBorderLayer);
                        }
                    }
                    break;
            }


        }

        #endregion
    }
}

