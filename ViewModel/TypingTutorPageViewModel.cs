using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.View.CustomControls;
using MaterialDesignThemes.Wpf.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingTutorPageViewModel : ViewModelBase
    {
        private List<KeyboardItemTextBlock> _keyboardElementsList;
        private List<KeyboardControlItemTextBlock> _keyboardControlElementsList;
        private Grid _keyboardGrid;
        private Visibility _isStartButtonVisible;
        private Visibility _isHidingPanelVisible;
        private double _progressBarValue;
        private double _progressBarMaxValue;
        private TextBlock _textBlock;

        private ICommand _startLessonCommand;
        private ICommand _restartLessonCommand;
        private bool _isLessonStarted;
        private bool _isRepeatButtonEnabled;
        private bool _isStartButtonEnabled;
        private bool _isErrorKeyPushed;

        private static TypingTutorPageViewModel _instance;
        private readonly TypingTutorPageModel _typingTutorPageModel;

        private TypingTutorPageViewModel()
        {
            KeyDownCommand = new RelayCommand(OnKeyDownCommand, CanExecuteOnKeyDownCommand);
            GenerateRunsBlocksCommand = new RelayCommand(OnGenarateRunsElements);
            _startLessonCommand = new RelayCommand(OnStartLessonExecuteCommand, CanExecuteStartLessonCommand);
            _restartLessonCommand = new RelayCommand(OnRestartLessonCommand, CanOnRepeatLessonCommandExecute);
            KeyUpCommand = new RelayCommand(OnKeyUpCommand);
            _typingTutorPageModel = TypingTutorPageModel.Instance();
            _keyboardControlElementsList = new List<KeyboardControlItemTextBlock>();
            _keyboardElementsList = new List<KeyboardItemTextBlock>();
            _isErrorKeyPushed = false;
            _isLessonStarted = false;
            IsRepeatButtonEnabled = false;
            ProgressBarValue = 0;
            IsStartButtonVisible = Visibility.Visible;
            IsHigingPanelVisible = Visibility.Visible;
            ProgressBarMaxValue = _typingTutorPageModel.GetProgressBarMaxValue();



        }
        public static TypingTutorPageViewModel Instance()
        {
            _instance ??= new TypingTutorPageViewModel();
            return _instance;

        }
        //Properties
        #region
        public Grid KeyboardGrid
        {
            get { return _keyboardGrid; }
            set
            {
                _keyboardGrid = value;
                InitKeyboardItemLists();
                OnPropertyChanged(nameof(KeyboardGrid));
            }
        }
        public ICommand RestartLessonCommand => _restartLessonCommand;
        public ICommand StartLessonCommand => _startLessonCommand;
        public ICommand KeyDownCommand
        {
            get;
        }
        public ICommand KeyUpCommand
        {
            get;
        }
        public ICommand GenerateRunsBlocksCommand
        {
            get;
        }
        public Visibility IsStartButtonVisible
        {
            get { return _isStartButtonVisible; }
            set
            {
                _isStartButtonVisible = value;
                OnPropertyChanged(nameof(IsStartButtonVisible));
            }
        }

        public Visibility IsHigingPanelVisible
        {
            get { return _isHidingPanelVisible; }
            set
            {
                _isHidingPanelVisible = value;
                OnPropertyChanged(nameof(IsHigingPanelVisible));
            }

        }
        public double ProgressBarMaxValue
        {
            get { return _progressBarMaxValue; }
            set
            {
                _progressBarMaxValue = value;
                OnPropertyChanged(nameof(ProgressBarMaxValue));
            }
        }
        public bool IsRepeatButtonEnabled
        {
            get { return _isRepeatButtonEnabled; }
            set
            {
                _isRepeatButtonEnabled = value;
                OnPropertyChanged(nameof(IsRepeatButtonEnabled));
            }
        }



        public double ProgressBarValue
        {
            get { return _progressBarValue; }
            set
            {
                _progressBarValue = value;
                OnPropertyChanged(nameof(ProgressBarValue));
            }

        }

        #endregion


        //Commands
        #region
        private void OnGenarateRunsElements(object param)
        {
            _textBlock = param as TextBlock;
            if (_textBlock is not null)
            {
                _textBlock.Inlines.AddRange(_typingTutorPageModel.GetLearnStrRuns());
            }
        }
        private void OnRestartLessonCommand(object param)
        {
            _typingTutorPageModel.LessonReset();
        }
        private void OnKeyDownCommand(object param)
        {
            var key = (Key)param;
            if (_typingTutorPageModel.IsFocusCharUppercase())
            {
                if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    ShiftKeyDownHandler();
                    if (_typingTutorPageModel.IsValidPushedButton(key, true))
                    {
                        _typingTutorPageModel.ChangeFocusToNextRun();
                        ProgressBarValue++;

                    }

                }
            }
            

        }
        private void OnStartLessonExecuteCommand(object param)
        {
            _isLessonStarted = true;
            IsRepeatButtonEnabled = true;
            IsStartButtonVisible = Visibility.Hidden;
            IsHigingPanelVisible = Visibility.Hidden;
            if (_typingTutorPageModel.IsFocusCharUppercase())
            {
                var shiftElementEnumerable = _keyboardControlElementsList.FindAll(oneControl => Convert.ToString(oneControl.Tag).Contains("Shift"));
                foreach (var oneShiftElement in shiftElementEnumerable)
                {
                    oneShiftElement.IsFocusKeyboardItem = true;
                }
            }
            else
            {
                SetFocusInKeyboardItem();
            }
        }
        private void OnKeyUpCommand(object param)
        {
            //сбарсываем стиль ошибочно нажатой кнопки а так же стиль нажатого Shift
            if (_isErrorKeyPushed)
            {
                var key = (Key)param;
                var tag = _typingTutorPageModel.GetKeyStrTag(key);
                if (tag is not null)
                {
                    var errorElement = _keyboardElementsList.FirstOrDefault(oneElement => Convert.ToString(oneElement.Tag).Contains(tag));
                    if (errorElement is not null)
                    {
                        errorElement.IsErrorPushedKeyboardItem = false;
                    }
                    else
                    {
                        var controlErrorElement = _keyboardControlElementsList.FirstOrDefault(oneElement => Convert.ToString(oneElement.Tag).Contains(tag));
                        if (controlErrorElement is not null)
                            controlErrorElement.IsErrorPushedKeyboardItem = false;


                    }
                }
            }
            var shifts = _keyboardControlElementsList.FindAll(oneControl => Convert.ToString(oneControl.Tag).Contains("Shift"));
            foreach (var oneShift in shifts)
            {
                oneShift.IsFocusKeyboardItem = false;
            }
            foreach (var oneKeyElement in _keyboardElementsList)
            {
                oneKeyElement.TextValue = Convert.ToString(oneKeyElement.Tag)[0].ToString();
            }
            var focusElement = _keyboardElementsList.FirstOrDefault(oneElement => oneElement.IsFocusKeyboardItem);
            if (focusElement is not null)
                focusElement.IsFocusKeyboardItem = false;


        }
        #endregion

        //command predicates block
        #region
        private bool CanOnRepeatLessonCommandExecute(object value)
        {
            return _isLessonStarted;
        }
        private bool CanExecuteOnKeyDownCommand(object param)
        {
            return _isLessonStarted;
        }
        private bool CanExecuteStartLessonCommand(object param)
        {
            return InputLanguageManager.Current.CurrentInputLanguage.EnglishName.Contains("English");
        }

        #endregion



        private void InitKeyboardItemLists()
        {
            _keyboardElementsList.Clear();
            _keyboardControlElementsList.Clear();
            foreach (var element in KeyboardGrid.Children)
            {
                var keyboardItem = element as KeyboardItemTextBlock;
                if (keyboardItem is not null)
                {
                    _keyboardElementsList.Add(keyboardItem);
                    continue;
                }
                var keyboardControlItem = element as KeyboardControlItemTextBlock;
                if (keyboardControlItem is not null)
                {
                    _keyboardControlElementsList.Add(keyboardControlItem);
                }


            }
        }
        private void ShiftKeyDownHandler()
        {
            var shiftEnumarable = _keyboardControlElementsList.Where(oneKey => oneKey.Name.Contains("Shift"));
            foreach (var oneShift in shiftEnumarable)
            {
                oneShift.IsFocusKeyboardItem = true;
            }
            foreach (var oneKeyElement in _keyboardElementsList)
            {
                oneKeyElement.TextValue = Convert.ToString(oneKeyElement.Tag)[1].ToString();
            }
            SetFocusInKeyboardItem();
        }
        private void SetFocusInKeyboardItem()
        {
            var currentTag = _typingTutorPageModel.GetCurrentKeyTag();
            var focusElement = _keyboardElementsList.FirstOrDefault(element => Convert.ToString(element.Tag).Contains(currentTag));
            focusElement.IsFocusKeyboardItem = true;
        }


    }
}
