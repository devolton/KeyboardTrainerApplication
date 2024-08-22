using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


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
        /// <summary>
        /// Send a request to the server and try get collection of EducationUsersProgress by user id
        /// </summary>
        /// <param name="userId">User ID value</param>
        /// <returns></returns>
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
        /// <summary>
        /// Send a request to the server to try add new EducatonUsersProgress
        /// </summary>
        /// <param name="educationUsersProgress">Created EducationUsersProgress</param>
        /// <returns></returns>
        public async Task AddEducationUsersProgressAsync(EducationUsersProgress educationUsersProgress)
        {
            try
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
            catch(Exception ex)
            {
                
                return;
            }
        }
        /// <summary>
        /// Send a request to the server to try update existing EducationUsersProgress
        /// </summary>
        /// <param name="educationUsersProgressCollection">EducationUsersProgressElement for update</param>
        /// <returns></returns>
        public async Task UpdateRangeEducationUsersProgress(IEnumerable<EducationUsersProgress> educationUsersProgressCollection)
        {
            try
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
            catch(Exception ex)
            {
                return;
            }
           
        }
        /// <summary>
        /// Send a request to server to try add collection of new EducationUsersProgress 
        /// </summary>
        /// <param name="educationUsersProgressesCollection">Collection of new EducatonUsersProgress elements</param>
        /// <returns></returns>
        public async Task AddEductionUsersProgressRangeAsync(IEnumerable<EducationUsersProgress> educationUsersProgressesCollection)
        {
            try
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
            catch(Exception ex)
            {
                return;
            }

        }
    }
}
