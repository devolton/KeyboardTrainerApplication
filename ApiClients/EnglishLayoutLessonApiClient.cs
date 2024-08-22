using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

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
        /// <summary>
        /// Send request to server to try get collecton of EnglishLayoutLesson elements
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EnglishLayoutLesson>?> GetLessonsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IEnumerable<EnglishLayoutLesson>>(jsonStr, _jsonOptions);
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return null;
        }
        /// <summary>
        /// Send request to server to get EnglishLayoutLesson collection by EnglishLayoutLevel ID
        /// </summary>
        /// <param name="levelId">EnglishLayoutLevel ID param</param>
        /// <returns></returns>
        public async Task<IEnumerable<EnglishLayoutLesson>?> GetLessonsByLevelIdAsync(int levelId)
        {
            try
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
            catch(Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Send a request to server to try update existing EnglishLayoutLesson element
        /// </summary>
        /// <param name="updatedLesson">Existing EnglishLayoutLesson element for update</param>
        /// <returns></returns>
        public async Task UpdateLessonAsync(EnglishLayoutLesson updatedLesson)
        {
            try
            {
                int id = updatedLesson.Id;
                var response = await _httpClient.PutAsJsonAsync($"{_apiKey}/{id}/{updatedLesson}", updatedLesson);
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {
                return;
            }

        }

    }
}
