using CourseProjectKeyboardApplication.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Entities
{
    [Table("typingTestResults")]
    public class TypingTestResult
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Required]
        [Column("speed")]
        public int Speed { get; set; }
        [Column("accuracy_percent")]
        [Required]
        public double AccuracyPercent { get; set; }
        [Required]
        [Column("layout_type")]
        public LayoutType LayoutType { get; set; }
        [Required]
        [Column("date")]
        public DateTime Date { get; set;}
        [Required]
        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; } = null;
        public override string ToString()
        {
            return $"Id: {Id}\n\tSpeed: {Speed}\n\tAccuracyPercent: {AccuracyPercent}\n\tLayoutType:{LayoutType}\n\tDate: {Date}\n\tUserId:{UserId}";
        }
    }
}
