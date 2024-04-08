using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication
{
    public class LearnPageEnInfoModel : ILearnPageInfo
    {
        public string MainTitle { get; init; } = "Learn how to touch type";
        public string MainDescription { get; init; } = "Touch typing is all about the idea that each finger has its own area on the keyboard. Thanks to that fact you can type without looking at the keys. Practice regularly and your fingers will learn their location on the keyboard through muscle memory.";
        public string TypingPoseTitle { get; init; } = "Sitting posture for typing";
        public List<string> TypingPoseRulesList { get; init; }
        public string StartPositionTitle { get; init; } = "Home row position";
        public string StartPositionDescription { get; init; } = "Curve your fingers a little and put them on the A S D F and J K L ; keys which are located in the middle row of the letter keys. This row is called HOME ROW because you always start from these keys and always return to them.\r\n\r\nF and J keys under your index fingers should have a raised line on them to aide in finding these keys without looking.";
        public string KeyboardSchemeTitle { get; init; } = "Keyboard scheme";
        public List<string> KeyboardSchemeRulesList { get; init; }
        public string KeyboardSchemeDescription { get; init; } = "This method may seem inconvenient at first, but do not stop, eventually, you'll find out that you are typing quickly, easily, and conveniently. To achieve the maximum result, choose a touch typing course for your keyboard layout and in the desired language.";
        public string FingerMovementTitle { get; init; } = "Fingers motion";
        public string FingerMovementDescription { get; init; } = "Don't look at the keys when you type. Just slide your fingers around until they find the home row marking.\r\n\r\nLimit your hand and finger movement only to what is necessary to press a specific key. Keep your hands and fingers close to the base position. This improves typing speed and reduces stress on the hands.\r\n\r\nPay attention to ring fingers and little fingers, since they are considerably underdeveloped.";
        public string TypingSpeedTitle { get; init; } = "Typing speed";
        public List<string> TypingSpeedRulesList { get; init; }
        public string TrainTimeTitle { get; init; } = "It's time to get some practice";
        public string TrainTimeTestButtonText { get; init; } = "Speed test";
        public string TrainTimeStudyButtonText { get; init; } = "Start learn";
        public LearnPageEnInfoModel()
        {
            TypingPoseRulesList = new List<string>() { "• Sit straight and remember to keep your back straight.",
                "• Keep your elbows bent at the right angle.",
                "• Face the screen with your head slightly tilted forward.",
                "• Keep at least 45 - 70 cm of distance between your eyes and the screen.",
                "• Еxpose the shoulder, arm, and wrist muscles to the least possible strain." +
                " The wrists can touch the tabletop in front of the keyboard." +
                " Never shift your body weight to the wrists by resting on them." };
            KeyboardSchemeRulesList = new List<string>()
            {
                "• Hit keys only with the fingers for which they have been reserved.",
                "• Always return to the starting position of the fingers 'ASDF – JKL;'.",
                "• Establish and maintain a rhythm while typing. Your keystrokes should come at equal intervals.",
                "• When typing, imagine the location of the symbol on the keyboard.",
                "• The SHIFT key is always pressed by the pinky finger opposite to the one hitting the other key.",
                "• Use the thumb of whichever hand is more convenient for you to press the Space bar."
            };
            TypingSpeedRulesList = new List<string>()
            {
                "• Do not rush when you just started learning. Speed up only when your fingers hit the right keys out of habit.",
                "• Take your time when typing to avoid mistakes. The speed will pick up as you progress.",
                "• Always scan the text a word or two in advance.",
                "• Pass all typing lessons at Ratatype. It will help you to get above the average typing speed."

            };
        }


    }
}
