
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Managers;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingTutorResultPageModel : ITypingTutorResultPageModel
    {
        private const int _WITHOUT_MISCLICK_DELIMITER = 0;
        private const int _LESS_TWO_DELIMITER = 2;
        private const int _SPEED_DELIMITER = 30;
        private int _typingTutorSpeed;
        private int _misclickCount;
        private string _lessTwoMistakeText;
        private string _withoutMistakeText;
        private string _speedText;
        private string _resultStr = string.Empty;
        private ImageSource _goldStarImageSource;
        private ImageSource _lightGrayStarImageSource;
        private ImageSource _goldTargetImageSource;
        private ImageSource _lightGrayTargetImageSource;
        private ImageSource _goldFlashImageSource;
        private ImageSource _lightGrayFlashImageSource;

        public TypingTutorResultPageModel()
        {
            _lessTwoMistakeText = $"less than {_LESS_TWO_DELIMITER} typos";
            _withoutMistakeText = "exercise without typos";
            _speedText = $"speed more than {_SPEED_DELIMITER} wpm";
            KeyboardAppEducationProgressController.InitLessons();
        }

        /// <summary>
        /// Init typing tutor result data
        /// </summary>
        public void InitData() 
        {
            _misclickCount = TypingTutorResultManager.MisclickCount;
            _typingTutorSpeed = TypingTutorResultManager.TypingTutorSpeed;
          
            _resultStr=$"{_typingTutorSpeed} wpm, {_misclickCount} errors!";
        }
        /// <summary>
        /// Check is LessTwoErrors condition completed
        /// </summary>
        /// <returns></returns>
      
        public bool IsExecuteLessTwoErrorCondition()
        {   
            return _misclickCount <= _LESS_TWO_DELIMITER;
        }
        /// <summary>
        /// Check is without mistake condition completed 
        /// </summary>
        /// <returns></returns>
        public bool IsExecuteWithoutMisclickCondition()
        {
            return _misclickCount == _WITHOUT_MISCLICK_DELIMITER;

        }
        /// <summary>
        /// Check is speed condition completed
        /// </summary>
        /// <returns></returns>
        public bool IsExecuteSpeedCondition()
        {
           
            return _typingTutorSpeed >= _SPEED_DELIMITER;
        }
        /// <summary>
        /// Update lessons EducationUsersProgresses object 
        /// </summary>
        
        public bool IsCurrentLessonNotLast()
        { 
            return (KeyboardAppEducationProgressController.CurrentLesson is not null) ? EnglishLayoutLessonsService.IsLessonNotLast(KeyboardAppEducationProgressController.CurrentLesson) : true;
        }
        public void UpdateLessonData()
        {
            bool isLessTwoCompleted = IsExecuteLessTwoErrorCondition();
            bool isWithoutMistakeCompleted = IsExecuteWithoutMisclickCondition();
            bool isSpeedCompleted = IsExecuteSpeedCondition();
            if(KeyboardAppEducationProgressController.CurrentUserEducationProgress is null)
            {
                KeyboardAppEducationProgressController.CurrentUserEducationProgress=KeyboardAppEducationProgressController.CreateNewEducationUsersProgresses();
                KeyboardAppEducationProgressController.ChangeCurrentUserLesson();

            }
            EducationUsersProgressService.UpdateEducationUserProgressLocal(KeyboardAppEducationProgressController.CurrentUserEducationProgress,isLessTwoCompleted, isWithoutMistakeCompleted, isSpeedCompleted);
            

           
        }
        /// <summary>
        /// Set Current EducationUserProgress to next 
        /// </summary>
        public void SetNextEducationUserProgress() 
        {
            KeyboardAppEducationProgressController.SetNextEducationUserProgeress();

        }

        public string GetLessTwoMistakeText() => _lessTwoMistakeText;
        public string GetWithoutMistakeText() => _withoutMistakeText;
        public string GetSpeedText() => _speedText;
        public string GetLessonResultStr() => _resultStr;
        public ImageSource GetGoldStarImageSource()
        {
            return _goldStarImageSource ??= AppImageSourceProvider.GoldStarImageSource;
        }
        public ImageSource GetLightGrayStarImageSource()
        {
            return _lightGrayStarImageSource ??= AppImageSourceProvider.LightGrayStarImageSource;
        }
        public ImageSource GetGoldFlashImageSource()
        {
            return _goldFlashImageSource ??= AppImageSourceProvider.GoldFlashImageSource;
        }
        public ImageSource GetLightGrayFlashImageSource()
        {
            return _lightGrayFlashImageSource ??= AppImageSourceProvider.LightGrayFlashImageSource;
        }
        public ImageSource GetGoldTargetImageSource()
        {
            return _goldTargetImageSource ??= AppImageSourceProvider.GoldTargetImageSource;

        }
        public ImageSource GetLightGrayTargetImageSource()
        {
            return _lightGrayTargetImageSource ??= AppImageSourceProvider.LightGrayTargetImageSource;
        }

    }
}
