using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Entities
{
    public class EducationLevel
    {
        public int Id { get; set; }
        public int Ordinal { get; set;}
        public string Title { get; set; }
        public List<EducationLesson> LessonsList { get; set; }
    }
}
