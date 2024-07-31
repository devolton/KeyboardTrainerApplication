using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _apiClient = ApiClientProvider.EnglishLayoutLessonApiClient;

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
            }
        }

    }
}
