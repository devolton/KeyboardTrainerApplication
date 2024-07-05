using CourseProjectKeyboardApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingTestPageViewModel : ViewModelBase
    {
        private static TypingTestPageViewModel _instance;
        private TypingTestPageModel _typingTestPageModel;
        private bool _isTestStarted;
        private bool _isFirstKeyPushed;
        private TextBlock _textBlock;
        private Visibility _startButtonVisibility;
        private Visibility _hidePanesVisibility;
        private ICommand _startLessonCommand;
        private ICommand _testSetupCommand;
        private ICommand _keyDownCommand;
        private ICommand _endTestCommand;

        private TypingTestPageViewModel()
        {
            _typingTestPageModel = TypingTestPageModel.Instance();
            HidePanelVisibility = Visibility.Visible;
            StartButtonVisibility = Visibility.Visible;
            _isTestStarted = false;
            _isFirstKeyPushed = false;
            _startLessonCommand = new RelayCommand(OnStartTestCommand);
            _testSetupCommand = new RelayCommand(OnTestSetupCommand);
            _keyDownCommand = new RelayCommand(OnKeyDownCommand, CanExecuteKeyCommand);
            _endTestCommand = new RelayCommand(OnEndTestCommand);

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
            _typingTestPageModel.ResetTestSettings();
            if (_textBlock is not null)
            {
                _textBlock.Inlines.Clear();
                _textBlock.Inlines.AddRange(_typingTestPageModel.GetTextRuns());

            }
            

        }
        private void OnKeyDownCommand(object param)
        {
            if (!_isFirstKeyPushed)
            {
                _isFirstKeyPushed = true;
                _typingTestPageModel.StartTimer();
            }

            Key key = (Key)param;
            if (_typingTestPageModel.IsFocusCharUppercase())
            {
                if (IsShiftPushed() && _typingTestPageModel.IsValidPushedButton(key, true))
                {
                    if (key.Equals(Key.Space))
                        _typingTestPageModel.IncrementWordsTypingCount();
                    _typingTestPageModel.SetSymbolRunStyle(true);
               
                }
                else
                {
                    if (IsShiftPushed())
                        return;
                    _typingTestPageModel.SetSymbolRunStyle(false);
                    _typingTestPageModel.IncrementMissclickCount();

                }
            }
            else
            {
                if (IsShiftPushed())
                    return;
                if (_typingTestPageModel.IsValidPushedButton(key, false))
                {
                    if (key.Equals(Key.Space))
                        _typingTestPageModel.IncrementWordsTypingCount();
                    _typingTestPageModel.SetSymbolRunStyle(true);
            
                }
                else
                {
                    _typingTestPageModel.SetSymbolRunStyle(false);
                    _typingTestPageModel.IncrementMissclickCount();
                }

            }


        }

        private void OnEndTestCommand(object param)
        {
            StartButtonVisibility = Visibility.Visible;
            HidePanelVisibility = Visibility.Visible;
            _isTestStarted = false;
            _isFirstKeyPushed = false;

        }
        #endregion

        //predicate commands
        #region
        private bool CanExecuteKeyCommand(object param)
        {
            return _isTestStarted;
        }
        #endregion
        private bool IsShiftPushed()
        {
            return Keyboard.GetKeyStates(Key.LeftShift) == KeyStates.Down || Keyboard.GetKeyStates(Key.RightShift) == KeyStates.Down;
        }
    }
}
