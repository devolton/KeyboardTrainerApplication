using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.ApiClients
{
    public class TypingTestResultApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _apiKey;
        public TypingTestResultApiClient(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions)
        {
            _httpClient = httpClient;
            _jsonOptions = jsonSerializerOptions;
            _apiKey = "TypingTestResult";


        }
        public async Task<IEnumerable<TypingTestResult>> GetTestsByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/{userId}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                var typingTutorTestsCollection = JsonSerializer.Deserialize<IEnumerable<TypingTestResult>>(jsonStr,_jsonOptions);
                return typingTutorTestsCollection;
            }
            return null;
        }
        public async Task<TypingTestResult?> GetBestUserTestAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/BestUserTest/{userId}");
            response.EnsureSuccessStatusCode();
            if(response .IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TypingTestResult>(jsonStr, _jsonOptions);

            }
            return null;
        }
        public async Task AddNewTypingTestResultAsync(TypingTestResult newTypingTestResult)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiKey}", newTypingTestResult);
            response.EnsureSuccessStatusCode(); 
        }
        public async Task RemoveTypingTestsByUserId(int userId)
        {
            var response = await _httpClient.DeleteAsync($"{_apiKey}/{userId}");
            response.EnsureSuccessStatusCode();

        }
    }
}
