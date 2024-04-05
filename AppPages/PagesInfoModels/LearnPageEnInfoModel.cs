using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication
{
    public class LearnPageEnInfoModel :ILearnPageInfo 
    {
        public string MainTitle { get; init; } = "Learn how to touch type";
        public string MainDescription { get; init; } = "Touch typing is all about the idea that each finger has its own area on the keyboard. Thanks to that fact you can type without looking at the keys. Practice regularly and your fingers will learn their location on the keyboard through muscle memory.";
        public string TypingPoseTitle { get; init; } = "Sitting posture for typing";
        public List<string> TypingPoseRulesList { get; init; }
        public string StartPositionTitle { get; init; } = "Home row position";
        public string StartPositionDescription { get; init; } = "Curve your fingers a little and put them on the A S D F and J K L ; keys which are located in the middle row of the letter keys. This row is called HOME ROW because you always start from these keys and always return to them.\r\n\r\nF and J keys under your index fingers should have a raised line on them to aide in finding these keys without looking.";
        public string KeyboardSchemeTitle { get; init; }
        public List<string> KeyboardSchemeRulesList { get; init;}
        public string KeyboardSchemeDescription { get; init; }
        public string FingerMovementTitle { get; init; }
        public string FingerMovementDescription { get; init; }
        public string TypingSpeedTitle { get; init; }
        public List<string> TypingSpeedRulesList { get; init; }
        public string TrainTimeTitle { get; init; }
        public string TrainTimeTestButtonText { get; init;}
        public string TrainTimeStudyButtonText { get; init; }
        public LearnPageEnInfoModel()
        {
            TypingPoseRulesList = new List<string>() { "First", "Second", "Third", "Fourth", "Fiveth" };
            
        }


    }
}
