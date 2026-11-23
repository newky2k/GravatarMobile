using System;
using Android.Content;
using Android.Util;
using Android.Widget;
using Android.Graphics;

namespace GravatarMobile
{
	/// <summary>
	/// Android Gravatar View
	/// </summary>
	public class GravatarView : ImageView, IGravatarView
	{
		#region Fields

		private Gravatar mItem;
		private GravatarViewStyle mStyle = GravatarViewStyle.Square;
		private Bitmap mBitmap;
		private int mLoadedSize;

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
					mBitmap = null;
					mLoadedSize = 0;

					LoadImageAsync();
				}
			}
		}

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

					ShowImage();

					this.Invalidate();
				}
			}
		}

		#endregion

		#region Constructors

		public GravatarView(Context context) :
			base(context)
		{
		}

		public GravatarView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			ReadStyle(attrs);
		}

		public GravatarView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
			ReadStyle(attrs);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Reads app:initial_style="square|round" from the layout (see Resources/values/attrs.xml).
		/// </summary>
		private void ReadStyle(IAttributeSet attrs)
		{
			var initialStyle = attrs?.GetAttributeValue("http://schemas.android.com/apk/res-auto", "initial_style");

			if (string.Equals(initialStyle, "round", StringComparison.OrdinalIgnoreCase) || initialStyle == "1")
				mStyle = GravatarViewStyle.Round;
		}

		/// <summary>
		/// Gets the size in pixels to request the image at: the view's width, or the width from its layout parameters before it has been laid out.
		/// </summary>
		private int GetImageSize()
		{
			var size = this.Width;

			if (size <= 0 && this.LayoutParameters != null && this.LayoutParameters.Width > 0)
				size = this.LayoutParameters.Width;

			return (size > 80) ? size : 80;
		}

		/// <summary>
		/// Loads the image async.
		/// </summary>
		private async void LoadImageAsync()
		{
			var item = mItem;

			if (item == null)
			{
				this.SetImageBitmap(null);
				return;
			}

			try
			{
				var size = GetImageSize();
				mLoadedSize = size;

				// fetch the image for this view's size, so it is not replaced by an image the Gravatar loaded at another size
				var data = await GravatarControl.GetImage(item.Hash, size);
				var bitmap = BitmapFactory.DecodeByteArray(data, 0, data.Length);

				this.Post(() =>
				{
					// ignore the result if the avatar was replaced while loading
					if (item != mItem)
						return;

					mBitmap = bitmap;
					ShowImage();
				});
			}
			catch (Exception)
			{
				// the image could not be loaded, leave the view as it is
			}
		}

		/// <summary>
		/// Shows the loaded image, in the current style.
		/// </summary>
		private void ShowImage()
		{
			if (mBitmap == null)
				return;

			this.SetImageBitmap((mStyle == GravatarViewStyle.Round) ? GetOvalBitmap(mBitmap) : mBitmap);
		}

		/// <summary>
		/// Reloads the image at a better resolution once the view has been laid out and is bigger than the size it was loaded at.
		/// </summary>
		protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged(w, h, oldw, oldh);

			if (mItem != null && w > mLoadedSize && w > 80)
				LoadImageAsync();
		}

		/// <summary>
		/// Gets the oval bitmap.
		/// </summary>
		/// <returns>The oval bitmap.</returns>
		/// <param name="bitmap">Bitmap.</param>
		private Bitmap GetOvalBitmap(Bitmap bitmap)
		{
			Bitmap output = null;

			if (bitmap != null)
			{
				output = Bitmap.CreateBitmap(bitmap.Width, bitmap.Height, Bitmap.Config.Argb8888);
				Canvas canvas = new Canvas(output);

				Paint paint = new Paint();
				Rect rect = new Rect(0, 0, bitmap.Width, bitmap.Height);
				RectF rectF = new RectF(rect);

				paint.AntiAlias = true;
				canvas.DrawARGB(0, 0, 0, 0);
				paint.Color = Color.Red;
				canvas.DrawOval(rectF, paint);

				paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));
				canvas.DrawBitmap(bitmap, rect, rect, paint);
			}

			return output;
		}

		#endregion
	}
}
