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

        private int _commonLevelCount;
        private string _languageLayoutType = "English layout";
        private EducationUserProgressModel _educationUserProgressModel;
        private EnglishLayoutLevelModel _englishLayoutLevelModel;
        private UserModel _userModel;
        private IEnumerable<EnglishLayoutLevel> _englishLayoutLevelsCollection;

       
        public EducationResultsPageModel()
        {
            _educationUserProgressModel = DatabaseModelMediator.EducationUserProgressModel;
            _englishLayoutLevelModel= DatabaseModelMediator.EnglishLayoutLevelModel;
            _userModel = DatabaseModelMediator.UserModel;
            GetLevels();
            InitAllLevelCount();
 
        }
        /// <summary>
        /// Get actual user education progress str(current level and common levels count) to educationResultsPage header
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLevelHeaderStr()
        {
            
            int currentLevelCounter = _userModel.GetUserById(1).EnglishLayoutLevel.Ordinal;
            return $"Level {currentLevelCounter} from {_commonLevelCount}";
        }
        /// <summary>
        /// Get actual user education progress percent of completed lessons count
        /// </summary>
        /// <returns></returns>
        public double GetPercentOfCompletedLessons()
        {
            var completedLessonCount = _educationUserProgressModel.GetUsersEducationProgress(1).Count();
            var commonLessonsCount = 0;
            var levelsCollection = _englishLayoutLevelModel.GetLevels();
            foreach(var level in levelsCollection)
            {
                commonLessonsCount += level.Lessons.Count;
            }
            return((double)completedLessonCount / (double)commonLessonsCount) * 100;

        }
        /// <summary>
        /// Get current language layout str 
        /// </summary>
        /// <returns>Percent of completed lessons</returns>
        public string GetLanguageLayoutTypeHeaderStr()
        {
            return _languageLayoutType;
        }
        /// <summary>
        /// Get current english layout levels colletion 
        /// </summary>
        /// <returns>User english layout levels collection</returns>
        public IEnumerable<EnglishLayoutLevel> GetLevels()
        {
           
            _englishLayoutLevelsCollection??= _englishLayoutLevelModel.GetLevels();
            return _englishLayoutLevelsCollection;
        }

        /// <summary>
        /// Initial all level count
        /// </summary>
        private void InitAllLevelCount()
        {
            _commonLevelCount = _englishLayoutLevelsCollection.Count();
        }


       
        
 
    }

}
