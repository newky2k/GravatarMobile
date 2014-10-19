using System;
using GravatarMobile.Core;

#if __UNIFIED__
using UIKit;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.Foundation;
#endif
namespace GravatarMobile.Extensions
{
    /// <summary>
    /// Gravatar extensions.
    /// </summary>
    public static class GravatarExtensions
    {
        /// <summary>
        /// Load the images as a UIImage object
        /// </summary>
        /// <returns>The as user interface image.</returns>
        /// <param name="Item">Item.</param>
        public static UIImage ImageAsUIImage(this Gravatar Item)
        {
            return UIImage.LoadFromData(NSData.FromStream(Item.ImageAsStream), UIScreen.MainScreen.Scale);
        }
    }
}

