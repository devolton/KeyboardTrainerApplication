using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Mediators;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProjectKeyboardApplication.Model
{
    public class EducationResultsPageModel
    {
        private int _currentLevel = 1;//change to data from db
        private int _commonLevelCount= 15; // change data from db
        private double _currentPercentOfCompletedLessons = 19.5;
        private string _languageLayoutType = "English layout";
        private EducationUserProgressModel _educationUserProgressModel;
        private EnglishLayoutLevelModel _englishLayoutLevelModel;
        private TypingTestResultModel _typingTestModel;

       
        public EducationResultsPageModel()
        {
            _educationUserProgressModel = DatabaseModelMediator.EducationUserProgressModel;
            _englishLayoutLevelModel= DatabaseModelMediator.EnglishLayoutLevelModel;
            _typingTestModel = DatabaseModelMediator.TypingTestResultModel;

        }
        /// <summary>
        /// Get actual user education progress str(current level and common levels count) to educationResultsPage header
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLevelHeaderStr()
        {
            return $"Level {_currentLevel} from {_commonLevelCount}";
        }
        /// <summary>
        /// Get actual user education progress percent of completed lessons count
        /// </summary>
        /// <returns></returns>
        public double GetPercentOfCompletedLessons()
        {
            return _currentPercentOfCompletedLessons;
        }
        /// <summary>
        /// Get current language layout str 
        /// </summary>
        /// <returns></returns>
        public string GetLanguageLayoutTypeHeaderStr()
        {
            return _languageLayoutType;
        }
        public IEnumerable<EnglishLayoutLevel> GetLevels()
        {
           
            return _englishLayoutLevelModel.GetLevels();
        }

       
        
 
    }

}
