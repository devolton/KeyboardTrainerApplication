using CourseProjectKeyboardApplication.Entities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Model
{
    public class EducationResultsPageModel
    {
        public EducationResultsPageModel()
        {
            
        }
        public List<EducationLevel> GetEducationLevelList()
        {
            return new List<EducationLevel>() { new EducationLevel() {
                Title="Title lesson",
                Ordinal=1,
                Id=1,
                LessonsList=CreateLessonList()
                
            }
            };
        }
        public  List<EducationLesson> CreateLessonList()
        {
            List<EducationLesson> lessons = new List<EducationLesson>();

            for (int i = 0; i < 19; i++)
            {
                EducationLesson lesson = new EducationLesson
                {
                    Id = i + 1,
                    Ordinal=i+1,
                    LessonText = $"Lesson {i + 1} text",
                    IsLessonUnlocked = i < 5,
                    IsLessTwoErrorsCompleted = false,
                    IsWithoutErrorsCompleted = false,
                    IsSpeedConditionCompleted = false
                };

                lessons.Add(lesson);
            }

            return lessons;
        }
    }
}
