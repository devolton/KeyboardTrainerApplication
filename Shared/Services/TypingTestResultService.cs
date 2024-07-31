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
    public static class TypingTestResultService
    {
        private static TypingTestResultApiClient _apiClient;
        static TypingTestResultService()
        {
            _apiClient = ApiClientProvider.TypingTestResultApiClient;
        }
        public static async Task<IEnumerable<TypingTestResult>?> GetTypingTestResultsByUserIdAsync(int userId)
        {
            return await _apiClient.GetTestsByUserIdAsync(userId);
        }
        public static async Task<TypingTestResult?> GetBestUserTestAsync(int userId)
        {
            return await _apiClient.GetBestUserTestAsync(userId);
        }
        public static async Task AddNewTypingTestAsync(TypingTestResult newTypingTestResult)
        {
            _apiClient.AddNewTypingTestResultAsync(newTypingTestResult);
        }
    }
}
