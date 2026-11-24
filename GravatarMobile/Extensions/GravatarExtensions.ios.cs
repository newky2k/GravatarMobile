using System;

using UIKit;
using Foundation;
using CoreGraphics;

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
            return Item.ImageAsUIImage(UITraitCollection.CurrentTraitCollection.DisplayScale);
        }

        /// <summary>
        /// Load the images as a UIImage object with the specified display scale
        /// </summary>
        /// <returns>The as user interface image.</returns>
        /// <param name="Item">Item.</param>
        /// <param name="Scale">The display scale of the screen the image will be shown on.</param>
        public static UIImage ImageAsUIImage(this Gravatar Item, nfloat Scale)
        {
            if (Scale <= 0)
                Scale = 1;

            using (var data = NSData.FromStream(Item.ImageAsStream))
            {
                return UIImage.LoadFromData(data, Scale);
            }
        }
    }
}
