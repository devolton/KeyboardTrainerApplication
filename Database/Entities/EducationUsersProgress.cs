using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Entities
{
    public class EducationUsersProgress
    {
        public int Id { get; set; }

        public bool IsLessThanTwoErrorsCompleted { get; set; } = false;

        public bool IsWithoutErrorsCompleted { get; set; } = false;

        public bool IsSpeedCompleted { get; set; } = false;

        public int UserId { get; set; }

        public int EnglishLayoutLevelId { get; set; }

        public int EnglishLayoutLessonId { get; set; }

        public virtual User User { get; set; } = null;
        public virtual EnglishLayoutLesson EnglishLayoutLesson { get; set; } = null;
        public virtual EnglishLayoutLevel EnglishLayoutLevel { get; set; } = null;

        public override string ToString()
        {
            return $"[Id]: {Id}\n\t[UserId]: {UserId}\n\t[EnglishLayoutLevelId]: {EnglishLayoutLevelId}\n\t" +
                $"[EnglishLayoutLessonId]: {EnglishLayoutLessonId}\n\t[IsLessThanTwoErrorCompleted]: {IsLessThanTwoErrorsCompleted}\n\t" +
                $"[IsWithoutErrorCompleted: {IsWithoutErrorsCompleted}\n\t[IsSpeedComleted]: {IsSpeedCompleted}";
        }

    }
}
