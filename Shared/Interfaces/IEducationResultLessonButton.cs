using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Interfaces
{
    public interface IEducationResultLessonButton
    {
        EnglishLayoutLesson EducationLesson { get; set; }
        string LessonNumber { get; set; }
    }
}
