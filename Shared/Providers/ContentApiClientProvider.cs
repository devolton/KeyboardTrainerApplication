
using CourseProjectKeyboardApplication.ApiClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Providers
{
    public static class ContentApiClientProvider 
    {
        private static HttpClient _httpClient;

        private static StaticImageApiClient _staticImageApiClient;
        private static UserAvatarImageApiClient _userAvatarImageApiClient;
        private static JsonTextApiClient _jsonTextApiClient;
        public static StaticImageApiClient StaticImageApiClient => _staticImageApiClient;
        public static UserAvatarImageApiClient UserAvatarImageApiClient => _userAvatarImageApiClient;
        public static JsonTextApiClient JsonTextApiClient => _jsonTextApiClient;
        static ContentApiClientProvider()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(@"https://localhost:7000/");
            _staticImageApiClient = new StaticImageApiClient(_httpClient);
            _userAvatarImageApiClient = new UserAvatarImageApiClient(_httpClient);
            _jsonTextApiClient = new JsonTextApiClient(_httpClient);


        }
        public static void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
