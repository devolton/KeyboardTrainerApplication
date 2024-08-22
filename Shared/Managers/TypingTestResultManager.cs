using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Managers
{
    public static class TypingTestResultManager
    {
        private static int _mislickCount = 0;
        private static int _allSymbolsCount = 0;
        private static int _typingSpeed = 0;
        public static int MiscliskCount
        {
            get => _mislickCount;
            set
            {
                _mislickCount = value;
            }
        }
        public static int PushedSymbolsCount
        {
            get => _allSymbolsCount;
            set
            {
                _allSymbolsCount = value;
            }
        }
        public static int TypingSpeed
        {
            get => _typingSpeed;
            set
            {
                _typingSpeed = value;
            }
        }
    }
}
