using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CourseProjectKeyboardApplication.ApiClients
{
    public class TypingTestResultApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _apiKey;
        public TypingTestResultApiClient(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions = null)
        {
            _httpClient = httpClient;
            _jsonOptions = jsonSerializerOptions;
            _apiKey = "TypingTestResult";


        }
        public async Task<IEnumerable<TypingTestResult>> GetTestsByUserIdAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/{userId}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var jsonStr = await response.Content.ReadAsStringAsync();
                    var typingTutorTestsCollection = JsonSerializer.Deserialize<IEnumerable<TypingTestResult>>(jsonStr, _jsonOptions);
                    return typingTutorTestsCollection;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            var jsonContent = JsonSerializer.Serialize(newTypingTestResult, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiKey}", content);
            response.EnsureSuccessStatusCode();

        }
        public async Task RemoveTypingTestsByUserId(int userId)
        {
            var response = await _httpClient.DeleteAsync($"{_apiKey}/{userId}");
            response.EnsureSuccessStatusCode();

        }
    }
}
