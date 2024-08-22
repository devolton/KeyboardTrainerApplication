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
            _apiClient = DbApiClientProvider.TypingTestResultApiClient;
            _addedTypingTestResultCollection = new List<TypingTestResult>(50);
        }
        /// <summary>
        /// Return collectoin of TypingTestResults by User id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Collection of TypingTestResult</returns>
        public static async Task<IEnumerable<TypingTestResult>?> GetTypingTestResultsByUserIdAsync(int userId)
        {
            _typingTestResultCollecton ??= (await _apiClient.GetTestsByUserIdAsync(userId)).ToList();
            return _typingTestResultCollecton;
        }

        /// <summary>
        /// Adding new created TypingTestResult entity in local collection
        /// </summary>
        /// <param name="typingTestResult">Created TypingTestResult</param>
        public static void AddNewTypingTestLocal(TypingTestResult typingTestResult)
        {
            _typingTestResultCollecton.Add(typingTestResult);
            _addedTypingTestResultCollection.Add(typingTestResult);
        }
        /// <summary>
        /// Return best TypingTestResult of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static TypingTestResult? GetBestUserTestAsync(int userId)
        {
            return _typingTestResultCollecton.Where(oneResult => oneResult.UserId.Equals(userId))?.OrderByDescending(oneResult => oneResult.Speed).FirstOrDefault();
        }

        /// <summary>
        /// Delegating adding operation 
        /// </summary>
        /// <param name="newTypingTestResult">Created TypingTestResult entity</param>
        /// <returns></returns>
        public static async Task AddNewTypingTestAsync(TypingTestResult newTypingTestResult)
        {
            _apiClient.AddNewTypingTestResultAsync(newTypingTestResult);
        }

        /// <summary>
        /// Delegating save new TypingTestResult collection in remote database oparation 
        /// </summary>
        /// <returns></returns>
        public static async Task SaveNewTypingTestAsync()
        {
            
                await _apiClient.AddRangeTypingTestsAsync(_addedTypingTestResultCollection);
        }

    }
}
