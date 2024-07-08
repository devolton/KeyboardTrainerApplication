using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Models
{
    public class EnglishTypingTestTextModel:BaseTypingTutorModel
    {
        private ObservableCollection<EnglishTypingTestText> _englishTypingTestTexts;
        public EnglishTypingTestTextModel()
        {
            _context.EnglishTypingTestTexts.Load();
            _englishTypingTestTexts = _context.EnglishTypingTestTexts.Local;
        }
        public void AddNewText(string text)
        {
            _englishTypingTestTexts.Add(new EnglishTypingTestText
            {
                Text = text
            });
        }
        
        public IEnumerable<EnglishTypingTestText> GetAllTexts() {
            return _englishTypingTestTexts;
        }
        public EnglishTypingTestText? GetTextById(int id)
        {
            return _englishTypingTestTexts.FirstOrDefault(oneText => oneText.Id == id);
        }
    }
}
