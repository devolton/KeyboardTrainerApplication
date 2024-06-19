using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Entities
{
    [Table("englishLayoutLevels")]
    public class EnglishLayoutLevel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Required]
        [Column("title")]
        [StringLength(32)]
        public string Title { get; set; }
        [Required]
        [Column("ordinal")]
        public int Ordinal { get; set; }
        public virtual ICollection<EnglishLayoutLesson> Lessons { get; set; }
        public virtual ICollection<User> Users { get; set; } = null;
  
    }
}
