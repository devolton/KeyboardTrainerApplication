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
    interface ITypingTestPageModel
    {
        ImageSource GetTargetImageSource();
        ImageSource GetFlashImageSource();
        ImageSource GetStarImageSource();
        ImageSource GetKeyboardIconImageSource();
        void SetTimerInterval(int milliseconds);
        List<Run> GetTextRuns();
        bool IsEnglishLanguageSelected();
        void SetupTest();
        void ResetTestSettings();
        void StartTimer();
        void TimerReset();
        bool IsFocusCharUppercase();
        bool IsValidPushedButton(Key pushedKey, bool isShiftPushed);
        void SetSymbolRunStyle(bool isValidPushed);
        string GetLeftInfoHeaderText();
        string GetRightInfoHeaderText();
        string GetFirstPartNearAchivementTebleText();
        string GetSecondPartNearAchivementTableText();
        string GetLeftInfoBodyText();
        string GetRightInfoBodyText();
        void IncrementMissclickCount();
        void IncrementPushedSymbolsCount();
        void IncrementWordsTypingCount();
    }
}
