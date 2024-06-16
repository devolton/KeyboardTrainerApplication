using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Entities
{
    [Table("englishTypingTestText")]
    public class EnglishTypingTestText
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Required]
        [Column("text")]
        public string Text { get; set; }
    }
}
