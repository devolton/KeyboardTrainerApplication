﻿using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Mediators;


namespace CourseProjectKeyboardApplication.Model
{
    public class EducationResultsPageModel
    {

        private int _commonLevelCount;
        private string _languageLayoutType = "English layout";
        private EducationUserProgressModel _educationUserProgressModel;
        private EnglishLayoutLevelModel _englishLayoutLevelModel;
        private User _currentUser;
        private IEnumerable<EnglishLayoutLevel> _englishLayoutLevelsCollection;

       
        public EducationResultsPageModel()
        {
            _educationUserProgressModel = DatabaseModelMediator.EducationUserProgressModel;
            _englishLayoutLevelModel= DatabaseModelMediator.EnglishLayoutLevelModel;
            _currentUser = UserController.CurrentUser;
            GetLevels();
            InitAllLevelCount();
 
        }
        /// <summary>
        /// Get actual user education progress str(current level and common levels count) to educationResultsPage header
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLevelHeaderStr()
        {
            
            int currentLevelCounter = UserController.CurrentUser.EnglishLayoutLevel.Ordinal;
            return $"Level {currentLevelCounter} from {_commonLevelCount}";
        }
        /// <summary>
        /// Get actual user education progress percent of completed lessons count
        /// </summary>
        /// <returns></returns>
        public double GetPercentOfCompletedLessons()
        {
            var completedLessonCount = _educationUserProgressModel.GetUsersEducationProgress(_currentUser.Id).Count();
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
