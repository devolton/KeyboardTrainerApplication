using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Entities
{

    public class EnglishLayoutLevel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public int Ordinal { get; set; }
        public virtual ICollection<EnglishLayoutLesson> Lessons { get; set; }
        public virtual ICollection<User> Users { get; set; } = null;
  
    }
}
