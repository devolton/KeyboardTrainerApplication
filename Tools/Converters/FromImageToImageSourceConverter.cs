using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;

namespace CourseProjectKeyboardApplication.Tools.Converters
{
    public static class FromImageToImageSourceConverter
    {
        /// <summary>
        /// Convert png image to ImageSource
        /// </summary>
        /// <param name="image">Image which we want convert</param>
        /// <returns>Image source which base in image</returns>
        public static ImageSource ConvertToImageSource(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                memoryStream.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
