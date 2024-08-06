using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProjectKeyboardApplication.Shared.Services
{
    public static class EducationUsersProgressService
    {
        private static EducationUserProgressApiClient _apiClient;
        private static List<EducationUsersProgress> _educationUserProgressesCollection;
        private static List<EducationUsersProgress> _addedNewEducationUsersProgressCollection;
        static EducationUsersProgressService()
        {
            _apiClient = ApiClientProvider.EducationUserProgressApiClient;
            _addedNewEducationUsersProgressCollection = new List<EducationUsersProgress>(100);

        }
        public static async Task<IEnumerable<EducationUsersProgress>?> GetEducationUsersProgressesByUserIdAsync(int userId)
        {
            _educationUserProgressesCollection ??= (await _apiClient.GetEducationUsersProgressesByUserIdAsync(userId))?.ToList();
            return _educationUserProgressesCollection;
        }
        public static async Task AddNewEducationUsersProgressAsync(EducationUsersProgress educationUsersProgress)
        {
            _apiClient.AddEducationUsersProgressAsync(educationUsersProgress);
        }
        public static async Task SaveAddedEducationUsersResultAsync()
        {
            if (_addedNewEducationUsersProgressCollection is not null && _addedNewEducationUsersProgressCollection.Count != 0)
               await _apiClient.AddEductionUsersProgressRangeAsync(_addedNewEducationUsersProgressCollection);
        }
        public static void AddNewEducationUsersProgressLocal(EducationUsersProgress newEducationUsersProgress)
        {
            _addedNewEducationUsersProgressCollection.Add(newEducationUsersProgress);
            _educationUserProgressesCollection.Add(newEducationUsersProgress);
        }
        public static EducationUsersProgress? GetNextEducationProgress(EducationUsersProgress currentProgress)
        {
            return _educationUserProgressesCollection.Where(oneProg => oneProg.Id > currentProgress.Id)?.OrderBy(oneProg => oneProg.Id).FirstOrDefault();
        }
        public static int GetIdOfLastEducationProgressElement()
        {
            return _educationUserProgressesCollection.LastOrDefault()?.Id ?? 0;
        }
        public static EducationUsersProgress? GetEducationProgressByLessonId(int lessonId)
        {
            try
            {
                var result = _educationUserProgressesCollection.FirstOrDefault(oneEducProgress => oneEducProgress.EnglishLayoutLessonId == lessonId
                && oneEducProgress.UserId == UserController.CurrentUser.Id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
