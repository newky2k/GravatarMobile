using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

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
                using (var stream = new InMemoryRandomAccessStream())
                {
                    
                    stream.WriteAsync(Item.Image.AsBuffer()).GetResults();
                   
                    var image = new BitmapImage();
                    stream.Seek(0);
                    image.SetSource(stream);
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
                using (var stream = new InMemoryRandomAccessStream())
                {

                    await stream.WriteAsync(Item.Image.AsBuffer());

                    var image = new BitmapImage();
                    stream.Seek(0);
                    image.SetSource(stream);
                    return image;
                }
            }

            return null;

        }

    }
}
