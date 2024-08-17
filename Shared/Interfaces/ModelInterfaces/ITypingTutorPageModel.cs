using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces
{
    public interface ITypingTutorPageModel
    {
        void StartMeasureTime();
        void StopMeasureTime();
        ImageSource GetRepeatIconImageSource();
        double GetProgressBarMaxValue();
        bool IsFocusCharUppercase();
        void SetRunErrorStyle();
        void RemoveRunErrorStyle();
        string GetCurrentKeyTag();
        bool IsValidPushedButton(Key pushedKey, bool isShiftPushed);
        bool IsEnglishLanguageSelected();
        List<Run> GetLearnStrRuns();
        void ChangeFocusToNextRun();
        void LessonReset();
        string GetKeyStrTag(Key key);
        void AddMissclickCount();
        double GetCurrentTextSize();

    }
}
