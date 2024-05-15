using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Entities
{
    public class EducationLesson
    {
        public int Id { get; set; } 
        public string LessonText {  get; set; }
        public int Ordinal { get; set; }
        public bool IsLessonUnlocked { get; set; }
        public bool IsLessTwoErrorsCompleted { get; set; }
        public bool IsWithoutErrorsCompleted { get; set; }
        public bool IsSpeedConditionCompleted { get; set; }

    }
}
