using CourseProjectKeyboardApplication.Interfaces;
using CourseProjectKeyboardApplication.Model;
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

        //commands
        private ICommand _initializationCommand;
        private ICommand _updateLessonStateCommand;
        private ICommand _updateHeaderCommand;
        private ICommand _continueEducationCommand;
        private ICommand _startLessonCommand;

        //header fields
        private double _valueProgressBar;
        private string _levelProgressHeaderStr = string.Empty;
        private string _languageLayoutTypeHeaderStr = string.Empty;

        private IEducationResultLessonButton _currentLessonButton;
        private EducationResultsPageViewModel()
        {
            _model = new EducationResultsPageModel();
            _initializationCommand = new RelayCommand(OnInitializationCommand);
            _updateLessonStateCommand = new RelayCommand(OnUpdateLessonStateCommand);
            _updateHeaderCommand = new RelayCommand(OnUpdateHeaderCommand, CanUpdateHeaderCommandExecute);
            _continueEducationCommand = new RelayCommand(OnContinueEducationCommand);
            _startLessonCommand = new RelayCommand(OnStartLessonCommand, CanStartLessonCommandExecute);

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

        //Команда обновления состояния кнопки после завершения урока
        public ICommand UpdateLessonStateCommand => _updateHeaderCommand;
        //команда обновления header после завершения урока
        public ICommand UpdateHeaderCommand => _updateHeaderCommand;
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
        private void OnInitializationCommand(object param)
        {
            _mainStackPanel = param as StackPanel;
            LevelProgressHeaderStr = _model.GetCurrentLevelHeaderStr();
            ValueProgressBar = _model.GetPercentOfCompletedLessons();
            LanguageLayoutTypeHeaderStr = _model.GetLanguageLayoutTypeHeaderStr();
            var levelsCollection = _model.GetLevels();
            foreach (var oneLevel in levelsCollection)
            {
                var educationResultLessonBlock = new EducationResultsLessonBlock();
                var lessonHeader = educationResultLessonBlock.FindName("LessonHeader") as EducationResultsLessonHeader;
                var lessonBodyWrapPanel = educationResultLessonBlock.FindName("LessonsWrapPanel") as WrapPanel;
                if (lessonHeader != null)
                {
                    lessonHeader.LessonTitle = oneLevel.Title;
                    lessonHeader.LessonNumber = "lesson " + oneLevel.Ordinal.ToString();
                }
                foreach (var oneLesson in oneLevel.Lessons)
                {
                    var currentEducUserProgress = oneLesson.EducationUsersProgresses.FirstOrDefault(oneEducProg => oneEducProg.UserId == 1);
                    if (currentEducUserProgress is null)
                    {
                        var lockButton = new EducationResultsLockButton(oneLesson);
                        lessonBodyWrapPanel?.Children.Add(lockButton);
                    }
                    else
                    {
                        //add current lesson button
                        var lessonButton = new EducationResultsLessonNumberButton(oneLesson,currentEducUserProgress);
                        lessonButton.MouseDoubleClick += LessonButton_MouseDoubleClick;
                        lessonButton.LessThanTwoTyposCircleBackground = (currentEducUserProgress.IsLessThanTwoErrorsCompleted) ? System.Windows.Media.Brushes.Orange : System.Windows.Media.Brushes.Silver;
                        lessonButton.WithoutErrorsCircleBackground = (currentEducUserProgress.IsWithoutErrorsCompleted) ? System.Windows.Media.Brushes.ForestGreen : System.Windows.Media.Brushes.Silver;
                        lessonButton.SpeedCircleBackground = (currentEducUserProgress.IsSpeedCompleted) ? System.Windows.Media.Brushes.Blue : System.Windows.Media.Brushes.Silver;
                        lessonBodyWrapPanel?.Children.Add(lessonButton);
                    }
                }
                MainStackPanel.Children.Add(educationResultLessonBlock);
            }
            //init template
            #region
            //foreach (var oneLevel in educatinProgram.Levels)
            //{
            //    var educationResultLessonBlock = new EducationResultsLessonBlock();
            //    var lessonHeader = (EducationResultsLessonHeader)educationResultLessonBlock.FindName("LessonHeader");
            //    var lessonBodyWrapPanel = (WrapPanel)educationResultLessonBlock.FindName("LessonsWrapPanel");
            //    lessonHeader.LessonTitle = oneLevel.Title;
            //    lessonHeader.LessonNumber = "lesson " + oneLevel.Ordinal.ToString();
            //    foreach (var oneLesson in oneLevel.LessonsList)
            //    {
            //        if (oneLesson.IsLessonUnlocked)
            //        {
            //            if (oneLesson.Id == educatinProgram.CurrentLessonId)
            //            {
            //                var button = new EducationResultsCurrentLessonButton(oneLesson);
            //                button.MouseDoubleClick += LessonButton_MouseDoubleClick;
            //                _currentLessonButton = button;
            //                lessonBodyWrapPanel.Children.Add(button);
            //                continue;
            //            }
            //            else
            //            {
            //                var lessonButton = new EducationResultsLessonNumberButton(oneLesson);
            //                lessonButton.MouseDoubleClick += LessonButton_MouseDoubleClick;
            //                lessonButton.LessThanTwoTyposCircleBackground = (oneLesson.IsLessTwoErrorsCompleted) ? System.Windows.Media.Brushes.Orange : System.Windows.Media.Brushes.Silver;
            //                lessonButton.WithoutErrorsCircleBackground = (oneLesson.IsWithoutErrorsCompleted) ? System.Windows.Media.Brushes.ForestGreen : System.Windows.Media.Brushes.Silver;
            //                lessonButton.SpeedCircleBackground = (oneLesson.IsSpeedConditionCompleted) ? System.Windows.Media.Brushes.Blue : System.Windows.Media.Brushes.Silver;
            //                lessonBodyWrapPanel.Children.Add(lessonButton);
            //            }
            //        }
            //        else
            //        {
            //            var lessonButton = new EducationResultsLockButton(oneLesson);
            //            lessonBodyWrapPanel.Children.Add(lessonButton);

            //        }
            //    }
            //    MainStackPanel.Children.Add(educationResultLessonBlock);
            //}
            #endregion




        }



        //команда обновления урока после его успешного завершения
        private void OnUpdateLessonStateCommand(object param)
        {

        }
        //команда обновления header
        private void OnUpdateHeaderCommand(object param)
        {

        }
        //комадна продолжения обучения(continue button)
        private void OnContinueEducationCommand(object param)
        {
            MessageBox.Show("Continue education command executed!");

        }
        //команда начала урока(клик по урокy)
        private void OnStartLessonCommand(object param)
        {
            IEducationResultLessonButton button = param as IEducationResultLessonButton;
            if (button is not null)
            {
                //открытие страницы обучения 

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
        private void LessonButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var button = sender as IEducationResultLessonButton;
            _currentLessonButton = button;
            StartLessonCommand.Execute(button);
        }
    }
}
