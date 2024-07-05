using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Models
{
    public class EnglishTypingTestTextModel:BaseTypingTutorModel
    {
        private DbSet<EnglishTypingTestText> englishTypingTestTexts;
        public EnglishTypingTestTextModel()
        {
            
        }
    }
}
