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
        public EnlishEducationalProgram GetEducationalProgram()
        {
            return new EnlishEducationalProgram() { Levels = GetEducationLevelList(), CurrentLessonId = 5 };
        }
        public List<EducationLevel> GetEducationLevelList()
        {
            return new List<EducationLevel>() { new EducationLevel() {
                Title="First lesson",
                Ordinal=1,
                Id=1,
                LessonsList=CreateLessonList()
                
            },new EducationLevel()
            {
                Title="SeconLesson",
                Ordinal=2,
                Id=2,
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
                    Ordinal = i + 1,
                    LessonText = $"Lesson {i + 1} text",
                    IsLessonUnlocked = i < 5,
                    IsLessTwoErrorsCompleted = i == 2,
                    IsWithoutErrorsCompleted = i < 3,
                    IsSpeedConditionCompleted = true
                };

                lessons.Add(lesson);
            }

            return lessons;
        }
    }
}
