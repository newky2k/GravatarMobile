using System;
using Foundation;
using GravatarMobile.Core.Interfaces;
using GravatarMobile.Core;
using GravatarMobile.Core.Data;
using CoreAnimation;
using System.ComponentModel;
using AppKit;
using CoreGraphics;

namespace GravatarMobile.Mac
{
	[Register("GravatarView")]
	public class GravatarView : NSView, IGravatarView
	{
		#region Fields

		private Gravatar mItem;
		private NSImageView mImageView;
		private float mBorderWidth = 0;
		private Enums.GravatarViewStyle mStyle = Enums.GravatarViewStyle.Square;
		private CAShapeLayer mMaskLayer;
		private CAShapeLayer mBorderLayer;
		private NSColor mBorderColor;
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

					this.SetNeedsDisplayInRect(this.Frame);

					ResizeSubviewsWithOldSize(this.Frame.Size);

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
					this.SetNeedsDisplayInRect(this.Frame);
					ResizeSubviewsWithOldSize(this.Frame.Size);
				}
			}
		}
			
		[Export("ViewStyle"), Browsable(true), DisplayNameAttribute("Intial View Style")]
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

					this.SetNeedsDisplayInRect(this.Frame);
					ResizeSubviewsWithOldSize(this.Frame.Size);
				}
			}
		}

		/// <summary>
		/// Gets or sets the color of the border.
		/// </summary>
		/// <value>The color of the border.</value>
		public NSColor BorderColor
		{
			get
			{
				if (mBorderColor == null)
				{
					mBorderColor = NSColor.Black;
				}

				return mBorderColor;
			}
			set
			{
				if (mBorderColor != value)
				{
					mBorderColor = value;
					this.SetNeedsDisplayInRect(this.Frame);
					ResizeSubviewsWithOldSize(this.Frame.Size);
				}
			}
		}
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarMobile.iOS.GravatarView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		public GravatarView(CGRect Frame) : base(Frame)
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
			this.WantsLayer = true;
			mImageView = new NSImageView(CGRect.Empty);
			mImageView.AutoresizingMask = NSViewResizingMask.HeightSizable | NSViewResizingMask.WidthSizable;
			mImageView.AutoresizesSubviews = true;

			this.AddSubview(mImageView);
		}

		private async void LoadImage()
		{
			var size = (this.Frame.Size.Width > 80) ? this.Frame.Size.Width : 80;

			size = size * NSApplication.SharedApplication.MainWindow.BackingScaleFactor;

			await mItem.LoadImageAsync((int)size);

			BeginInvokeOnMainThread(() =>
			{
				mImageView.Image = mItem.ImageAsNSImage();
			});

		}

		#endregion

		#region Overrides

	    public override void DrawRect(CGRect dirtyRect)
		{
			base.DrawRect(dirtyRect);

			mImageView.Frame = this.Bounds;

		}

		public override void ResizeSubviewsWithOldSize(CGSize oldSize)
		{
			base.ResizeSubviewsWithOldSize(oldSize);

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

			if (this.Layer == null)
			{
				this.Layer = new CALayer();
			}

			this.Layer.Mask = null;

			switch (mStyle)
			{
				case Enums.GravatarViewStyle.Square:
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
							mBorderLayer.FillColor = NSColor.Clear.CGColor;
							mBorderLayer.Position = point;

							this.Layer.AddSublayer(mBorderLayer);
						}
					}
					break;
				case Enums.GravatarViewStyle.Round:
					{
						mMaskLayer = new CAShapeLayer();

						var maskPath = CGPath.EllipseFromRect(MaskBounds, CGAffineTransform.MakeIdentity());


						mMaskLayer.Bounds = MaskBounds;
						mMaskLayer.Path = maskPath;
						mMaskLayer.FillColor = NSColor.Blue.CGColor;

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
							mBorderLayer.FillColor = NSColor.Clear.CGColor;
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

