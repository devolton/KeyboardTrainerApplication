using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingTutorResultPageModel
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
        
        public TypingTutorResultPageModel()
        {
            _lessTwoMistakeText = $"less than {_LESS_TWO_DELIMITER} typos";
            _withoutMistakeText = "exercise without typos";
            _speedText = $"speed more than {_SPEED_DELIMITER} wpm";
            UserController.InitLessons();
        }

        /// <summary>
        /// Init typing tutor result data
        /// </summary>
        public void InitData() // maybe raname
        {
            _misclickCount = TypingTutorResultController.MisclickCount;
            _typingTutorSpeed = TypingTutorResultController.TypingTutorSpeed;
          
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
            return true;// придумать проверку
        }
        public void UpdateLessonData()
        {
            bool isLessTwoCompleted = IsExecuteLessTwoErrorCondition();
            bool isWithoutMistakeCompleted = IsExecuteWithoutMisclickCondition();
            bool isSpeedCompleted = IsExecuteSpeedCondition();
            if(UserController.CurrentUserEducationProgress is null)
            {
                UserController.CurrentUserEducationProgress=UserController.CreateNewEducationUsersProgresses();
                UserController.ChangeCurrentUserLesson();

            }
            UserController.UpdateCurrentEducationUserProgress(isLessTwoCompleted, isWithoutMistakeCompleted,isSpeedCompleted);
            

           
        }
        /// <summary>
        /// Set Current EducationUserProgress to next 
        /// </summary>
        public void SetNextEducationUserProgress() // maybe rename
        {
            UserController.SetNextEducationUserProgeress();

        }

        public string GetLessTwoMistakeText() => _lessTwoMistakeText;
        public string GetWithoutMistakeText() => _withoutMistakeText;
        public string GetSpeedText() => _speedText;
        public string GetLessonResultStr() => _resultStr;

    }
}
