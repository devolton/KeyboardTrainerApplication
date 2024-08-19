using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Providers;
using Microsoft.Web.WebView2.Core;
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
            _apiClient = DbApiClientProvider.UserApiClient;
        }
        public static async  Task<KeyValuePair<bool,NotifyType>> IsUserExistByLoginOrEmail(string loginOrEmail)
        {
            var emailAndStatusPair = (await _apiClient.IsUserExistByEmailAsync(loginOrEmail));
            var loginAndStatusPair = (await _apiClient.IsUserExistByLoginAsync(loginOrEmail));
            bool isUserExistByEmail = emailAndStatusPair.Key;
            bool isUserExistByLogin = loginAndStatusPair.Key;
            NotifyType serverRequestStatus = (emailAndStatusPair.Value == NotifyType.ServerRequestTimeout || loginAndStatusPair.Value == NotifyType.ServerRequestTimeout) ? NotifyType.ServerRequestTimeout : NotifyType.ServerOk;

            return new KeyValuePair<bool, NotifyType>((isUserExistByEmail || isUserExistByLogin), serverRequestStatus); 
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
        public static async Task<User?> AddNewUserAsync(User newUser)
        {
           return await _apiClient.AddNewUserAsync(newUser);
        }
    }
}
