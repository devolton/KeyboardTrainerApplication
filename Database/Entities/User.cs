using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [MinLength(2)]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [Column("login")]
        [MinLength(3)]
        [StringLength(16)]
        public string Login { get; set; }

        [Required]
        [Column("email")]
        [MinLength(4)]
        [StringLength(42)]
        public string Email { get; set; }

        [Required]
        [Column("password")]

        public string Password { get; set; } //sha256

        [Column("avatar_path")]
        public string AvatarPath { get; set; } = string.Empty;

        [Column("english_layout_level_id")]
        [Required]
        [ForeignKey(nameof(EnglishLayoutLevel))]
        public int EnglishLayoutLevelId { get; set; }

        [Required]
        [Column("english_layout_lesson_id")]
        [ForeignKey(nameof(EnglishLayoutLesson))]
        public int EnglishLayoutLessonId { get; set; }
        public virtual EnglishLayoutLevel EnglishLayoutLevel { get; set; } = null;
        public virtual EnglishLayoutLesson EnglishLayoutLesson { get; set; } = null;
        public virtual ICollection<TypingTestResult> TypingTestResults { get; set; } = null;
        public virtual ICollection<EducationUsersProgress> EducationUsersProgresses { get; set; } = null;
       




    }
}
