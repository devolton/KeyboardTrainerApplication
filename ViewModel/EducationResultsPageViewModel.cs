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
        private StackPanel _mainStackPanel;
        private static EducationResultsPageViewModel _instance;
        private ICommand _initializationCommand;
        private ICommand _updateLessonStateCommand;
        private ICommand _updateHeaderCommand;
        private ICommand _continueEducationCommand;
        private ICommand _startLessonCommand;
        private EducationResultsPageViewModel()
        {
            _model = new EducationResultsPageModel();
            _initializationCommand = new RelayCommand(OnInitializatioCommand);
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
        #endregion

        //command
        #region
        /// <summary>
        /// команда инициализации
        /// </summary>
        /// <param name="param">MainStackPanel</param>
        private void OnInitializatioCommand(object param)
        {
            _mainStackPanel = param as StackPanel;
            var educatinLevelsList = _model.GetEducationLevelList();

            foreach (var oneLevel in educatinLevelsList)
            {
                var educationResultLessonBlock = new EducationResultsLessonBlock();
                var lessonHeader = (EducationResultsLessonHeader)educationResultLessonBlock.FindName("LessonHeader");
                var lessonBodyWrapPanel = (WrapPanel)educationResultLessonBlock.FindName("LessonsWrapPanel");
                lessonHeader.LessonTitle = oneLevel.Title;
                lessonHeader.LessonNumber = "lesson " + oneLevel.Ordinal.ToString();
                foreach (var oneLesson in oneLevel.LessonsList)
                {
                    if (oneLesson.IsLessonUnlocked)
                    {
                        var lessonButton = new EducationResultsLessonNumberButton(oneLesson);
                        lessonButton.MouseDoubleClick += LessonButton_MouseDoubleClick;
                        lessonButton.LessThanTwoTyposCircleBackground = (oneLesson.IsLessTwoErrorsCompleted) ? System.Windows.Media.Brushes.Orange : System.Windows.Media.Brushes.Silver;
                        lessonButton.WithoutErrorsCircleBackground = (oneLesson.IsWithoutErrorsCompleted) ? System.Windows.Media.Brushes.ForestGreen : System.Windows.Media.Brushes.Silver;
                        lessonButton.SpeedCircleBackground = (oneLesson.IsSpeedConditionCompleted) ? System.Windows.Media.Brushes.Blue : System.Windows.Media.Brushes.Silver;
                        lessonBodyWrapPanel.Children.Add(lessonButton);
                    }
                    else
                    {
                        var lessonButton = new EducationResultsLockButton(oneLesson);
                        lessonBodyWrapPanel.Children.Add(lessonButton);

                    }
                }
                MainStackPanel.Children.Add(educationResultLessonBlock);
            }





        }

        private void LessonButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var button = sender as IEducationResultLessonButton;
            if (button != null)
            {
                MessageBox.Show(button.EducationLesson.Ordinal.ToString());
                    }
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

        }
        //команда начала урока(клик по урокy)
        private void OnStartLessonCommand(object param)
        {

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

    }
}
