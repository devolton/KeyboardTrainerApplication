using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Entities
{
    public class EnglishLayoutLesson
    {
        public int Id { get; set; }
        public string Text {  get; set; }
        public int Ordinal { get; set; }
        public int EnglishLayoutLevelId { get; set; }
        public virtual EnglishLayoutLevel EnglishLayoutLevel { get; set; } = null;
        public virtual ICollection<User> Users { get; set; } = null;
        public virtual ICollection<EducationUsersProgress> EducationUsersProgresses { get; set; } = null;
    }
}
