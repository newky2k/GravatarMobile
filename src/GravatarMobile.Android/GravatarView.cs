
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
using GravatarMobile.Core.Interfaces;
using GravatarMobile.Core;
using Android.Graphics;
using GravatarMobile.Core.Data;
using Android.Graphics.Drawables;

namespace GravatarMobile.Droid
{
	/// <summary>
	/// Android Gravatar View
	/// </summary>
	public class GravatarView : ImageView, IGravatarView
	{
		#region Fields

		private Gravatar mItem;
		private Enums.GravatarViewStyle mStyle = Enums.GravatarViewStyle.Square;
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

					LoadImage();


				}
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

					var bm =(mStyle == Enums.GravatarViewStyle.Round) ? 
						GetOvalBitmap(mItem.ImageAsBitmap()) : mItem.ImageAsBitmap();

					this.SetImageDrawable(new BitmapDrawable(bm));

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

		private async void LoadImage()
		{

			var size = (this.Width > 80) ? this.Width : 80;
		

			await mItem.LoadImage((int)size);

			this.Post(()=>
			{
				//mBitmap = mItem.ImageAsBitmap();
				var bm =(mStyle == Enums.GravatarViewStyle.Round) ? 
					GetOvalBitmap(mItem.ImageAsBitmap()) : mItem.ImageAsBitmap();

				this.SetImageDrawable(new BitmapDrawable(bm));

			});

		}
			
		public Bitmap GetOvalBitmap(Bitmap bitmap) 
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
	}
}

