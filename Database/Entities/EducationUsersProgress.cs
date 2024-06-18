using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Entities
{
    [Table("educationUsersProgresses")]
    public class EducationUsersProgress
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("is_less_than_two_errors_completed")]
        public bool IsLessThanTwoErrorsCompleted { get; set; } = false;

        [Required]
        [Column("is_without_errors_completed")]
        public bool IsWithoutErrorsCompleted { get; set; } = false;

        [Required]
        [Column("id_speed_completed")]
        public bool IsSpeedCompleted { get; set; } = false;

        [Required]
        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required]
        [Column("english_layout_level_id")]
        [ForeignKey(nameof(EnglishLayoutLevel))]
        public int EnglishLayoutLevelId { get; set; }

        [Required]
        [Column("english_layout_lesson_id")]
        [ForeignKey(nameof(EnglishLayoutLesson))]
        public int EnglishLayoutLessonId { get; set; }

        public User User { get; set; } = null;
        public EnglishLayoutLesson EnglishLayoutLesson { get; set; } = null;
        public EnglishLayoutLevel EnglishLayoutLevel { get; set; } = null;

        public override string ToString()
        {
            return $"[Id]: {Id}\n\t[UserId]: {UserId}\n\t[EnglishLayoutLevelId]: {EnglishLayoutLevelId}\n\t" +
                $"[EnglishLayoutLessonId]: {EnglishLayoutLessonId}\n\t[IsLessThanTwoErrorCompleted]: {IsLessThanTwoErrorsCompleted}\n\t" +
                $"[IsWithoutErrorCompleted: {IsWithoutErrorsCompleted}\n\t[IsSpeedComleted]: {IsSpeedCompleted}";
        }

    }
}
