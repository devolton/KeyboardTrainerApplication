using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.ApiClients
{
    public class EnglishLayoutLessonApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _apiKey;
        public EnglishLayoutLessonApiClient(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions = null)
        {
            _httpClient = httpClient;
            _jsonOptions = jsonSerializerOptions;
            _apiKey = "EnglishLayoutLesson";
        }
        public async Task<IEnumerable<EnglishLayoutLesson>?> GetLessonsByLevelIdAsync(int levelId)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/{levelId}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<EnglishLayoutLesson>>(jsonStr, _jsonOptions);

            }
            return null;
        }
        public async Task UpdateLessonAsync(EnglishLayoutLesson updatedLesson)
        {
            int id = updatedLesson.Id;
            var response = await _httpClient.PutAsJsonAsync($"{_apiKey}/{id}/{updatedLesson}", updatedLesson);
            response.EnsureSuccessStatusCode();

        }

    }
}
