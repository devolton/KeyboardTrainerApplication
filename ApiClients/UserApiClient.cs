using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
namespace CourseProjectKeyboardApplication.ApiClients
{
    public class UserApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _apiKey;

        public UserApiClient(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions)
        {
            _httpClient = httpClient;
            _jsonOptions = jsonSerializerOptions;
            _apiKey = "User";
        }

        // Метод для получения пользователя по логину/емейлу и паролю
        public async Task<User?> GetUserByLoginOrEmailAndPasswordAsync(string loginOrEmail, string shaPassword)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/LoginOrEmailAndPassword?loginOrEmail={loginOrEmail}&shaPassword={shaPassword}");

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(jsonStr, _jsonOptions);
                return user;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
            }
        }


       

        // Метод для получения пользователя по ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(jsonStr, _jsonOptions);
                return user;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
            }
        }

        // Метод для добавления нового пользователя
        public async Task AddNewUserAsync(User newUser)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiKey}", newUser);

            response.EnsureSuccessStatusCode();
        }


        public async Task<int> UpdateUserAsync(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiKey}/{user.Id}", user);

            var successCode = 0;
            if (response.IsSuccessStatusCode)
            {
                ++successCode;
            }
            return successCode;
        }

        // Метод для удаления пользователя
        public async Task<int> RemoveUserAsync(int id)
        {
            int successCode = 0;
            var response = await _httpClient.DeleteAsync($"{_apiKey} /{id}");

            if (response.IsSuccessStatusCode)
            {
                successCode++;
            }
            return successCode;
        }

        // Метод для проверки уникальности email
        public async Task<bool> IsUniqueEmailAsync(string email)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/IsUniqueEmail/{email}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
            }
        }

        // Метод для проверки уникальности логина
        public async Task<bool> IsUniqueLoginAsync(string login)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/IsUniqueLogin/{login}");
            response.EnsureSuccessStatusCode();
            var isExist = await response.Content.ReadFromJsonAsync<bool>();
            return isExist;
        }
        public async Task<bool> IsUserExistByEmailAsync(string email)
        {
            var response = await _httpClient.GetAsync($"{_apiKey}/IsUserExistByEmail/{email}");
            response.EnsureSuccessStatusCode();
            var isExist = await response.Content.ReadFromJsonAsync<bool>();
            return isExist;
        }
    }
}
