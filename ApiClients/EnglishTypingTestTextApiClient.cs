using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CourseProjectKeyboardApplication.ApiClients
{
    public class EnglishTypingTestTextApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _apiKey;
        public EnglishTypingTestTextApiClient(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions =null)
        {
            _httpClient = httpClient;
            _jsonOptions = jsonSerializerOptions;
            _apiKey = "EnglishTypingTestText";

        }

        /// <summary>
        /// Send a request to server and try get all EnglishTypingTestText elements
        /// </summary>
        /// <returns>Collection of EnglishLayoutTestText</returns>
        public async Task<IEnumerable<EnglishTypingTestText>?> GetAllTextsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IEnumerable<EnglishTypingTestText>>(jsonStr, _jsonOptions);

                }
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// Send a request to server to try get EnglishLayoutTestText by Id
        /// </summary>
        /// <param name="id">Id of EnglishLayoutTestText</param>
        /// <returns>EnglishLayoutTestText with the specific ID or NULL</returns>
        public async Task<EnglishTypingTestText?> GetTextByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/{id}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<EnglishTypingTestText>(jsonStr, _jsonOptions);
                }
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// Send a request and try to remove EnglishLayoutTestText by Id 
        /// </summary>
        /// <param name="id">EngilshTypingTestText Id</param>
        /// <returns></returns>
        public async Task RemoveTextAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiKey}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {

            }

        }

        /// <summary>
        /// Send request to server to try add new EnglishLayoutTestText
        /// </summary>
        /// <param name="text">Created EnglishLayoutTestText</param>
        /// <returns></returns>
        public async Task AddNewTextAsync(EnglishTypingTestText text)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiKey}", text);
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
