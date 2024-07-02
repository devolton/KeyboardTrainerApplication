using CourseProjectKeyboardApplication.Shared.Controllers;
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
        private int _lessTwoDelimiter;
        private int _speedDelimiter;
        private int _typingTutorSpeed;
        private int _misclickCount;
        private string _lessTwoMistakeText;
        private string _withoutMistakeText;
        private string _speedText;
        private string _resultStr = string.Empty;
        
        public TypingTutorResultPageModel()
        {
            _lessTwoDelimiter = 2;
            _speedDelimiter= 21;
            _lessTwoMistakeText = "less than 2 typos";
            _withoutMistakeText = "exercise without typos";
            _speedText = "speed more than 21 wpm";
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
            return _misclickCount <= _lessTwoDelimiter;
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
           
            return _typingTutorSpeed <= _speedDelimiter;
        }
        /// <summary>
        /// Update lessons EducationUsersProgresses object 
        /// </summary>
        public void UpdateLessonData()
        {
            UserController.CurrentUserEducationProgress.IsLessThanTwoErrorsCompleted = IsExecuteLessTwoErrorCondition();
            UserController.CurrentUserEducationProgress.IsWithoutErrorsCompleted = IsExecuteWithoutMisclickCondition();
            UserController.CurrentUserEducationProgress.IsSpeedCompleted = IsExecuteSpeedCondition();
            MessageBox.Show("Updated education user progress: " + UserController.CurrentUserEducationProgress.ToString());
        }

        public string GetLessTwoMistakeText() => _lessTwoMistakeText;
        public string GetWithoutMistakeText() => _withoutMistakeText;
        public string GetSpeedText() => _speedText;
        public string GetLessonResultStr() => _resultStr;

    }
}
