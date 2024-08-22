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
using CourseProjectKeyboardApplication.Tools.Converters;

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
        /// <summary>
        /// Send request to server to try get ImageSource by image name(with extentions)
        /// </summary>
        /// <param name="fileName">Image name with extensions which we want to receive</param>
        /// <returns>ImageSource or NULL</returns>
        public async Task<ImageSource> GetImageSourceAsync(string fileName)
        {
            try
            {
                var byteArray = await _apiClient.GetByteArrayAsync($"{_apiKey}/{fileName}");
                using (var ms = new MemoryStream(byteArray))
                {
                    Image image = Image.FromStream(ms);
                    return FromImageToImageSourceConverter.ConvertToImageSource(image);

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
