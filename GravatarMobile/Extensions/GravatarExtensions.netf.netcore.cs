using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

            if (Item != null && Item.Image != null)
            {
                using (var stream = new MemoryStream())
                {
                    var imagedata = Item.Image.ToArray();

                    stream.Write(imagedata, 0, imagedata.Length);

                    var image = new BitmapImage();
                    stream.Position = 0;

                    image.StreamSource = stream;

                    return image;
                }
            }

            return null;

        }

        /// <summary>
        /// Load the images as a BitmapImage object asyncronously
        /// </summary>
        /// <returns>The as user interface image.</returns>
        /// <param name="Item">Item.</param>
        public static async Task<BitmapImage> ImageAsBitmapAsync(this Gravatar Item)
        {

            if (Item != null && Item.Image != null)
            {
                using (var stream = new MemoryStream())
                {
                    var imagedata = Item.Image.ToArray();

                    await stream.WriteAsync(imagedata, 0, imagedata.Length);

                    var image = new BitmapImage();
                    stream.Position = 0;

                    image.StreamSource = stream;

                    return image;
                }
            }

            return null;

        }
    }
}
