using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Providers;
using Microsoft.VisualBasic.ApplicationServices;
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
        private static List<EducationUsersProgress> _updatedEducationUsersProgressCollection;
        static EducationUsersProgressService()
        {
            _apiClient = DbApiClientProvider.EducationUserProgressApiClient;
            _addedNewEducationUsersProgressCollection = new List<EducationUsersProgress>(100);
            _updatedEducationUsersProgressCollection = new List<EducationUsersProgress>(100);

        }
        /// <summary>
        /// Async initializing EducationUserProgress collection 
        /// </summary>
        /// <param name="userId">User id which EducationUserProgress collection we want get</param>
        /// <returns></returns>
        public static async Task InitEducationUserProgressCollection(int userId)
        {
            _educationUserProgressesCollection = (await _apiClient.GetEducationUsersProgressesByUserIdAsync(userId))?.ToList();
        }

        /// <summary>
        /// This methor return EducationUsersProgress collection 
        /// </summary>
        /// <param name="userId">User id </param>
        /// <returns></returns>
        public static async Task<IEnumerable<EducationUsersProgress>?> GetEducationUsersProgressesByUserIdAsync(int userId)
        {
            _educationUserProgressesCollection ??= (await _apiClient.GetEducationUsersProgressesByUserIdAsync(userId))?.ToList();
            return _educationUserProgressesCollection;
        }

        /// <summary>
        /// Delegating adding operation of new EducationUsersProgress to Api client 
        /// </summary>
        /// <param name="educationUsersProgress">Created EducationUsersProgress entity</param>
        /// <returns></returns>
        public static async Task AddNewEducationUsersProgressAsync(EducationUsersProgress educationUsersProgress)
        {
            _apiClient.AddEducationUsersProgressAsync(educationUsersProgress);
        }

        /// <summary>
        /// Delegeting adding and updating operations to ApiClient 
        /// </summary>
        /// <returns></returns>
        public static async Task SaveAddedEducationUsersResultAsync()
        {
            try
            {
                if (_addedNewEducationUsersProgressCollection is not null && _addedNewEducationUsersProgressCollection.Count != 0)
                {

                    var addTask = _apiClient.AddEductionUsersProgressRangeAsync(_addedNewEducationUsersProgressCollection);
                    Task updateTask = Task.CompletedTask;
                    if (_updatedEducationUsersProgressCollection is not null && _updatedEducationUsersProgressCollection.Count != 0)
                    {
                        updateTask = _apiClient.UpdateRangeEducationUsersProgress(_updatedEducationUsersProgressCollection);
                    }

                    await Task.WhenAll(addTask, updateTask);
                }
                else if (_updatedEducationUsersProgressCollection is not null && _updatedEducationUsersProgressCollection.Count != 0)
                {
                    await _apiClient.UpdateRangeEducationUsersProgress(_updatedEducationUsersProgressCollection);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Adding new created EducationUsersProgress in local collections
        /// </summary>
        /// <param name="newEducationUsersProgress">Created EducationUsersProgress entity</param>
        public static void AddNewEducationUsersProgressLocal(EducationUsersProgress newEducationUsersProgress)
        {
            _addedNewEducationUsersProgressCollection.Add(newEducationUsersProgress);
            _educationUserProgressesCollection.Add(newEducationUsersProgress);
        }

        /// <summary>
        /// Get EducaionUsersProgress which comes after current one
        /// </summary>
        /// <param name="currentProgress">Current EducationUsersProgress entity</param>
        /// <returns> Next EducatoinUsersProgress entity or NULL</returns>
        public static EducationUsersProgress? GetNextEducationProgress(EducationUsersProgress currentProgress)
        {
            return _educationUserProgressesCollection.Where(oneProg => oneProg.Id > currentProgress.Id)?.OrderBy(oneProg => oneProg.Id).FirstOrDefault();
        }

        /// <summary>
        /// Get Id of last EducationUsersProgress in local collection
        /// </summary>
        /// <returns>Id of last EducationUsersProgress in local collection</returns>
        public static int GetIdOfLastEducationProgressElement()
        {
            return _educationUserProgressesCollection?.LastOrDefault()?.Id ?? 0;
        }

        /// <summary>
        /// Get EducationUsersProgress entity by lesson id 
        /// </summary>
        /// <param name="lessonId">EnglisLayoutLesson Id</param>
        /// <returns>EducationUsersProgress entity or NULL</returns>
        public static EducationUsersProgress? GetEducationProgressByLessonId(int lessonId)
        {
            try
            {
                return _educationUserProgressesCollection.FirstOrDefault(oneEducProgress => oneEducProgress.EnglishLayoutLessonId == lessonId
                && oneEducProgress.UserId == KeyboardAppEducationProgressController.CurrentUser.Id);
                
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// Update local EducationUsersProgress entity
        /// </summary>
        /// <param name="isLessCompleted">Is user completed LessThanTwoMissclick condition</param>
        /// <param name="isWithoutMistakeCompleted">Is user completed WhithoutMistate condition</param>
        /// <param name="isSpeedCompleted">Is user completed Speed condition</param>
        public static void UpdateEducationUserProgressLocal(EducationUsersProgress updatedEducationUsersProgress, bool isLessCompleted, bool isWithoutMistakeCompleted, bool isSpeedCompleted)
        {
            var currentEducationUsersProgress = _educationUserProgressesCollection.FirstOrDefault(oneEducProg => oneEducProg.Id == updatedEducationUsersProgress.Id);
            if (currentEducationUsersProgress is not null)
            {
                if (isLessCompleted)
                {
                    currentEducationUsersProgress.IsLessThanTwoErrorsCompleted = isLessCompleted;
                }
                if (isWithoutMistakeCompleted)
                {
                    currentEducationUsersProgress.IsWithoutErrorsCompleted = isWithoutMistakeCompleted;
                }
                if (isSpeedCompleted)
                {
                    currentEducationUsersProgress.IsSpeedCompleted = isSpeedCompleted;
                }
                if (!_addedNewEducationUsersProgressCollection.Contains(currentEducationUsersProgress))
                    _updatedEducationUsersProgressCollection.Add(currentEducationUsersProgress);
            }

        }
        public static int GetEducationUsersProgressesCount() => _educationUserProgressesCollection.Count;
        public static bool IsAllPerfectlyCompleted() => _educationUserProgressesCollection.All(oneEducProg => oneEducProg.IsSpeedCompleted && oneEducProg.IsWithoutErrorsCompleted && oneEducProg.IsLessThanTwoErrorsCompleted);
    }
}
