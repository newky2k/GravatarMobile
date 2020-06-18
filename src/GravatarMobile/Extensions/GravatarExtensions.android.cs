using System;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace GravatarMobile
{
	public static class GravatarExtensions
	{
		/// <summary>
		/// Load the images as a UIImage object
		/// </summary>
		/// <returns>The as user interface image.</returns>
		/// <param name="Item">Item.</param>
		public static Drawable ImageAsDrawable(this Gravatar Item)
		{
			//Item.ImageAsStream
			var bm = BitmapFactory.DecodeStream(Item.ImageAsStream,null, null);

			return new BitmapDrawable(bm);
		}

		/// <summary>
		/// Images as bitmap.
		/// </summary>
		/// <returns>The as bitmap.</returns>
		/// <param name="Item">Item.</param>
		public static Bitmap ImageAsBitmap(this Gravatar Item)
		{
			var bm = BitmapFactory.DecodeStream(Item.ImageAsStream,null, null);

			return bm;
		}
	}
}

