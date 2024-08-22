using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Providers;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CourseProjectKeyboardApplication.ApiClients
{
    public class UserApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _apiKey;

        public UserApiClient(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions = null)
        {
            _httpClient = httpClient;
            _jsonOptions = jsonSerializerOptions;
            _apiKey = "User";
        }

        /// <summary>
        /// Send a request to try get User entity by login or email and password and JWT-token if user with current credentials
        /// </summary>
        /// <param name="loginOrEmail">Login or email</param>
        /// <param name="shaPassword">password which is encrypted by SHA-256</param>
        /// <returns>User entity or NULL</returns>
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

            }
            catch (Exception ex)
            {
                
            }
            return null;
        }




        /// <summary>
        /// Send a request to server to try get User by Id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User entity or NULL</returns>
        public async Task<User?> GetUserByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonStr = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<User>(jsonStr, _jsonOptions);
                    return user;
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        /// <summary>
        /// Send a request to server and try create new User. If user create successfully, method return User entity and initialize JWT token
        /// </summary>
        /// <param name="newUser">User entity which we want add</param>
        /// <returns>User entity or NULL</returns>
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
                    var userTokenPair = JsonSerializer.Deserialize<KeyValuePair<User, string?>>(jsonStr, _jsonOptions);
                    string? token = userTokenPair.Value;
                    User? user = userTokenPair.Key;
                    DbApiClientProvider.InitJwtToken(token);
                    return user;
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        /// <summary>
        /// Send a reques to server to try update existing User
        /// </summary>
        /// <param name="user">User entity which we want update</param>
        /// <returns>Success operation code(1-success, 0 - failed )</returns>
        public async Task<int> UpdateUserAsync(User user)
        {
            var successCode = 0;
            try
            {

                var jsonContent = JsonSerializer.Serialize(user, _jsonOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiKey}/{user.Id}", content);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    ++successCode;
                }
            }
            catch (Exception ex)
            {

            }
            return successCode;

        }

        /// <summary>
        /// Send a request to remove existing user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Successs operation code(1 - success, 0 - failed) </returns>
        public async Task<int> RemoveUserAsync(int id)
        {
            int successCode = 0;
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiKey} /{id}");

                if (response.IsSuccessStatusCode)
                {
                    successCode++;
                }

            }
            catch (Exception ex)
            {

            }
            return successCode;
        }

        /// <summary>
        /// Send a request to server to check email for uniqueness
        /// </summary>
        /// <param name="email">Email which we will check for uniqueness</param>
        /// <returns>Is current email unique</returns>
        public async Task<bool> IsUniqueEmailAsync(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/IsUniqueEmail/{email}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        /// <summary>
        /// Send a request to server to check login for uniqueness
        /// </summary>
        /// <param name="login">Login which we will check for uniqueness</param>
        /// <returns>Is current login unique</returns>
        public async Task<bool> IsUniqueLoginAsync(string login)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/IsUniqueLogin/{login}");
                response.EnsureSuccessStatusCode();
                var isExist = await response.Content.ReadFromJsonAsync<bool>();
                return isExist;
            }
            catch(Exception ex)
            {

            }
            return false;
        }
        /// <summary>
        /// Send a reques to server to try check to check if a user with a given email exists and check if there is a connection to the server
        /// </summary>
        /// <param name="email">Email which we will check for existing</param>
        /// <returns>KeyValuePair of bool result (is user with current email exist) and NotifiType(Server connection is ok or no)</returns>
        public async Task<KeyValuePair<bool, NotifyType>> IsUserExistByEmailAsync(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/IsUserExistByEmail/{email}");
                response.EnsureSuccessStatusCode();
                var isExist = await response.Content.ReadFromJsonAsync<bool>();
                return new KeyValuePair<bool, NotifyType>(isExist, NotifyType.ServerOk);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, NotifyType>(false, NotifyType.ServerRequestTimeout);
            }
        }
        /// <summary>
        /// Send a reques to server to try check to check if a user with a given login exists and check if there is a connection to the server
        /// </summary>
        /// <param name="login">Login which we will check for existing</param>
        /// <returns>KeyValuePair of bool result (is user with current login exist) and NotifiType(Server connection is ok or no)</returns>
        public async Task<KeyValuePair<bool, NotifyType>> IsUserExistByLoginAsync(string login)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiKey}/IsUserExistByLogin/{login}");
                response.EnsureSuccessStatusCode();
                bool isExist = await response.Content.ReadFromJsonAsync<bool>();
                return new KeyValuePair<bool, NotifyType>(isExist, NotifyType.ServerOk);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, NotifyType>(false, NotifyType.ServerRequestTimeout);
            }
        }
    }
}
