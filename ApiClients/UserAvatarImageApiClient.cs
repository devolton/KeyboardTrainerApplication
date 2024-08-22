using CourseProjectKeyboardApplication.Tools.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.ApiClients
{
    public class UserAvatarImageApiClient
    {
        private HttpClient _apiClient;
        private string _apiKey = "UserAvatarImage";
        public UserAvatarImageApiClient(HttpClient httpClient)
        {
            _apiClient = httpClient;
        }

        /// <summary>
        /// Send a request to server to try upload user avatar image
        /// </summary>
        /// <param name="filePath">Path the avatar that the user want to upload</param>
        /// <param name="userLogin">User login</param>
        /// <returns>Remote avatar file name with extension</returns>
        public async Task<string> UploadFileAsync(string filePath, string userLogin)
        {

            using (var content = new MultipartFormDataContent())
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    var fileContent = new StreamContent(fileStream);
                    string mediaTypeHeaderValue="image/"+Path.GetExtension(filePath).Remove(0, 1);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(mediaTypeHeaderValue); 
                    content.Add(fileContent, "avatar", Path.GetFileName(filePath));

                    var response = await _apiClient.PostAsync($"{_apiKey}/{userLogin}", content);

                    if (response.IsSuccessStatusCode)
                    {
                       return await response.Content.ReadAsStringAsync();
                    }
                  
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// Send a request to server to get user avatar image source
        /// </summary>
        /// <param name="fileName">Name of user avatar with extension</param>
        /// <returns>User avatar image source of NULL</returns>
        public async Task<ImageSource?> GetUserAvatarAsync(string fileName)
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
