using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Shared.Providers;
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
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingTestPageViewModel : ViewModelBase
    {
        private static TypingTestPageViewModel _instance;
        private bool _isInit = true;
        private System.Timers.Timer _timer;
        private ITypingTestPageModel _model;
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
        private ICommand _changeTimerValueCommand;
        private ICommand _showNotificatoinWindowCommand;

        private string? _infoBlockRightHeaderText = null;
        private string? _infoBlockLeftHeaderText = null;
        private string? _infoBlockRightBodyText = null;
        private string? _infoBlockLeftBodyText = null;
        private string? _firstPartNearAchivementTableText = null;
        private string? _secondPartNearAchivementTebleText = null;

        private int _timerMinutesValue = 0;
        private int _timerSecondsValue = 30;
        private int _timeComboBoxSelectedIndex = 0;

        private ImageSource _flashImageSource;
        private ImageSource _targetImageSource;
        private ImageSource _starImageSource;
        private ImageSource _keyboardIconImageSource;
        private TypingTestPageViewModel()
        {
            _model = new TypingTestPageModel();
            HidePanelVisibility = Visibility.Visible;
            StartButtonVisibility = Visibility.Visible;
            _startLessonCommand = new RelayCommand(OnStartTestCommand, CanExecuteStartTestCommand);
            _testSetupCommand = new RelayCommand(OnTestSetupCommand);
            _keyDownCommand = new RelayCommand(OnKeyDownCommand, CanExecuteKeyCommand);
            _endTestCommand = new RelayCommand(OnEndTestCommand);
            _changeTimerValueCommand = new RelayCommand(OnChangeTimerValueCommand);
            _showNotificatoinWindowCommand = new RelayCommand(OnShowNotificationCommand, CanExecuteShowNotificationWindow);
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += _timer_Elapsed;



        }

        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (_timerMinutesValue != 0)
            {
                TimerMinutesValue = (--_timerMinutesValue).ToString();
                TimerSecondsValue = 59.ToString();

            }
            else
                TimerSecondsValue = (--_timerSecondsValue).ToString();

        }

        public static TypingTestPageViewModel Instance()
        {
            _instance ??= new TypingTestPageViewModel();
            return _instance;
        }

        //properties
        #region
        public ICommand ShowNotificationWindowCommand => _showNotificatoinWindowCommand;
        public ICommand ChangeTimeValueCommand => _changeTimerValueCommand;
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
        public string? InfoBlockRightHeaderText
        {
            get => _infoBlockRightHeaderText;
            set
            {
                _infoBlockRightHeaderText = value;
                OnPropertyChanged(nameof(InfoBlockRightHeaderText));
            }
        }
        public string? InfoBlockLeftHeaderText
        {
            get => _infoBlockLeftHeaderText;
            set
            {
                _infoBlockLeftHeaderText = value;
                OnPropertyChanged(nameof(InfoBlockLeftHeaderText));
            }
        }
        public string? InfoBlockRightBodyText
        {
            get => _infoBlockRightBodyText;
            set
            {
                _infoBlockRightBodyText = value;
                OnPropertyChanged(nameof(InfoBlockRightBodyText));
            }
        }
        public string? InfoBlockLeftBodyText
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
        public string? SecondPartNearAchivementTableText
        {
            get => _secondPartNearAchivementTebleText;
            set
            {
                _secondPartNearAchivementTebleText = value;
                OnPropertyChanged(nameof(SecondPartNearAchivementTableText));
            }
        }
        public string TimerMinutesValue
        {
            get
            {
                var currentValue = (_timerMinutesValue < 10) ? "0" + _timerMinutesValue.ToString() : _timerMinutesValue.ToString();
                return currentValue;
            }
            set
            {
                _timerMinutesValue = int.Parse(value);
                OnPropertyChanged(nameof(TimerMinutesValue));
            }
        }
        public string TimerSecondsValue
        {
            get
            {
                var currentValue = (_timerSecondsValue < 10) ? "0" + _timerSecondsValue.ToString() : _timerSecondsValue.ToString();
                return currentValue;
            }
            set
            {
                _timerSecondsValue = int.Parse(value);
                OnPropertyChanged(nameof(TimerSecondsValue));
            }
        }
        public ImageSource StarImageSource
        {
            get => _starImageSource;
            set
            {
                _starImageSource = value;
                OnPropertyChanged(nameof(StarImageSource));
            }
        }
        public ImageSource FlashImageSource
        {
            get => _flashImageSource;
            set
            {
                _flashImageSource = value;
                OnPropertyChanged(nameof(FlashImageSource));
            }
        }
        public ImageSource TargetImageSource
        {
            get => _targetImageSource;
            set
            {
                _targetImageSource = value;
                OnPropertyChanged(nameof(TargetImageSource));

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
        public int TimeComboBoxSelectedIndex
        {
            get => _timeComboBoxSelectedIndex;
            set
            {
                _timeComboBoxSelectedIndex = value;
                OnPropertyChanged(nameof(TimeComboBoxSelectedIndex));
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
        private void OnChangeTimerValueCommand(object param)
        {
            SetTimerBlock();
        }
        private void OnTestSetupCommand(object param)
        {
            if (_isInit)
                InitStaticContent();
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
                _timer.Start();
            }

            Key key = (Key)param;
            if (_model.IsFocusCharUppercase())
            {
                if ((IsShiftPushed() || _isShiftWasPushedBefore) && _model.IsValidPushedButton(key, true))
                {
                    if (key.Equals(Key.Space))
                        _model.IncrementWordsTypingCount();
                    _model.IncrementPushedSymbolsCount();
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
                    _model.IncrementPushedSymbolsCount();
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
            SetTimerBlock();
            TimerSecondsValue = _timerSecondsValue.ToString();
            _timer.Stop();

        }
        private void OnShowNotificationCommand(object param)
        {
            NotificationMediator.ShowNotificationWindow(NotifyType.InvalidLanguageSelected);
        }
        #endregion

        //predicate commands
        #region
        private bool CanExecuteShowNotificationWindow(object param)
        {
            return !_model.IsEnglishLanguageSelected();
        }
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

        private void InitStaticContent()
        {
            TargetImageSource ??= _model.GetTargetImageSource();
            FlashImageSource ??= _model.GetFlashImageSource();
            StarImageSource ??= _model.GetStarImageSource();
            InfoBlockRightHeaderText ??= _model.GetRightInfoHeaderText();
            InfoBlockRightBodyText ??= _model.GetRightInfoBodyText();
            InfoBlockLeftHeaderText ??= _model.GetLeftInfoHeaderText();
            InfoBlockLeftBodyText ??= _model.GetLeftInfoBodyText(); ;
            FirstPartNearAchivementTableText ??= _model.GetFirstPartNearAchivementTebleText();
            SecondPartNearAchivementTableText ??= _model.GetSecondPartNearAchivementTableText();
            KeyboardIconImageSource ??= _model.GetKeyboardIconImageSource();
            _isInit = false;
        }
        private void SetTimerBlock()
        {
            if (TimeComboBoxSelectedIndex == 0)
            {
                TimerMinutesValue = 0.ToString();
                TimerSecondsValue = 30.ToString();
                _model.SetTimerInterval(30000);
            }
            else
            {
                TimerMinutesValue = 1.ToString();
                TimerSecondsValue = 0.ToString();
                _model.SetTimerInterval(60000);
            }
        }
    }
}
