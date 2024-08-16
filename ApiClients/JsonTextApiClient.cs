using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProjectKeyboardApplication.ApiClients
{
    public class JsonTextApiClient
    {
        private HttpClient _apiClient;
        private string _apiKey;
        public JsonTextApiClient(HttpClient httpClient)
        {
            _apiClient = httpClient;
            _apiKey = "TextContent";
        }
        public async Task<string?> GetPageJsonAsync(PageType pageType)
        {
            try
            {
                var response = await _apiClient.GetAsync($"{_apiKey}/{pageType}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();

                }
            }
            catch (Exception ex)
            {
              
            }
            return null;
        }
    }
}
