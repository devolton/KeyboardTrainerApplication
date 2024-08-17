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
    public class TypingTestResult
    {
        public int Id { get; set; }
        public int Speed { get; set; }
        public double AccuracyPercent { get; set; }
        public LayoutType LayoutType { get; set; }
        public DateTime Date { get; set;}
        public int UserId { get; set; }
        public virtual User User { get; set; } = null;
        public override string ToString()
        {
            return $"Id: {Id}\n\tSpeed: {Speed}\n\tAccuracyPercent: {AccuracyPercent}\n\tLayoutType:{LayoutType}\n\tDate: {Date}\n\tUserId:{UserId}";
        }
    }
}
