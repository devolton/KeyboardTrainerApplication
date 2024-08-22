using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Interfaces;
using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Shared.Services;
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
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class EducationResultsPageViewModel : ViewModelBase
    {
        private IEducationResultsPageModel _model;

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
        private ImageSource _studyIconImageSource;

        private IEducationResultLessonButton _currentLessonButton;
        private EducationResultsPageViewModel()
        {
            _model = new EducationResultsPageModel();
            _initializationCommand = new RelayCommand(OnInitializationCommand);
            _continueEducationCommand = new RelayCommand(OnContinueEducationCommand);
            _startLessonCommand = new RelayCommand(OnStartLessonCommand);
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
        public ImageSource StudyIconImageSource
        {
            get => _studyIconImageSource;
            set
            {
                _studyIconImageSource = value;
                OnPropertyChanged(nameof(StudyIconImageSource));
            }
        }
        #endregion

        //command
        #region
        /// <summary>
        /// Intializing page 
        /// </summary>
        /// <param name="param">MainStackPanel</param>
        private async void OnInitializationCommand(object param)  //возможно разбить на мелкие функции
        {
            _mainStackPanel = param as StackPanel;
      
            StudyIconImageSource ??= _model.GetStudyIconImageSourceAsync();
            _isCurrentLesson = true;
            if (MainStackPanel.Children.Count > 2)
                MainStackPanel.Children.RemoveRange(2, MainStackPanel.Children.Count - 2);
            LevelProgressHeaderStr = _model.GetCurrentLevelHeaderStr();
            LanguageLayoutTypeHeaderStr = _model.GetLanguageLayoutTypeHeaderStr();
            var levelsCollection = await _model.GetLevelsAsync();
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
                    EducationUsersProgress? currentEducUserProgress = EducationUsersProgressService.GetEducationProgressByLessonId(oneLesson.Id);
                    if (currentEducUserProgress is null)
                    {
                        currentEducUserProgress = oneLesson.EducationUsersProgresses.FirstOrDefault(oneEducProg => oneEducProg.UserId == KeyboardAppEducationProgressController.CurrentUser.Id);
                    }
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
            ValueProgressBar = await _model.GetPercentOfCompletedLessonsAsync();




        }

       /// <summary>
       /// Command for continue education which redirect user to TypingTutor page
       /// </summary>
       /// <param name="param">NULL</param>
        private void OnContinueEducationCommand(object param)
        {
            if (_currentLessonButton != null)
            {
                FrameMediator.DisplayTypingTutorPage();
            }

        }
        /// <summary>
        /// Command for starting lesson which user selected and redirect to TypingTutor page
        /// </summary>
        /// <param name="param"></param>
        private void OnStartLessonCommand(object param)
        {
            IEducationResultLessonButton button = param as IEducationResultLessonButton;
            if (button is not null)
            {
                FrameMediator.DisplayTypingTutorPage();

            }


        }
        #endregion

        /// <summary>
        /// Setting up lesson options and delegating to StartLessonCommand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LessonButton_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var button = sender as IEducationResultLessonButton;
            if (button != null)
            {
                _currentLessonButton = button;
                KeyboardAppEducationProgressController.CurrentUserEducationProgress = button.EducationUserProgress;
                KeyboardAppEducationProgressController.CurrentLesson = button.EducationLesson;
                StartLessonCommand.Execute(button);
            }
        }

        /// <summary>
        /// Genatate EducationResultsLessonNumberButton
        /// </summary>
        /// <param name="lesson">EnglishLayoutLesson entity which will be bound to this button</param>
        /// <param name="educationUsersProgress">EducationUsersProgress which will be bound to this button</param>
        /// <returns>EducationResultsLesssonNumberButton entity</returns>
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

        /// <summary>
        /// Genarate EducationResultsCurrentLessonButton
        /// </summary>
        /// <param name="lesson">EnglishLayoutLesson entity which will be bound to this button</param>
        /// <returns>EducationResultsCurrentButton entity</returns>
        private EducationResultsCurrentLessonButton CreateEducationResultCurrentButton(EnglishLayoutLesson lesson)
        {
            var currentButton = new EducationResultsCurrentLessonButton(lesson);
            currentButton.Style = _lessonButtonUserElementStyle;
            currentButton.MouseLeftButtonDown += LessonButton_MouseClick;
            _isCurrentLesson = false;
            _currentLessonButton = currentButton;
            return currentButton;
        }
        /// <summary>
        /// Genarate EducationResultLockButton
        /// </summary>
        /// <param name="lesson">EnglishLayoutLesson entity which will be bound to this button</param>
        /// <returns></returns>
        private EducationResultsLockButton CreateEducationResultsLockButton(EnglishLayoutLesson lesson)
        {
            return new EducationResultsLockButton(lesson);
        }
    }
}
