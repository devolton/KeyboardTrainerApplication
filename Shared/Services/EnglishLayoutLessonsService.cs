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
        public static EnglishLayoutLesson? GetNextLesson(EnglishLayoutLesson currentLesson)
        {
            if (_lessonsCollection is null)
            {
                return null;
            }
            return _lessonsCollection.Where(oneLesson => oneLesson.Id > currentLesson.Id)?.OrderBy(oneLesson => oneLesson.Id).FirstOrDefault();
        }
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
        private static EnglishLayoutLesson? GetLessonById(int id)
        {
            return _lessonsCollection.FirstOrDefault(oneLesson => oneLesson.Id == id);
        }
        private static void InitNewUserLesson()
        {
            UserController.CurrentLesson ??= (UserController.CurrentUser.EnglishLayoutLesson is null)
               ? GetLessonById(UserController.CurrentUser.EnglishLayoutLessonId)
               : UserController.CurrentUser.EnglishLayoutLesson;


        }

    }
}
