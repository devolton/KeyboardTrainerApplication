using CourseProjectKeyboardApplication.Database.Context;
using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Models
{
    public class EnglishLayoutLevelModel:BaseTypingTutorModel
    {
        private DbSet<EnglishLayoutLevel> _englishLayoutLevels;
        public EnglishLayoutLevelModel()
        {
            _englishLayoutLevels = _context.EnglishLayoutLevels;   
        }
        public IEnumerable<EnglishLayoutLevel> GetLevels()
        {
            return _englishLayoutLevels;
        }
        public void AddLevel(EnglishLayoutLevel level)
        {
            _englishLayoutLevels.Add(level);
        }
        public int ChangeLevelTitle(string newTitle, int levelId)
        {
            int successOparationCode = 0;
            var level = _englishLayoutLevels.FirstOrDefault(oneLevel => oneLevel.Id == levelId);
            if(level != null)
            {
                level.Title = newTitle;
                successOparationCode++;

            }
            return successOparationCode;
        }
        public int ChangeLevelOrdinal(int newOrdinal, int levelId)
        {
            int successOparationCode = 0;
            var level = _englishLayoutLevels.FirstOrDefault(oneLevel => oneLevel.Id == levelId);
            if(level != null)
            {
                level.Ordinal = newOrdinal;
                successOparationCode++;
            }
            return successOparationCode;
        }
    }
}
