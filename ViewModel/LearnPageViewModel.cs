using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class LearnPageViewModel:ViewModelBase
    {
        private ILearnPageModel _model;
        private ImageSource _keyboardSchemeEngImageSource;
        private ImageSource _startLeftEngPositionImageSource;
        private ImageSource _startRightEngPositionImageSource;
        private ImageSource _keyboardIconImageSource;
        private ICommand _loadCommand;

        private string _mainTitle = string.Empty;
        private string _mainDescription = string.Empty;
        private string _typingPoseTitle = string.Empty;
        private List<string> _typingPoseRulesList = new();
        private string _startPositionTitle = string.Empty;
        private string _startPositionDescription = string.Empty;
        private string _keyboardSchemeTitle = string.Empty;
        private List<string> _keyboardSchemeRulesList = new();
        private string _keyboardSchemeDescription = string.Empty;
        private string _fingerMovementTitle = string.Empty;
        private string _fingerMovementDescription = string.Empty;
        private string _typingSpeedTitle = string.Empty;
        private List<string> _typingSpeedRulesList = new();
        private string _trainTimeTitle = string.Empty;
        private string _trainTimeTestButtonText = string.Empty;
        private string _trainTimeStudyButtonText = string.Empty;



        public LearnPageViewModel()
        {
            _loadCommand = new RelayCommand(OnLoadCommand);
            InitModel();
           
        }
        //prop
        #region
        public ICommand LoadCommand => _loadCommand;
        public string MainTitle {
            get => _mainTitle;
            private set
            {
                _mainTitle = value;
                OnPropertyChanged(nameof(MainTitle));
            }

        }

  

        public string MainDescription
        {
            get => _mainDescription;
            private set
            {
                _mainDescription = value;
                OnPropertyChanged(nameof(MainDescription));
            }
        }

        public string TypingPoseTitle
        {
            get => _typingPoseTitle;
            private set
            {
                _typingPoseTitle = value;
                OnPropertyChanged(nameof(TypingPoseTitle));
            }
        }

        public List<string> TypingPoseRulesList
        {
            get => _typingPoseRulesList;
            private set
            {
                _typingPoseRulesList = value;
                OnPropertyChanged(nameof(TypingPoseRulesList));
            }
        }

        public string StartPositionTitle
        {
            get => _startPositionTitle;
            private set
            {
                _startPositionTitle = value;
                OnPropertyChanged(nameof(StartPositionTitle));
            }
        }

        public string StartPositionDescription
        {
            get => _startPositionDescription;
            private set
            {
                _startPositionDescription = value;
                OnPropertyChanged(nameof(StartPositionDescription));
            }
        }

        public string KeyboardSchemeTitle
        {
            get => _keyboardSchemeTitle;
            private set
            {
                _keyboardSchemeTitle = value;
                OnPropertyChanged(nameof(KeyboardSchemeTitle));
            }
        }

        public List<string> KeyboardSchemeRulesList
        {
            get => _keyboardSchemeRulesList;
            private set
            {
                _keyboardSchemeRulesList = value;
                OnPropertyChanged(nameof(KeyboardSchemeRulesList));
            }
        }

        public string KeyboardSchemeDescription
        {
            get => _keyboardSchemeDescription;
            private set
            {
                _keyboardSchemeDescription = value;
                OnPropertyChanged(nameof(KeyboardSchemeDescription));
            }
        }

        public string FingerMovementTitle
        {
            get => _fingerMovementTitle;
            private set
            {
                _fingerMovementTitle = value;
                OnPropertyChanged(nameof(FingerMovementTitle));
            }
        }

        public string FingerMovementDescription
        {
            get => _fingerMovementDescription;
            private set
            {
                _fingerMovementDescription = value;
                OnPropertyChanged(nameof(FingerMovementDescription));
            }
        }

        public string TypingSpeedTitle
        {
            get => _typingSpeedTitle;
            private set
            {
                _typingSpeedTitle = value;
                OnPropertyChanged(nameof(TypingSpeedTitle));
            }
        }

        public List<string> TypingSpeedRulesList
        {
            get => _typingSpeedRulesList;
            private set
            {
                _typingSpeedRulesList = value;
                OnPropertyChanged(nameof(TypingSpeedRulesList));
            }
        }

        public string TrainTimeTitle
        {
            get => _trainTimeTitle;
            private set
            {
                _trainTimeTitle = value;
                OnPropertyChanged(nameof(TrainTimeTitle));
            }
        }

        public string TrainTimeTestButtonText
        {
            get => _trainTimeTestButtonText;
            private set
            {
                _trainTimeTestButtonText = value;
                OnPropertyChanged(nameof(TrainTimeTestButtonText));
            }
        }

        public string TrainTimeStudyButtonText
        {
            get => _trainTimeStudyButtonText;
            private set
            {
                _trainTimeStudyButtonText = value;
                OnPropertyChanged(nameof(TrainTimeStudyButtonText));
            }
        }



        public ImageSource KeyboardSchemeEngImageSource
        {
            get => _keyboardSchemeEngImageSource;
            set
            {
                _keyboardSchemeEngImageSource = value;
                OnPropertyChanged(nameof(KeyboardSchemeEngImageSource));
            }
        }
        public ImageSource StartLeftEngPositionImageSource
        {
            get => _startLeftEngPositionImageSource;
            set
            {
                _startLeftEngPositionImageSource = value;
                OnPropertyChanged(nameof(StartLeftEngPositionImageSource));
            }
        }
        public ImageSource StartRightEngPositionImageSource
        {
            get => _startRightEngPositionImageSource;
            set
            {
                _startRightEngPositionImageSource = value;
                OnPropertyChanged(nameof(StartRightEngPositionImageSource));
            }
        }
        public ImageSource KeyboardIconImageSource
        {
            get => _keyboardIconImageSource;
            set
            {
                _keyboardIconImageSource = value;
                OnPropertyChanged(nameof(KeyboardIconImageSource));
            }
        }
        #endregion
        //command
        #region
        public void OnLoadCommand(object param)
        {

            MainTitle = _model.MainTitle;
            MainDescription = _model.MainDescription;
            TypingPoseTitle = _model.TypingPoseTitle;
            TypingPoseRulesList = _model.TypingPoseRulesList;
            StartPositionTitle = _model.StartPositionTitle;
            StartPositionDescription = _model.StartPositionDescription;
            KeyboardSchemeTitle = _model.KeyboardSchemeTitle;
            KeyboardSchemeRulesList = _model.KeyboardSchemeRulesList;
            KeyboardSchemeDescription = _model.KeyboardSchemeDescription;
            FingerMovementTitle = _model.FingerMovementTitle;
            FingerMovementDescription = _model.FingerMovementDescription;
            TypingSpeedTitle = _model.TypingSpeedTitle;
            TypingSpeedRulesList = _model.TypingSpeedRulesList;
            TrainTimeTitle = _model.TrainTimeTitle;
            TrainTimeTestButtonText = _model.TrainTimeTestButtonText;
            TrainTimeStudyButtonText = _model.TrainTimeStudyButtonText;
            KeyboardIconImageSource = _model.GetKeyboardIconImageSource();
            KeyboardSchemeEngImageSource = _model.GetKeyboardSchemeEngImageSource();
            StartLeftEngPositionImageSource = _model.GetStartLeftEngPositionImageSource();
            StartRightEngPositionImageSource = _model.GetStartRightEngPositionImageSource();
            

        }
        #endregion
        private async Task InitModel()
        {
            _model = JsonSerializer.Deserialize<LearnPageModel>(await ContentApiClientProvider.JsonTextApiClient.GetPageJsonAsync(Shared.Enums.PageType.LearnPage));
        }


    }
}
