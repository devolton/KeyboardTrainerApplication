using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingTutorResultPageModel
    {
        private int _typingTutorSpeed;
        private int _misclickCount;
        public TypingTutorResultPageModel(int typingTutorSpeed,int misclickCount)
        {
            _typingTutorSpeed=typingTutorSpeed;
            _misclickCount=misclickCount;
            
        }
        public bool IsExecuteLessTwoErrorCondition()
        {
            return _misclickCount <= 2;
        }
        public bool IsExecuteWithoutMisclickCondition()
        {
            return _misclickCount == 0;

        }
        public bool IsExecuteSpeedCondition()
        {
            return _typingTutorSpeed <= 21;
        }
        //добавлени в базу данных и возможно обновление зависимых страниц

    }
}
