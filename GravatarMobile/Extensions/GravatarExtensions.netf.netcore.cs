using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GravatarMobile
{
    public static class GravatarExtensions
    {
        /// <summary>
        /// Load the images as a BitmapImage object
        /// </summary>
        /// <returns>The as user interface image.</returns>
        /// <param name="Item">Item.</param>
        public static BitmapImage ImageAsBitmap(this Gravatar Item)
        {
            if (Item == null)
                return null;

            return CreateBitmap(Item.Image);
        }

        /// <summary>
        /// Load the images as a BitmapImage object asyncronously
        /// </summary>
        /// <returns>The as user interface image.</returns>
        /// <param name="Item">Item.</param>
        public static Task<BitmapImage> ImageAsBitmapAsync(this Gravatar Item)
        {
            return Task.FromResult(ImageAsBitmap(Item));
        }

        private static BitmapImage CreateBitmap(byte[] data)
        {
            if (data == null || data.Length == 0)
                return null;

            using (var stream = new MemoryStream(data))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // decode now, the stream is disposed below
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze();

                return image;
            }
        }
    }
}
