using CourseProjectKeyboardApplication.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Model
{
    public class LearnPageModel
    {
        private ImageSource _keyboardSchemeEngImageSource;
        private ImageSource _startLeftEngPositionImageSource;
        private ImageSource _startRightEngPositionImageSource;
        private ImageSource _keyboardIconImageSource;
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

        public ImageSource GetKeyboardSchemeEngImageSource()
        {
            _keyboardSchemeEngImageSource ??= AppImageSourceProvider.KeyboardSchemeEngImageSource;
            return _keyboardSchemeEngImageSource;
        }
        public ImageSource GetStartLeftEngPositionImageSource()
        {
            _startLeftEngPositionImageSource ??= AppImageSourceProvider.StartLeftEngPositionImageSource;
            return _startLeftEngPositionImageSource;
        }
        public ImageSource GetStartRightEngPositionImageSource()
        {
            _startRightEngPositionImageSource ??= AppImageSourceProvider.StartRightEngPositionImageSource;
            return _startRightEngPositionImageSource;
        }
        public ImageSource GetKeyboardIconImageSource()
        {
            _keyboardIconImageSource ??= AppImageSourceProvider.KeyboardIconImageSource;
            return _keyboardIconImageSource;
        }
        

    }
}
