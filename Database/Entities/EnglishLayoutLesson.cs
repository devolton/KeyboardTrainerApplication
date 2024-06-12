using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Entities
{
    [Table("englishLayoutLessons")]
    public class EnglishLayoutLesson
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        [Column("text")]
        public string Text {  get; set; }
        [Required]
        [Column("ordinal")]
        public int Ordinal { get; set; }
        [Required]
        [Column("english_layout_level_id")]
        [ForeignKey(nameof(EnglishLayoutLevel))]
        public int EnglishLayoutLevelId { get; set; }
        public EnglishLayoutLevel EnglishLayoutLevel { get; set; } = null;
    }
}
