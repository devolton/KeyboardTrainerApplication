using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces
{
    public interface ITypingTutorResultPageModel
    {
        void InitData();
        bool IsExecuteLessTwoErrorCondition();
        bool IsExecuteWithoutMisclickCondition();
        bool IsExecuteSpeedCondition();
        bool IsCurrentLessonNotLast();
        void UpdateLessonData();
        void SetNextEducationUserProgress();
        string GetLessTwoMistakeText();
        string GetWithoutMistakeText();
        string GetSpeedText();
        string GetLessonResultStr();
        ImageSource GetGoldStarImageSource();
        ImageSource GetLightGrayStarImageSource();
        ImageSource GetGoldFlashImageSource();
        ImageSource GetLightGrayFlashImageSource();
        ImageSource GetGoldTargetImageSource();
        ImageSource GetLightGrayTargetImageSource();
    }
}
