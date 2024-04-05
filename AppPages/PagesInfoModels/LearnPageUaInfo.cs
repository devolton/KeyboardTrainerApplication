using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication
{
    public class LearnPageUaInfo :ILearnPageInfo
    {
        public string MainTitle { get; init; }
        public string MainDescription { get; init; }
        public string TypingPoseTitle { get; init; }
        public List<string> TypingPoseRulesList { get; init; }
        public string StartPositionTitle { get; init; }
        public string StartPositionDescription { get; init; }
        public string KeyboardSchemeTitle { get; init; }
        public List<string> KeyboardSchemeRulesList { get; init; }
        public string KeyboardSchemeDescription { get; init; }
        public string FingerMovementTitle { get; init; }
        public string FingerMovementDescription { get; init; }
        public string TypingSpeedTitle { get; init; }
        public List<string> TypingSpeedRulesList { get; init; }
        public string TrainTimeTitle { get; init; }
        public string TrainTimeTestButtonText { get; init; }
        public string TrainTimeStudyButtonText { get; init; }
    }
}
