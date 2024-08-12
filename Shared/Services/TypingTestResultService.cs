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
        private static List<TypingTestResult> _typingTestResultCollecton;
        private static List<TypingTestResult> _addedTypingTestResultCollection;
        static TypingTestResultService()
        {
            _apiClient = ApiClientProvider.TypingTestResultApiClient;
            _addedTypingTestResultCollection = new List<TypingTestResult>(50);
        }
        public static async Task<IEnumerable<TypingTestResult>?> GetTypingTestResultsByUserIdAsync(int userId)
        {
            _typingTestResultCollecton ??= (await _apiClient.GetTestsByUserIdAsync(userId)).ToList();
            return _typingTestResultCollecton;
        }
        public static void AddNewTypingTestLocal(TypingTestResult typingTestResult)
        {
            _typingTestResultCollecton.Add(typingTestResult);
            _addedTypingTestResultCollection.Add(typingTestResult);
        }
        public static TypingTestResult? GetBestUserTestAsync(int userId)
        {
            return _typingTestResultCollecton.Where(oneResult => oneResult.UserId.Equals(userId))?.OrderByDescending(oneResult => oneResult.Speed).FirstOrDefault();
        }
        public static async Task AddNewTypingTestAsync(TypingTestResult newTypingTestResult)
        {
            _apiClient.AddNewTypingTestResultAsync(newTypingTestResult);
        }
        public static async Task SaveNewTypingTestAsync()
        {
            //if (_addedTypingTestResultCollection is not null && _addedTypingTestResultCollection.Count != 0)
                await _apiClient.AddRangeTypingTestsAsync(_addedTypingTestResultCollection);
        }

    }
}
