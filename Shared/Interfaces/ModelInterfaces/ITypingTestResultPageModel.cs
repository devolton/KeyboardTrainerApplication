using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces
{
    public interface ITypingTestResultPageModel
    {
        string GetAccuracyPercentStr();
        string GetSpeedStr();
        ImageSource GetKeyboardIconImageSource();
        void InitStat();
    }
}
