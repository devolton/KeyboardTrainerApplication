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
    public class EnglishLayoutLevelApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _apiKey;
        public EnglishLayoutLevelApiClient(HttpClient httpClient,JsonSerializerOptions jsonSerializerOptions = null)
        {
            _httpClient = httpClient;
            _jsonOptions = jsonSerializerOptions;
            _apiKey = "EnglishLayoutLevel";
        }
        public async Task<IEnumerable<EnglishLayoutLevel>?> GetAllLevelsAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiKey}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<EnglishLayoutLevel>>(jsonStr, _jsonOptions);
            }
            return null;
        }
        public async Task<EnglishLayoutLevel?> GetLevelByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/{id}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EnglishLayoutLevel>(jsonStr, _jsonOptions);
            }
            return null;
        }
        public async Task UpdateLevelAsync(int id, EnglishLayoutLevel level)
        {
            var response = await _httpClient.PutAsJsonAsync<EnglishLayoutLevel>($"{_apiKey}/{id}/{level}", level);
            response.EnsureSuccessStatusCode();
            
        }
        public async Task AddNewLevelAsync(EnglishLayoutLevel newLevel)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiKey}", newLevel);
            response.EnsureSuccessStatusCode();
        }
    }
}
