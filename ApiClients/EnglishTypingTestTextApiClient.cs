using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<EnglishTypingTestText>?> GetAllTextsAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiKey}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<EnglishTypingTestText>>(jsonStr,_jsonOptions);

            }
            return null;
        }
        public async Task<EnglishTypingTestText?> GetTextByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/{id}");
            response.EnsureSuccessStatusCode();
            if(response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EnglishTypingTestText>(jsonStr, _jsonOptions);
            }
            return null;
        }
        public async Task RemoveTextAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiKey}/{id}");
            response.EnsureSuccessStatusCode();

        }
        public async Task AddNewTextAsync(EnglishTypingTestText text)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiKey}", text);
            response.EnsureSuccessStatusCode();
        }
    }
}
