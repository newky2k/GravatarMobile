using System;
using AppKit;
using Foundation;

namespace GravatarMobile
{
	public static class GravatarExtensions
	{
		/// <summary>
		/// Load the images as a UIImage object
		/// </summary>
		/// <returns>The as user interface image.</returns>
		/// <param name="Item">Item.</param>
		public static NSImage ImageAsNSImage(this Gravatar Item)
		{
			return NSImage.FromStream(Item.ImageAsStream);
		}
	}
}

