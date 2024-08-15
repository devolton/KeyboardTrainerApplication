using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.View.CustomControls.TypingTutorResultControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CourseProjectKeyboardApplication.Shared.Mediators;
using System.Xml.XPath;
using System.Runtime.CompilerServices;

namespace CourseProjectKeyboardApplication.ViewModel
{

    public class TypingTutorResultPageViewModel : ViewModelBase
    {
        private string _lessonResultStr = string.Empty;
        private StackPanel _achivementsStackPanel;

        private SolidColorBrush _lessTwoMissclickBrush;
        private SolidColorBrush _withoutMissclickBrush;
        private SolidColorBrush _speedBrush;

        private static TypingTutorResultPageViewModel _instance;
        private TypingTutorResultPageModel _model;

        private ICommand _loadedPageCommand;
        private ICommand _tryAgainLessonCommand;
        private ICommand _nextLessonCommand;

        private bool _isNextLessonButtonEnabled = true;

        private TypingTutorResultPageViewModel()
        {
            _model = new TypingTutorResultPageModel();
            _tryAgainLessonCommand = new RelayCommand(OnTryAgainLessonCommand);
            _nextLessonCommand = new RelayCommand(OnNextLessonCommand, CanExecuteNextLessonCommand);
            _loadedPageCommand = new RelayCommand(OnLoadedPageCommand);

        }
        public static TypingTutorResultPageViewModel Instance()
        {
            _instance ??= new TypingTutorResultPageViewModel();

            return _instance;
        }
        //Properties
        #region
        public ICommand TryAgainLessonCommand => _tryAgainLessonCommand;
        public ICommand NextLessonCommand => _nextLessonCommand;
        public ICommand LoadedPageCommand => _loadedPageCommand;

        public SolidColorBrush LessTwoMissclickBrush
        {
            get { return _lessTwoMissclickBrush; }
            set
            {
                _lessTwoMissclickBrush = value;
                OnPropertyChanged(nameof(LessTwoMissclickBrush));
            }
        }
        public SolidColorBrush WithoutMissclickBrush
        {
            get { return _withoutMissclickBrush; }
            set
            {
                _withoutMissclickBrush = value;
                OnPropertyChanged(nameof(WithoutMissclickBrush));
            }
        }
        public SolidColorBrush SpeedBrush
        {
            get { return _speedBrush; }
            set
            {
                _speedBrush = value;
                OnPropertyChanged(nameof(SpeedBrush));
            }
        }

        public StackPanel AchivementStackPanel
        {
            get { return _achivementsStackPanel; }
            set
            {
                _achivementsStackPanel = value;
            }
        }
        public string LessonResultStr
        {
            get { return _lessonResultStr; }
            set
            {
                _lessonResultStr = value;
                OnPropertyChanged(nameof(LessonResultStr));
            }
        }
        public bool IsNextLessonButtonEnabled
        {
            get => _isNextLessonButtonEnabled;
            set
            {
                _isNextLessonButtonEnabled = value;
                OnPropertyChanged(nameof(IsNextLessonButtonEnabled));
            }
        }
        #endregion
        //command
        #region
        private void OnTryAgainLessonCommand(object param)
        {
            FrameMediator.DisplayTypingTutorPage();
        }
        private void OnNextLessonCommand(object param)
        {
            _model.SetNextEducationUserProgress();
            FrameMediator.DisplayTypingTutorPage();
        }
        private void OnLoadedPageCommand(object param)
        {
            _model.InitData();
            LessonResultStr = _model.GetLessonResultStr();
            if(AchivementStackPanel != null)
            {
                InitAchivementStackPanel();
            }
            else
            {
                MessageBox.Show("Achivement stack panel is null!");
            }
        }

        #endregion
        //command predicate 
        #region
         private bool CanExecuteNextLessonCommand(object param)
        {
            return _model.IsCurrentLessonNotLast();
        }
        #endregion
        private void InitAchivementStackPanel()
        {

            AchivementStackPanel.Children.Clear();
            var lessTwoMistakeAchivementBlock = GetLessTwoMistakeAchivementBlock();
            var withoutMistakeAchivementBlock = GetWithoutMistakeAchivementBlock();
            var speedAchivementBlock = GetSpeedAchivementBlock();
            AchivementStackPanel.Children.Add(lessTwoMistakeAchivementBlock);
            AchivementStackPanel.Children.Add(withoutMistakeAchivementBlock);
            AchivementStackPanel.Children.Add(speedAchivementBlock);
            IsNextLessonButtonEnabled = CanExecuteNextLessonCommand(null);
            _model.UpdateLessonData();


        }
        //Achivement blocks methods generator
        #region 
        private AchivementBlock GetLessTwoMistakeAchivementBlock()
        {
            var lessTwoMistakeAchivementBlock = new AchivementBlock();
            lessTwoMistakeAchivementBlock.AchivementText = _model.GetLessTwoMistakeText();
            if (_model.IsExecuteLessTwoErrorCondition())
            {

                lessTwoMistakeAchivementBlock.AchivementImageSource = _model.GetGoldStarImageSource();
                lessTwoMistakeAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Orange;
                LessTwoMissclickBrush = System.Windows.Media.Brushes.Orange;

            }
            else
            {
                lessTwoMistakeAchivementBlock.AchivementImageSource = _model.GetLightGrayStarImageSource();
                lessTwoMistakeAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Silver;
                LessTwoMissclickBrush = System.Windows.Media.Brushes.Silver;
            }
            return lessTwoMistakeAchivementBlock;
        }
        private AchivementBlock GetWithoutMistakeAchivementBlock()
        {
            var withoutMistakeAchivementBlock= new AchivementBlock();
            withoutMistakeAchivementBlock.AchivementText = _model.GetWithoutMistakeText();
            if (_model.IsExecuteWithoutMisclickCondition())
            {
                withoutMistakeAchivementBlock.AchivementImageSource = _model.GetGoldTargetImageSource();
                withoutMistakeAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Green;
                WithoutMissclickBrush = System.Windows.Media.Brushes.Green;

            }
            else
            {
                withoutMistakeAchivementBlock.AchivementImageSource = _model.GetLightGrayTargetImageSource();
                withoutMistakeAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Silver;
                WithoutMissclickBrush = System.Windows.Media.Brushes.Silver;
            }

            return withoutMistakeAchivementBlock;
        }
        private AchivementBlock GetSpeedAchivementBlock()
        {
            var speedAchivementBlock = new AchivementBlock();
            speedAchivementBlock.AchivementText = _model.GetSpeedText();


            if (_model.IsExecuteSpeedCondition())
            {
                speedAchivementBlock.AchivementImageSource = _model.GetGoldFlashImageSource();
                speedAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Blue;
                SpeedBrush = System.Windows.Media.Brushes.Blue;
            }
            else
            {
                speedAchivementBlock.AchivementImageSource = _model.GetLightGrayFlashImageSource();
                speedAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Silver;
                SpeedBrush = System.Windows.Media.Brushes.Silver;
            }

            return speedAchivementBlock;

        }
        #endregion

    }
}
