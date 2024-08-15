using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace CourseProjectKeyboardApplication.ApiClients
{
    public class StaticImageApiClient
    {
        private  HttpClient _apiClient;
        private string _apiKey = "StaticImage";
        public StaticImageApiClient(HttpClient httpClient)
        {
            _apiClient = httpClient;


        }
        public async Task<ImageSource> GetImageSourceAsync(string fileName)
        {
            try
            {
                var byteArray = await _apiClient.GetByteArrayAsync($"{_apiKey}/{fileName}");
                using (var ms = new MemoryStream(byteArray))
                {
                    Image image = Image.FromStream(ms);
                    return ConvertToImageSource(image);

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //вынести в отдельный класс 
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
