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

        /// <summary>
        /// Send request to server to try get JSON of page static text content
        /// </summary>
        /// <param name="pageType">Type of page the text of which we want to receive </param>
        /// <returns>JSON string of page text content</returns>
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
