using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

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

        /// <summary>
        /// Send a request to server to get all TypingTestResult of specific user
        /// </summary>
        /// <param name="userId">ID of the user whose tests we want to receive</param>
        /// <returns>Collection of TypingTestResult or NULL</returns>
        public async Task<IEnumerable<TypingTestResult>?> GetTestsByUserIdAsync(int userId)
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
                
            }
            return null;
        }
        /// <summary>
        /// Send a request to server to try get best TypingTestResult specific user
        /// </summary>
        /// <param name="userId">ID of the user whose test we want to receive</param>
        /// <returns></returns>
        public async Task<TypingTestResult?> GetBestUserTestAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/BestUserTest/{userId}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var jsonStr = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<TypingTestResult>(jsonStr, _jsonOptions);

                }
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// Send a request to server and try add new TypingTestResult entity
        /// </summary>
        /// <param name="newTypingTestResult">Created TypingTestResult entity</param>
        /// <returns></returns>
        public async Task AddNewTypingTestResultAsync(TypingTestResult newTypingTestResult)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(newTypingTestResult, _jsonOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiKey}", content);
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {

            }

        }

        /// <summary>
        /// Send a request to server to try remove all TypingTestResult of specific user
        /// </summary>
        /// <param name="userId">User id </param>
        /// <returns></returns>
        public async Task RemoveTypingTestsByUserId(int userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiKey}/{userId}");
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {

            }

        }

        /// <summary>
        /// Send a request to server to try add TypingTestResult collection 
        /// </summary>
        /// <param name="typingTestResultsCollection">Collection of TypingTestResult which we want add</param>
        /// <returns></returns>
        public async Task AddRangeTypingTestsAsync(IEnumerable<TypingTestResult> typingTestResultsCollection)
        {
            try
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    MaxDepth = 64,
                };

                var jsonContent = JsonSerializer.Serialize(typingTestResultsCollection, jsonOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiKey}/AddRange", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
