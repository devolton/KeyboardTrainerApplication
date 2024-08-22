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
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Windows.Shapes;
using System.Windows.Media;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingTutorPageViewModel : ViewModelBase
    {
        private List<IKeyboardItem> _keyboardItemList;
        private List<IKeyboardItem> _errorPressedKeyboardCollection;
        private List<IKeyboardItem> _shiftKeyboardItemCollection;
        private Grid _keyboardGrid;
        private Visibility _isStartButtonVisible;
        private Visibility _isHidingPanelVisible;

        private double _progressBarValue;
        private double _progressBarMaxValue;
        private double _textSize;

        private TextBlock? _textBlock;

        private ImageSource _repeatIconImageSource;

        private ICommand _startLessonCommand;
        private ICommand _restartLessonCommand;
        private ICommand _keyDownCommand;
        private ICommand _keyUpCommand;
        private ICommand _generateRunsBlockCommand;
        private bool _isLessonStarted;
        private bool _isRepeatButtonEnabled;

        private static TypingTutorPageViewModel _instance;
        private readonly ITypingTutorPageModel _model;

        private TypingTutorPageViewModel()
        {
            _model = new TypingTutorPageModel();
            _keyDownCommand = new RelayCommand(OnKeyDownCommand, CanExecuteOnKeyDownCommand);
            _generateRunsBlockCommand = new RelayCommand(OnGenarateRunsElements);
            _startLessonCommand = new RelayCommand(OnStartLessonExecuteCommand, CanExecuteStartLessonCommand);
            _restartLessonCommand = new RelayCommand(OnRestartLessonCommand, CanOnRepeatLessonCommandExecute);
            _keyUpCommand = new RelayCommand(OnKeyUpCommand, CanExecuteOnKeyDownCommand);
          
            _keyboardItemList = new List<IKeyboardItem>();
            _errorPressedKeyboardCollection = new List<IKeyboardItem>();
            _shiftKeyboardItemCollection = new List<IKeyboardItem>();

            _isLessonStarted = false;
            IsRepeatButtonEnabled = false;
            _textSize = 24;
            IsStartButtonVisible = Visibility.Visible;
            IsHigingPanelVisible = Visibility.Visible;




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
            get { return _isLessonStarted; }
            set
            {
                _isLessonStarted = value;
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
        public ICommand KeyDownCommand => _keyDownCommand;
        public ICommand KeyUpCommand => _keyUpCommand;
        public ICommand GenerateRunsBlocksCommand => _generateRunsBlockCommand;
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

        public double TextSize
        {
            get => _textSize;
            set
            {
                _textSize = value;
                OnPropertyChanged(nameof(TextSize));
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
        public ImageSource RepeatButtonImageSource {
            get => _repeatIconImageSource;
            set
            {
                _repeatIconImageSource = value;
                OnPropertyChanged(nameof(RepeatButtonImageSource));
            }
            }

        #endregion


        //Commands
        #region
        /// <summary>
        /// Command of genarate Runs elements (letters ) when page is loaded
        /// </summary>
        /// <param name="param"></param>
        private void OnGenarateRunsElements(object param)
        {
            _textBlock = param as TextBlock;
            RepeatButtonImageSource ??= _model.GetRepeatIconImageSource();

            if (_textBlock is not null)
            {
                _textBlock.Inlines.Clear();
                _textBlock.Inlines.AddRange(_model.GetLearnStrRuns());
            }
            ProgressBarValue = 0;
            ProgressBarMaxValue = _model.GetProgressBarMaxValue();
            TextSize = _model.GetCurrentTextSize();
        }
        /// <summary>
        /// Command when user pushed restart button
        /// </summary>
        /// <param name="param"></param>
        private void OnRestartLessonCommand(object param)
        {
            ProgressBarValue = 0;
            _model.LessonReset();
            RemoveFocusStyle();
            IsLessonStarter = false;

        }
        /// <summary>
        /// Command when user start lesson and pushed keyboard button
        /// </summary>
        /// <param name="param">Key of pushed button</param>
        private void OnKeyDownCommand(object param)
        {

            var key = (Key)param;
            string keyTag = _model.GetKeyStrTag(key);
            if (IsShiftPushed())
            {
                ShiftKeyDownHandler();


                if (_model.IsFocusCharUppercase() && IsShiftPushed())
                {

                    if (_model.IsValidPushedButton(key, true))
                    {
                        ProgressBarValue++;
                        _model.ChangeFocusToNextRun();
                    }
                    else
                    {
                        SetErrorStyleInKeyboardItem(keyTag);
                        _model.SetRunErrorStyle();
                        _model.AddMissclickCount();
                    }
                }
            }
            else
            {
                if (_model.IsValidPushedButton(key, false) && !IsShiftPushed())
                {
                    ProgressBarValue++;
                    _model.ChangeFocusToNextRun();
                }
                else
                {

                    if (!IsShiftPushed())
                    {
                        _model.AddMissclickCount();
                        SetErrorStyleInKeyboardItem(keyTag);
                        _model.SetRunErrorStyle();
                    }

                }
            }

        }
        /// <summary>
        /// Command when user push 'Start Lesson' 
        /// </summary>
        /// <param name="param"></param>
        private void OnStartLessonExecuteCommand(object param)
        {
            _isLessonStarted = true;
            IsRepeatButtonEnabled = true;
            IsStartButtonVisible = Visibility.Hidden;
            IsHigingPanelVisible = Visibility.Hidden;
            if (_model.IsFocusCharUppercase())
            {
                ChangeFocusForShift(true);
            }
            else
            {
                SetFocusInKeyboardItem();
            }
            _model.StartMeasureTime();//запускаем секундомер
        }

        /// <summary>
        /// Command when user released the keyboard button
        /// </summary>
        /// <param name="param"></param>
        private void OnKeyUpCommand(object param)
        {

            var key = (Key)param;
            if (_errorPressedKeyboardCollection.Count != 0)
            {
                RemoveErrorStyle();
            }

            if (key == Key.LeftShift || key == Key.RightShift)
            {
                ChangeFocusForShift(false);
                foreach (var oneKeyElement in _keyboardItemList)
                {
                    var oneKeyItem = oneKeyElement as KeyboardItemTextBlock;
                    if (oneKeyItem is not null)
                        oneKeyElement.TextValue = Convert.ToString(oneKeyElement.KeyTag)[0].ToString();
                }
            }
            RemoveFocusStyle();
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
            return _model.IsEnglishLanguageSelected();
        }


        #endregion
        /// <summary>
        /// Change visibility of panel and button
        /// </summary>
        private void IsHiginglockVisible()
        {
            IsHigingPanelVisible = (CanExecuteOnKeyDownCommand(null)) ? Visibility.Hidden : Visibility.Visible;
            IsStartButtonVisible = (CanExecuteOnKeyDownCommand(null)) ? Visibility.Hidden : Visibility.Visible;

        }


        private void InitKeyboardItemList()
        {
            _keyboardItemList.Clear();
            _shiftKeyboardItemCollection.Clear();
            foreach (var element in KeyboardGrid.Children)
            {
                var keyElement = element as IKeyboardItem;
                if (keyElement is not null)
                    _keyboardItemList.Add(keyElement);
            }
            _shiftKeyboardItemCollection = _keyboardItemList.Where(oneKey => oneKey.KeyTag.Contains("Shift")).ToList();
        }
        /// <summary>
        /// Work when user pressed SHIFT button
        /// </summary>
        private void ShiftKeyDownHandler()
        {
            ChangeFocusForShift(true);
            foreach (var oneKeyElement in _keyboardItemList)
            {
                var keyElement = oneKeyElement as KeyboardItemTextBlock;
                if (keyElement is not null)
                    oneKeyElement.TextValue = oneKeyElement.KeyTag[1].ToString();
            }
            SetFocusInKeyboardItem();
        }
        /// <summary>
        /// Set focus style for Keyboard item
        /// </summary>
        private void SetFocusInKeyboardItem()
        {
            var currentTag = _model.GetCurrentKeyTag();
            var focusElement = _keyboardItemList.FirstOrDefault(element => element.KeyTag.Contains(currentTag));
            if (focusElement is not null)
                focusElement.IsFocusKeyboardItem = true;

        }
        /// <summary>
        /// Set error style for keyboard item
        /// </summary>
        /// <param name="keyTag">Keyboard button tag</param>
        private void SetErrorStyleInKeyboardItem(string keyTag)
        {
            IKeyboardItem errorElement = null;
            if (keyTag is not null)
                errorElement = _keyboardItemList.FirstOrDefault(oneKey => oneKey.KeyTag.Contains(keyTag));
            if (errorElement is not null)
            {
                errorElement.IsErrorPushedKeyboardItem = true;
                _errorPressedKeyboardCollection.Add(errorElement);
            }

        }
        private bool IsShiftPushed()
        {
            return Keyboard.GetKeyStates(Key.LeftShift) == KeyStates.Down || Keyboard.GetKeyStates(Key.RightShift) == KeyStates.Down;
        }

        /// <summary>
        /// Delete focus style
        /// </summary>
        private void RemoveFocusStyle()
        {
            var focusElement = _keyboardItemList.FirstOrDefault(oneElement => oneElement.IsFocusKeyboardItem);
            if (focusElement is not null)
                focusElement.IsFocusKeyboardItem = false;
        }

        /// <summary>
        /// Delete error style
        /// </summary>
        private void RemoveErrorStyle()
        {
            foreach (var item in _errorPressedKeyboardCollection)
            {
                item.IsErrorPushedKeyboardItem = false;

            }
            _errorPressedKeyboardCollection.Clear();
            _model.RemoveRunErrorStyle();
        }
        
        private void ChangeFocusForShift(bool isFocused)
        {

            foreach (var oneShift in _shiftKeyboardItemCollection)
            {
                oneShift.IsFocusKeyboardItem = isFocused;
            }
        }



    }
}
