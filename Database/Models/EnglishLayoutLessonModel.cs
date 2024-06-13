using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Models
{
    public class EnglishLayoutLessonModel:BaseTypingTutorModel
    {
        private DbSet<EnglishLayoutLesson> _englishLayoutLessons;

        public EnglishLayoutLessonModel()
        {
            _englishLayoutLessons = _context.EnglishLayoutLessons;
        }
        public IEnumerable<EnglishLayoutLesson> GetLessonsByLevelId(int levelId)
        {
            return _englishLayoutLessons.Where(oneLesson=>oneLesson.EnglishLayoutLevelId == levelId);
        }
        public IEnumerable<EnglishLayoutLesson> GetAllLessons()
        {
            return _englishLayoutLessons;
        }
        public int ChangeLessonsText(string newText, int lessonId)
        {
            int successOperationCode = 0;
            var lesson = _englishLayoutLessons.FirstOrDefault(oneLesson => oneLesson.Id == lessonId);
            if(lesson != null)//maybe add length validation
            {
                lesson.Text = newText;
                successOperationCode++;
            }
            return successOperationCode;
        }
        public int ChangeLessonOrdinal(int newOrdinal, int lessonId)
        {
            int successOperationCode = 0;
            var lesson = _englishLayoutLessons.FirstOrDefault(oneLesson => oneLesson.Id == lessonId);
            if(lesson != null)
            {
                lesson.Ordinal = newOrdinal;
                successOperationCode++;
            }
            return successOperationCode;

        }
    }
}
