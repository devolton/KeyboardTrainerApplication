using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Entities
{
    public class EnlishEducationalProgram
    {
        public List<EducationLevel> Levels { get; set; }
        public int CurrentLessonId { get; set; }
        public int CurrentLevelId { get; set; }
    }
}
