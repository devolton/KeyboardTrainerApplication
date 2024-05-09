using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Interfaces;
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
using System.Windows.Interop;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingTutorPageViewModel : ViewModelBase
    {
        private List<IKeyboardItem> _keyboardItemList;
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
            KeyUpCommand = new RelayCommand(OnKeyUpCommand, CanExecuteOnKeyDownCommand);
            _typingTutorPageModel = TypingTutorPageModel.Instance();
            _keyboardItemList = new List<IKeyboardItem>();
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
        public bool IsLessonStarter
        {
            get {return _isLessonStarted; }
            set
            {
                _isLessonStarted= value;
                IsHiginglockVisible();
                OnPropertyChanged(nameof(IsLessonStarter));
            }
        }
        public Grid KeyboardGrid
        {
            get { return _keyboardGrid; }
            set
            {
                _keyboardGrid = value;
                InitKeyboardItemList();
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
            ProgressBarValue = 0;
            _textBlock.Inlines.Clear();
            if (_textBlock is not null)
            {
                _textBlock.Inlines.AddRange(_typingTutorPageModel.GetLearnStrRuns());
            }
        }
        private void OnRestartLessonCommand(object param)
        {
            ProgressBarValue = 0;
            _typingTutorPageModel.LessonReset();
            IsLessonStarter = false;
            
        }
        private void OnKeyDownCommand(object param)
        {
            var key = (Key)param;
            string keyTag = _typingTutorPageModel.GetKeyStrTag(key);
            if (IsShiftPushed())
            {
                ShiftKeyDownHandler();


                if (_typingTutorPageModel.IsFocusCharUppercase() && IsShiftPushed())
                {


                    if (_typingTutorPageModel.IsValidPushedButton(key, true))
                    {
                        ProgressBarValue++;
                        _typingTutorPageModel.ChangeFocusToNextRun();
                    }
                    else
                    {
                        SetErrorStyleInKeyboardItem(keyTag);
                        _typingTutorPageModel.SetRunErrorStyle();
                        _typingTutorPageModel.AddMissclickCount();
                    }
                }
            }
            else
            {
                if (_typingTutorPageModel.IsValidPushedButton(key, false) && !IsShiftPushed())
                {
                    ProgressBarValue++;
                    _typingTutorPageModel.ChangeFocusToNextRun();
                }
                else
                {

                    if (!IsShiftPushed())
                    {
                        _typingTutorPageModel.AddMissclickCount();
                        SetErrorStyleInKeyboardItem(keyTag);
                        _typingTutorPageModel.SetRunErrorStyle();
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
                var shiftElementEnumerable = _keyboardItemList.FindAll(oneControl => oneControl.KeyTag.Contains("Shift"));
                foreach (var oneShiftElement in shiftElementEnumerable)
                {
                    oneShiftElement.IsFocusKeyboardItem = true;
                }
            }
            else
            {
                SetFocusInKeyboardItem();
            }
            _typingTutorPageModel.StartMeasureTime();//запускаем секундомер
        }
        private void OnKeyUpCommand(object param)
        {
            var key = (Key)param;
            var tag = _typingTutorPageModel.GetKeyStrTag(key);
            //сбарсываем стиль ошибочно нажатой кнопки а так же стиль нажатого Shift
            if (_isErrorKeyPushed)
            {
                if (tag is not null)
                {
                    var errorElement = _keyboardItemList.FirstOrDefault(oneElement => oneElement.KeyTag.Contains(tag));
                    if (errorElement is not null)
                    {
                        errorElement.IsErrorPushedKeyboardItem = false;
                    }

                }
                _isErrorKeyPushed = false;
                _typingTutorPageModel.RemoveRunErrorStyle();
            }

            if (key == Key.LeftShift || key == Key.RightShift)
            {
                var shifts = _keyboardItemList.FindAll(oneControl => oneControl.KeyTag.Contains("Shift"));
                foreach (var oneShift in shifts)
                {
                    oneShift.IsFocusKeyboardItem = false;
                }
                foreach (var oneKeyElement in _keyboardItemList)
                {
                    var oneKeyItem = oneKeyElement as KeyboardItemTextBlock;
                    if (oneKeyItem is not null)
                        oneKeyElement.TextValue = Convert.ToString(oneKeyElement.KeyTag)[0].ToString();
                }
            }
            var focusElement = _keyboardItemList.FirstOrDefault(oneElement => oneElement.IsFocusKeyboardItem);
            if (focusElement is not null)
                focusElement.IsFocusKeyboardItem = false;

            SetFocusInKeyboardItem();



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
        private void IsHiginglockVisible()
        {
            IsHigingPanelVisible = (CanExecuteOnKeyDownCommand(null)) ? Visibility.Hidden: Visibility.Visible;
            IsStartButtonVisible = (CanExecuteOnKeyDownCommand(null)) ? Visibility.Hidden : Visibility.Visible;
   
        }

        #endregion




        private void InitKeyboardItemList()
        {
            _keyboardItemList.Clear();
            foreach (var element in KeyboardGrid.Children)
            {
                var keyElement = element as IKeyboardItem;
                if (keyElement is not null)
                    _keyboardItemList.Add(keyElement);
            }
        }
        private void ShiftKeyDownHandler()
        {
            var shiftEnumarable = _keyboardItemList.Where(oneKey => oneKey.KeyTag.Contains("Shift"));
            foreach (var oneShift in shiftEnumarable)
            {
                oneShift.IsFocusKeyboardItem = true;
            }
            foreach (var oneKeyElement in _keyboardItemList)
            {
                var keyElement = oneKeyElement as KeyboardItemTextBlock;
                if (keyElement is not null)
                    oneKeyElement.TextValue = oneKeyElement.KeyTag[1].ToString();
            }
            SetFocusInKeyboardItem();
        }
        private void SetFocusInKeyboardItem()
        {
            var currentTag = _typingTutorPageModel.GetCurrentKeyTag();
            var focusElement = _keyboardItemList.FirstOrDefault(element => element.KeyTag.Contains(currentTag));
            if (focusElement is not null)
                focusElement.IsFocusKeyboardItem = true;

        }

        private void SetErrorStyleInKeyboardItem(string keyTag)
        {
            _isErrorKeyPushed = true;
            IKeyboardItem errorElement = null;
            if (keyTag is not null)
                errorElement = _keyboardItemList.FirstOrDefault(oneKey => oneKey.KeyTag.Contains(keyTag));
            if (errorElement is not null)
                errorElement.IsErrorPushedKeyboardItem = true;

        }
        private bool IsShiftPushed()
        {
            return Keyboard.GetKeyStates(Key.LeftShift) == KeyStates.Down || Keyboard.GetKeyStates(Key.RightShift) == KeyStates.Down;
        }



    }
}
