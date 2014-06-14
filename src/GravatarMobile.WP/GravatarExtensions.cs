using GravatarMobile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GravatarMobile.WP
{
    public static class GravatarExtensions
    {
        /// <summary>
        /// Load the images as a UIImage object
        /// </summary>
        /// <returns>The as user interface image.</returns>
        /// <param name="Item">Item.</param>
        public static BitmapImage ImageAsBitmap(this Gravatar Item)
        {
            var bitmap = new BitmapImage();
            bitmap.SetSource(Item.ImageAsStream);

            return bitmap;
        }
    }
}
