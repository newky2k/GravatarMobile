using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;

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
		#endregion
		#region Properties
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

					ShowImage(mItem);

					this.Invalidate();
				}
			}
		}
		#endregion

		#region Constructors
		public GravatarView(Context context) :
			base(context)
		{
			Initialize();
		}

		public GravatarView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			// app:initial_style="square|round" (see Resources/values/attrs.xml)
			var initialStyle = attrs?.GetAttributeValue("http://schemas.android.com/apk/res-auto", "initial_style");

			if (string.Equals(initialStyle, "round", StringComparison.OrdinalIgnoreCase) || initialStyle == "1")
				mStyle = GravatarViewStyle.Round;

			Initialize();
		}

		public GravatarView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
			Initialize();
		}

		private void Initialize ()
		{

		}

		#endregion

		#region Methods

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
				var size = (this.Width > 80) ? this.Width : 80;

				await item.LoadImageAsync(size);

				this.Post(() =>
				{
					// ignore the result if the avatar was replaced while loading
					if (item == mItem)
						ShowImage(item);
				});
			}
			catch (Exception)
			{
				// the image could not be loaded, leave the view as it is
			}
		}

		/// <summary>
		/// Shows the loaded image of the Gravatar, in the current style.
		/// </summary>
		private void ShowImage(Gravatar item)
		{
			if (item == null)
				return;

			Bitmap bm;

			try
			{
				bm = item.ImageAsBitmap();
			}
			catch (Exception)
			{
				// the image has not been loaded yet
				return;
			}

			this.SetImageBitmap((mStyle == GravatarViewStyle.Round) ? GetOvalBitmap(bm) : bm);
		}

		/// <summary>
		/// Gets the oval bitmap.
		/// </summary>
		/// <returns>The oval bitmap.</returns>
		/// <param name="bitmap">Bitmap.</param>
		private Bitmap GetOvalBitmap(Bitmap bitmap) 
		{
			Bitmap output = null;

			if(bitmap != null)
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

