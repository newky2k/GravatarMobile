using System;

using UIKit;
using Foundation;

namespace GravatarMobile
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

