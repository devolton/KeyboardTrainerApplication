using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Managers
{
    public static class TypingTutorResultManager
    {
        private static int _misclickCount = 0;
        private static int _typingTutorSpeed = 0;
        public static int MisclickCount
        {
            get => _misclickCount;
            set
            {
                _misclickCount = value;
            }
        }
        public static int TypingTutorSpeed
        {
            get => _typingTutorSpeed;
            set
            {
                _typingTutorSpeed = value;
            }
        }
    }
}
