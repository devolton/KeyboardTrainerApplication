using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Services
{
    public static class UserService
    {
        private static UserApiClient _apiClient;
        static UserService()
        {
            _apiClient = ApiClientProvider.UserApiClient;
        }
        public static async  Task<bool> IsUserExistByLoginOrEmail(string loginOrEmail)
        {
            return (await _apiClient.IsUserExistByEmailAsync(loginOrEmail)) || (await _apiClient.IsUserExistByLoginAsync(loginOrEmail));
        }
        public static async Task<User?> GetUserByLoginOrEmailAndPasswordAsync(string loginOrEmail, string encryptSha256Password)
        {
            return await _apiClient.GetUserByLoginOrEmailAndPasswordAsync(loginOrEmail, encryptSha256Password);
        }
        public static async Task<bool> IsUniqueEmailAsync(string email)
        {
            return await _apiClient.IsUniqueEmailAsync(email);
        }
        public static async Task<bool> IsUniqueLoginAsync(string login)
        {
            return await _apiClient.IsUniqueLoginAsync(login);
        }
        public static async Task UpdateUser(User user)
        {
            await _apiClient.UpdateUserAsync(user);
        }
        public static async Task AddNewUserAsync(User newUser)
        {
            await _apiClient.AddNewUserAsync(newUser);
        }
    }
}
