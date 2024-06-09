using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingTutorResultPageModel
    {
        private const int _WITHOUT_MISCLICK_DELIMITER = 0;
        private int _lessTwoDelimiter;
        private int _speedDelimiter;
        private int _typingTutorSpeed;
        private int _misclickCount;
        public TypingTutorResultPageModel(int typingTutorSpeed,int misclickCount)
        {
            _typingTutorSpeed=typingTutorSpeed;
            _misclickCount=misclickCount;
            _lessTwoDelimiter = 2;
            _speedDelimiter= 21;
            
        }
        public bool IsExecuteLessTwoErrorCondition()
        {
            return _misclickCount <= _lessTwoDelimiter;
        }
        public bool IsExecuteWithoutMisclickCondition()
        {
            return _misclickCount == _WITHOUT_MISCLICK_DELIMITER;

        }
        public bool IsExecuteSpeedCondition()
        {
            return _typingTutorSpeed <= _speedDelimiter;
        }
        //добавлени в базу данных и возможно обновление зависимых страниц

    }
}
