using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProjectKeyboardApplication.Shared.Services
{
    public static class EnglishLayoutLessonsService
    {
        private static EnglishLayoutLessonApiClient _apiClient;
        private static List<EnglishLayoutLesson> _lessonsCollection;
        static EnglishLayoutLessonsService()
        {
            _apiClient = DbApiClientProvider.EnglishLayoutLessonApiClient;

        }

        /// <summary>
        /// Get EnglishLayoutLesson which comes after current one
        /// </summary>
        /// <param name="currentLesson">Current EnglishLayoutLesson entity</param>
        /// <returns>Next EnglishLayoutLesson</returns>
        public static EnglishLayoutLesson? GetNextLesson(EnglishLayoutLesson currentLesson)
        {
            if (_lessonsCollection is null)
            {
                return null;
            }
            return _lessonsCollection.Where(oneLesson => oneLesson.Id > currentLesson.Id)?.OrderBy(oneLesson => oneLesson.Id).FirstOrDefault();
        }

        /// <summary>
        /// Initializing EnglishLayoutLessons collection
        /// </summary>
        /// <returns></returns>
        public static async Task InitLessonsCollectionAsync()
        {
            if (_apiClient is not null)
            {
                var lessonsCollection = await _apiClient.GetLessonsAsync();
                if (lessonsCollection is not null)
                    _lessonsCollection = lessonsCollection.ToList();
                InitNewUserLesson();
            }
        }

        /// <summary>
        /// Check is current lesson NOT last in collection
        /// </summary>
        /// <param name="englishLayoutLesson">Current EnglishLayoutLesson</param>
        /// <returns></returns>
        public static bool IsLessonNotLast(EnglishLayoutLesson englishLayoutLesson)
        {
            return _lessonsCollection.Last().Id != englishLayoutLesson.Id;
        }
        public static int GetLessonsCount() => _lessonsCollection.Count;
        private static EnglishLayoutLesson? GetLessonById(int id)
        {
            return _lessonsCollection.FirstOrDefault(oneLesson => oneLesson.Id == id);
        }
        private static void InitNewUserLesson()
        {
            KeyboardAppEducationProgressController.CurrentLesson ??= (KeyboardAppEducationProgressController.CurrentUser.EnglishLayoutLesson is null)
               ? GetLessonById(KeyboardAppEducationProgressController.CurrentUser.EnglishLayoutLessonId)
               : KeyboardAppEducationProgressController.CurrentUser.EnglishLayoutLesson;


        }

    }
}
