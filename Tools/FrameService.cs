using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseProjectKeyboardApplication.Tools
{
    public static class FrameService
    {
        private static Frame _mainFrame;

        public static Frame MainFrame
        {
            get { return _mainFrame; }
            set { _mainFrame = value; }
        }
    }
}
