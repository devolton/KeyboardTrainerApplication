using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.View.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.ApiClients
{
    public class EducationUserProgressApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _apiKey;
        public EducationUserProgressApiClient(HttpClient httpClient, JsonSerializerOptions jsonOptions = null)
        {
            _httpClient =httpClient;
            _jsonOptions = jsonOptions;
            _apiKey = "EducationUserProgress";
        }
        public async Task<IEnumerable<EducationUsersProgress>?> GetEducationUsersProgressesByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/{userId}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<EducationUsersProgress>>(jsonStr, _jsonOptions);
            }
            return null;
        }
        public async Task AddEducationUsersProgressAsync(EducationUsersProgress educationUsersProgress)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 64,
            };

            var jsonContent = JsonSerializer.Serialize(educationUsersProgress, jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiKey}", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateRangeEducationUsersProgress(IEnumerable<EducationUsersProgress> educationUsersProgressCollection)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 64,
            };

            var jsonContent = JsonSerializer.Serialize(educationUsersProgressCollection, jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiKey}/UpdateRange", content);
            response.EnsureSuccessStatusCode();
           
        }
        public async Task AddEductionUsersProgressRangeAsync(IEnumerable<EducationUsersProgress> educationUsersProgressesCollection)
        {
            var jsonOptions = new JsonSerializerOptions
            {
               
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 64,
            };

            var jsonContent = JsonSerializer.Serialize(educationUsersProgressesCollection, jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiKey}/AddRange", content);
            response.EnsureSuccessStatusCode();

        }
    }
}
