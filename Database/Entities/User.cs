using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }


        public string Password { get; set; } //sha256

        public string AvatarPath { get; set; } = string.Empty;

        public int EnglishLayoutLevelId { get; set; }

        public int EnglishLayoutLessonId { get; set; }
        public virtual EnglishLayoutLevel EnglishLayoutLevel { get; set; } = null;
        public virtual EnglishLayoutLesson EnglishLayoutLesson { get; set; } = null;
        public virtual ICollection<TypingTestResult> TypingTestResults { get; set; } = null;
        public virtual ICollection<EducationUsersProgress> EducationUsersProgresses { get; set; } = null;
       




    }
}
