using System;
using GravatarMobile.Core;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace GravatarMobile.Android
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
	}
}

