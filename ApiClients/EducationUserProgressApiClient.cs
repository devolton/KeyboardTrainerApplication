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
            var response = await _httpClient.PostAsJsonAsync($"{_apiKey}", educationUsersProgress);
            response.EnsureSuccessStatusCode();
        }
        public async Task AddEductionUsersProgressRangeAsync(IEnumerable<EducationUsersProgress> educationUsersProgressesCollection)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiKey}/AddRange", educationUsersProgressesCollection);
            response.EnsureSuccessStatusCode();
        }
    }
}
