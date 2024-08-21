using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Providers;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProjectKeyboardApplication.ApiClients
{
    public class UserApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _apiKey;

        public UserApiClient(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions=null)
        {
            _httpClient = httpClient;
            _jsonOptions = jsonSerializerOptions;
            _apiKey = "User";
        }

        // Метод для получения пользователя по логину/емейлу и паролю
        public async Task<User?> GetUserByLoginOrEmailAndPasswordAsync(string loginOrEmail, string shaPassword)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/LoginOrEmailAndPassword?loginOrEmail={loginOrEmail}&shaPassword={shaPassword}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonStr = await response.Content.ReadAsStringAsync();
                    var userTokenPair = JsonSerializer.Deserialize<KeyValuePair<User, string>>(jsonStr, _jsonOptions);
                    User? user = userTokenPair.Key;
                    string? token = userTokenPair.Value;
                    DbApiClientProvider.InitJwtToken(token); //инициализируем токен 
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
            catch(Exception ex)
            {
                return null;
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
        public async Task<User?> AddNewUserAsync(User newUser)
        {
            try
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    MaxDepth = 64,
                };

                var jsonContent = JsonSerializer.Serialize(newUser, jsonOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiKey}", content);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var jsonStr = await response.Content.ReadAsStringAsync();
                    var userTokenPair = JsonSerializer.Deserialize<KeyValuePair<User,string?>>(jsonStr, _jsonOptions);
                    string? token = userTokenPair.Value;
                    User? user = userTokenPair.Key;
                    DbApiClientProvider.InitJwtToken(token);
                    return user;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return null;

        }


        public async Task<int> UpdateUserAsync(User user)
        {
            var successCode = 0;
            var jsonContent = JsonSerializer.Serialize(user, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiKey}/{user.Id}", content);
            response.EnsureSuccessStatusCode();
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
        public async Task<KeyValuePair<bool,NotifyType>> IsUserExistByEmailAsync(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/IsUserExistByEmail/{email}");
                response.EnsureSuccessStatusCode();
                var isExist = await response.Content.ReadFromJsonAsync<bool>();
                return new KeyValuePair<bool, NotifyType>(isExist, NotifyType.ServerOk);
            }
            catch(Exception ex)
            {
                return new KeyValuePair<bool, NotifyType>(false, NotifyType.ServerRequestTimeout);
            }
        }
        public async Task<KeyValuePair<bool,NotifyType>> IsUserExistByLoginAsync(string login)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/IsUserExistByLogin/{login}");
                response.EnsureSuccessStatusCode();
                bool isExist = await response.Content.ReadFromJsonAsync<bool>();
                return new KeyValuePair<bool, NotifyType>(isExist, NotifyType.ServerOk);
            }
            catch(Exception ex)
            {
                return new KeyValuePair<bool, NotifyType>(false, NotifyType.ServerRequestTimeout);
            }
        }
    }
}
