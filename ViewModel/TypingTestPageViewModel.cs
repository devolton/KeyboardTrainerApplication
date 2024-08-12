using CourseProjectKeyboardApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingTestPageViewModel : ViewModelBase
    {
        private static TypingTestPageViewModel _instance;
        private TypingTestPageModel _model;
        private bool _isShiftWasPushedBefore = false;
        private bool _isTestStarted = false;
        private bool _isFirstKeyPushed = false;
        private TextBlock _textBlock;
        private Visibility _startButtonVisibility;
        private Visibility _hidePanesVisibility;
        private ICommand _startLessonCommand;
        private ICommand _testSetupCommand;
        private ICommand _keyDownCommand;
        private ICommand _endTestCommand;

        private string _infoBlockRightHeaderText = string.Empty;
        private string _infoBlockLeftHeaderText = string.Empty;
        private string _infoBlockRightBodyText = string.Empty;
        private string _infoBlockLeftBodyText = string.Empty;
        private string _firstPartNearAchivementTableText = string.Empty;
        private string _secondPartNearAchivementTebleText = string.Empty;
        private TypingTestPageViewModel()
        {
            _model = TypingTestPageModel.Instance();
            HidePanelVisibility = Visibility.Visible;
            StartButtonVisibility = Visibility.Visible;
            _startLessonCommand = new RelayCommand(OnStartTestCommand, CanExecuteStartTestCommand);
            _testSetupCommand = new RelayCommand(OnTestSetupCommand);
            _keyDownCommand = new RelayCommand(OnKeyDownCommand, CanExecuteKeyCommand);
            _endTestCommand = new RelayCommand(OnEndTestCommand);
            _infoBlockRightHeaderText = _model.GetRightInfoHeaderText();
            _infoBlockRightBodyText = _model.GetRightInfoBodyText();
            _infoBlockLeftHeaderText = _model.GetLeftInfoHeaderText();
            _infoBlockLeftBodyText = _model.GetLeftInfoBodyText();
            _firstPartNearAchivementTableText = _model.GetFirstPartNearAchivementTebleText();
            _secondPartNearAchivementTebleText = _model.GetSecondPartNearAchivementTableText();

        }
        public static TypingTestPageViewModel Instance()
        {
            _instance ??= new TypingTestPageViewModel();
            return _instance;
        }

        //properties
        #region
        public ICommand TestSetupCommand => _testSetupCommand;
        public ICommand StartTestCommand => _startLessonCommand;
        public ICommand KeyDownCommand => _keyDownCommand;
        public ICommand EndTestCommand => _endTestCommand;
        public Visibility StartButtonVisibility
        {
            get { return _startButtonVisibility; }
            set
            {
                _startButtonVisibility = value;
                OnPropertyChanged(nameof(StartButtonVisibility));
            }
        }
        public Visibility HidePanelVisibility
        {
            get { return _hidePanesVisibility; }
            set
            {
                _hidePanesVisibility = value;
                OnPropertyChanged(nameof(HidePanelVisibility));
            }
        }
        public string InfoBlockRightHeaderText
        {
            get => _infoBlockRightHeaderText;
            set
            {
                _infoBlockLeftBodyText = value;
                OnPropertyChanged(nameof(InfoBlockRightHeaderText));
            }
        }
        public string InfoBlockLeftHeaderText
        {
            get => _infoBlockLeftHeaderText;
            set
            {
                _infoBlockLeftHeaderText = value;
                OnPropertyChanged(nameof(InfoBlockLeftHeaderText));
            }
        }
        public string InfoBlockRightBodyText
        {
            get => _infoBlockRightBodyText;
            set
            {
                _infoBlockRightBodyText = value;
                OnPropertyChanged(nameof(InfoBlockRightBodyText));
            }
        }
        public string InfoBlockLeftBodyText
        {
            get => _infoBlockLeftBodyText;
            set
            {
                _infoBlockLeftBodyText = value;
                OnPropertyChanged(nameof(InfoBlockLeftBodyText));
            }
        }
        public string FirstPartNearAchivementTableText
        {
            get => _firstPartNearAchivementTableText;
            set
            {
                _firstPartNearAchivementTableText = value;
                OnPropertyChanged(nameof(FirstPartNearAchivementTableText));
            }
        }
        public string SecondPartNearAchivementTableText
        {
            get => _secondPartNearAchivementTebleText;
            set
            {
                _secondPartNearAchivementTebleText = value;
                OnPropertyChanged(nameof(SecondPartNearAchivementTableText));
            }
        }

        #endregion
        //commands
        #region
        private void OnStartTestCommand(object param)
        {
            HidePanelVisibility = Visibility.Hidden;
            StartButtonVisibility = Visibility.Hidden;
            _isTestStarted = true;

        }
        private void OnTestSetupCommand(object param)
        {
            _textBlock = param as TextBlock;
            _model.ResetTestSettings();
            _model.SetupTest();
            if (_textBlock is not null)
            {
                _textBlock.Inlines.Clear();
                _textBlock.Inlines.AddRange(_model.GetTextRuns());

            }


        }
        private void OnKeyDownCommand(object param)
        {
            if (!_isFirstKeyPushed)
            {
                _isFirstKeyPushed = true;
                _model.StartTimer();
            }

            Key key = (Key)param;
            if (_model.IsFocusCharUppercase())
            {
                if ((IsShiftPushed() || _isShiftWasPushedBefore) && _model.IsValidPushedButton(key, true))
                {
                    if (key.Equals(Key.Space))
                        _model.IncrementWordsTypingCount();
                    _model.SetSymbolRunStyle(true);

                }
                else
                {
                    if (IsShiftPushed())
                    {
                       _isShiftWasPushedBefore = true;
                        Task.Run(async () =>
                        {
                            await Task.Delay(1000);
                            _isShiftWasPushedBefore = false;
                        });
                        return;
                    }
                    _model.SetSymbolRunStyle(false);
                    _model.IncrementMissclickCount();

                }
            }
            else
            {
                if (IsShiftPushed())
                {
                    _isShiftWasPushedBefore = true;
                    Task.Run(async () =>
                    {
                        await Task.Delay(1000);
                        _isShiftWasPushedBefore = false;
                    });
                    return;
                }
                if (_model.IsValidPushedButton(key, false))
                {
                    if (key.Equals(Key.Space))
                        _model.IncrementWordsTypingCount();
                    _model.SetSymbolRunStyle(true);

                }
                else
                {
                    _model.SetSymbolRunStyle(false);
                    _model.IncrementMissclickCount();
                }

            }


        }

        private void OnEndTestCommand(object param)
        {
            StartButtonVisibility = Visibility.Visible;
            HidePanelVisibility = Visibility.Visible;
            _isTestStarted = false;
            _isFirstKeyPushed = false;
            _model.TimerReset();

        }
        #endregion

        //predicate commands
        #region
        private bool CanExecuteKeyCommand(object param)
        {
            return _isTestStarted;
        }
        private bool CanExecuteStartTestCommand(object param)
        {
            return _model.IsEnglishLanguageSelected();
        }
        #endregion
        private bool IsShiftPushed()
        {
            return Keyboard.GetKeyStates(Key.LeftShift) == KeyStates.Down || Keyboard.GetKeyStates(Key.RightShift) == KeyStates.Down;
        }
    }
}
