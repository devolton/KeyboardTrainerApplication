using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;


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

        /// <summary>
        /// Send request to server to get collection of all EnglishLayoutLevel elements
        /// </summary>
        /// <returns>EnglishLayoutLevel collection</returns>
        public async Task<IEnumerable<EnglishLayoutLevel>?> GetAllLevelsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IEnumerable<EnglishLayoutLevel>>(jsonStr, _jsonOptions);
                }
                
            }
            catch(Exception ex)
            {
              
            }
            return null;
        }

        /// <summary>
        /// Send request to server to get EnglishLayoutLevel by Id
        /// </summary>
        /// <param name="id">Id of EnglishLayoutLevel</param>
        /// <returns>EnglishLayoutLevel with the specified Id or NULL</returns>
        public async Task<EnglishLayoutLevel?> GetLevelByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/{id}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var jsonStr = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<EnglishLayoutLevel>(jsonStr, _jsonOptions);
                }
            }
            catch(Exception ex) {

            }
            return null;
        }

        /// <summary>
        /// Send a request to server to update existing EnglishLayoutLevel element
        /// </summary>
        /// <param name="id">EnglishLayoutLevel Id</param>
        /// <param name="level">EnglishLayoutLevel entity for update</param>
        /// <returns></returns>
        public async Task UpdateLevelAsync(int id, EnglishLayoutLevel level)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync<EnglishLayoutLevel>($"{_apiKey}/{id}/{level}", level);
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {

            }
            
        }

        /// <summary>
        /// Send a request to server to try add new EnglishLayoutLesson entity
        /// </summary>
        /// <param name="newLevel">Entity of EnglishLayoutLesson</param>
        /// <returns></returns>
        public async Task AddNewLevelAsync(EnglishLayoutLevel newLevel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiKey}", newLevel);
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
