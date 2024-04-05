using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication
{
    interface ILearnPageInfo
    {
         string MainTitle { get; init; }
         string MainDescription { get; init; }
         string TypingPoseTitle { get; init; }
         List<string> TypingPoseRulesList { get; init; }
         string StartPositionTitle { get; init; }
         string StartPositionDescription { get; init; }
         string KeyboardSchemeTitle { get; init; }
         List<string> KeyboardSchemeRulesList { get; init; }
         string KeyboardSchemeDescription { get; init; }
         string FingerMovementTitle { get; init; }
         string FingerMovementDescription { get; init; }
         string TypingSpeedTitle { get; init; }
         List<string> TypingSpeedRulesList { get; init; }
         string TrainTimeTitle { get; init; }
         string TrainTimeTestButtonText { get; init; }
         string TrainTimeStudyButtonText { get; init; }
    }
}
