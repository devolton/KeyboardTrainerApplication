using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Interfaces;
using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.View.CustomControls.EducationResults;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class EducationResultsPageViewModel : ViewModelBase
    {
        private EducationResultsPageModel _model;

        private static EducationResultsPageViewModel _instance;
        private StackPanel _mainStackPanel;
        private Style _lessonButtonUserElementStyle;

        //commands
        private ICommand _initializationCommand;
        private ICommand _continueEducationCommand;
        private ICommand _startLessonCommand;

        //header fields
        private double _valueProgressBar;
        private string _levelProgressHeaderStr = string.Empty;
        private string _languageLayoutTypeHeaderStr = string.Empty;
        private bool _isCurrentLesson = true;

        private IEducationResultLessonButton _currentLessonButton;
        private EducationResultsPageViewModel()
        {
            _model = new EducationResultsPageModel();
            _initializationCommand = new RelayCommand(OnInitializationCommand);
            _continueEducationCommand = new RelayCommand(OnContinueEducationCommand);
            _startLessonCommand = new RelayCommand(OnStartLessonCommand, CanStartLessonCommandExecute);
            _lessonButtonUserElementStyle = (Style)Application.Current.Resources["EducationLessonUserControl"];

        }
        public static EducationResultsPageViewModel Instance()
        {
            _instance ??= new EducationResultsPageViewModel();
            return _instance;
        }
        //properties
        #region
        //команда иницализации блоков
        public ICommand InitializationCommand => _initializationCommand;

        //команда нажатия на кнопку продолжить обучения 
        public ICommand ContinueEducationCommand => _continueEducationCommand;
        //команда начала урока 
        public ICommand StartLessonCommand => _startLessonCommand;
        public StackPanel MainStackPanel
        {
            get { return _mainStackPanel; }
            set
            {
                _mainStackPanel = value;
                OnPropertyChanged(nameof(MainStackPanel));
            }

        }
        public double ValueProgressBar
        {
            get => _valueProgressBar;
            set
            {
                _valueProgressBar = value;
                OnPropertyChanged(nameof(ValueProgressBar));
            }
        }
        //template(Level 2 from 19)
        public string LevelProgressHeaderStr
        {
            get => _levelProgressHeaderStr;
            set
            {
                _levelProgressHeaderStr = value;
                OnPropertyChanged(nameof(LevelProgressHeaderStr));
            }
        }
        public string LanguageLayoutTypeHeaderStr
        {
            get => _languageLayoutTypeHeaderStr;
            set
            {
                _languageLayoutTypeHeaderStr = value;
                OnPropertyChanged(nameof(LanguageLayoutTypeHeaderStr));
            }
        }
        #endregion

        //command
        #region
        /// <summary>
        /// команда инициализации
        /// </summary>
        /// <param name="param">MainStackPanel</param>
        private void OnInitializationCommand(object param)  //возможно разбить на мелкие функции
        {
            _mainStackPanel = param as StackPanel;
            _isCurrentLesson = true;
            if (MainStackPanel.Children.Count >2) 
                MainStackPanel.Children.RemoveRange(2, MainStackPanel.Children.Count - 2);
            LevelProgressHeaderStr = _model.GetCurrentLevelHeaderStr();
            ValueProgressBar = _model.GetPercentOfCompletedLessons();
            LanguageLayoutTypeHeaderStr = _model.GetLanguageLayoutTypeHeaderStr();
            var levelsCollection = _model.GetLevels();
            foreach (var oneLevel in levelsCollection)
            {
                var lessonsCollection = oneLevel.Lessons.OrderBy(oneLesson => oneLesson.Ordinal);
                var educationResultLessonBlock = new EducationResultsLessonBlock();
                var lessonHeader = educationResultLessonBlock.FindName("LessonHeader") as EducationResultsLessonHeader;
                var lessonBodyWrapPanel = educationResultLessonBlock.FindName("LessonsWrapPanel") as WrapPanel;
                if (lessonHeader != null)
                {
                    lessonHeader.LessonTitle = oneLevel.Title;
                    lessonHeader.LessonNumber = "lesson " + oneLevel.Ordinal.ToString();
                }
                foreach (var oneLesson in lessonsCollection)
                {
                    var currentEducUserProgress = oneLesson.EducationUsersProgresses.FirstOrDefault(oneEducProg => oneEducProg.UserId == UserController.CurrentUser.Id);
                    
                    if (currentEducUserProgress != null)
                    {

                        var lessonButton = CreateEducationResultLessonNumberButton(oneLesson, currentEducUserProgress);
                        lessonBodyWrapPanel?.Children.Add(lessonButton);
                    }

                    else
                    {
                        if (_isCurrentLesson)
                        {
                            var currentButton = CreateEducationResultCurrentButton(oneLesson);
                            lessonBodyWrapPanel?.Children.Add(currentButton);
                        }
                        else
                        {
                            var lockButton = CreateEducationResultsLockButton(oneLesson);
                            lessonBodyWrapPanel?.Children.Add(lockButton);
                            
                        }
                    }
                }
               
                MainStackPanel.Children.Add(educationResultLessonBlock);

            }
            ValueProgressBar = _model.GetPercentOfCompletedLessons();




        }

        //комадна продолжения обучения(continue button)
        private void OnContinueEducationCommand(object param)
        {
            if (_currentLessonButton != null)
            {
                FrameMediator.DisplayTypingTutorPage();
            }

        }
        //команда начала урока(клик по урокy)
        private void OnStartLessonCommand(object param)
        {
            IEducationResultLessonButton button = param as IEducationResultLessonButton;
            if (button is not null)
            {
                FrameMediator.DisplayTypingTutorPage();

            }
            else
            {
                MessageBox.Show("Button is null!");
            }

        }
        #endregion

        //command predicate
        #region 
        private bool CanUpdateHeaderCommandExecute(object param)
        {
            return true;
        }
        private bool CanStartLessonCommandExecute(object param)
        {
            return true;
        }

        #endregion
        private void LessonButton_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var button = sender as IEducationResultLessonButton;
            if (button != null)
            {
                _currentLessonButton = button;
                UserController.CurrentUserEducationProgress = button.EducationUserProgress;
                UserController.CurrentLesson = button.EducationLesson;
                StartLessonCommand.Execute(button);
            }
            else
            {
                MessageBox.Show("Button is null", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private EducationResultsLessonNumberButton CreateEducationResultLessonNumberButton(EnglishLayoutLesson lesson, EducationUsersProgress educationUsersProgress)
        {
            var lessonButton = new EducationResultsLessonNumberButton(lesson, educationUsersProgress);
            lessonButton.Style = _lessonButtonUserElementStyle;
            lessonButton.MouseLeftButtonDown += LessonButton_MouseClick;
            lessonButton.LessThanTwoTyposCircleBackground = (educationUsersProgress.IsLessThanTwoErrorsCompleted) ? System.Windows.Media.Brushes.Orange : System.Windows.Media.Brushes.Silver;
            lessonButton.WithoutErrorsCircleBackground = (educationUsersProgress.IsWithoutErrorsCompleted) ? System.Windows.Media.Brushes.ForestGreen : System.Windows.Media.Brushes.Silver;
            lessonButton.SpeedCircleBackground = (educationUsersProgress.IsSpeedCompleted) ? System.Windows.Media.Brushes.Blue : System.Windows.Media.Brushes.Silver;
            return lessonButton;
        }
        private EducationResultsCurrentLessonButton CreateEducationResultCurrentButton(EnglishLayoutLesson lesson)
        {
            var currentButton = new EducationResultsCurrentLessonButton(lesson);
            currentButton.Style = _lessonButtonUserElementStyle;
            currentButton.MouseLeftButtonDown += LessonButton_MouseClick;
            _isCurrentLesson = false;
            _currentLessonButton = currentButton;
            return currentButton;
        }
        private EducationResultsLockButton CreateEducationResultsLockButton(EnglishLayoutLesson lesson)
        {
           return new EducationResultsLockButton(lesson);
        }
    }
}
