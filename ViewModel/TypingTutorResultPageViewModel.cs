using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.View.CustomControls.TypingTutorResultControls;
using CourseProjectKeyboardApplication.View.Pages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CourseProjectKeyboardApplication.ViewModel
{

    public class TypingTutorResultPageViewModel : ViewModelBase
    {
        private string _lessonResultStr;
        private StackPanel _achivementsStackPanel;
        private int _typingTutorSpeed;
        private int _missclickCount;
        private SolidColorBrush _lessTwoMissclickBrush;
        private SolidColorBrush _withoutMissclickBrush;
        private SolidColorBrush _speedBrush;
        private static TypingTutorResultPageViewModel _instance;
        private TypingTutorResultPageModel _typingTutorPageModel;

        private TypingTutorResultPageViewModel(int typingTutorSpeed, int missclickCount)
        {
            _typingTutorPageModel = new TypingTutorResultPageModel(typingTutorSpeed, missclickCount);
            _typingTutorSpeed = typingTutorSpeed;
            _missclickCount = missclickCount;
            LessonResultStr = $"{_typingTutorSpeed} wpm, {_missclickCount} errors!";

        }
        public static TypingTutorResultPageViewModel Instance(int typingTutorSpeed, int missclickCount)
        {
            _instance ??= new TypingTutorResultPageViewModel(typingTutorSpeed, missclickCount);

            return _instance;
        }
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



        #region 
        public StackPanel AchivementStackPanel
        {
            get { return _achivementsStackPanel; }
            set
            {
                _achivementsStackPanel = value;
                InitAchivementStackPanel();
            }
        }
        public string LessonResultStr
        {
            get { return _lessonResultStr; }
            init
            {
                _lessonResultStr = value;
                OnPropertyChanged(nameof(LessonResultStr));
            }
        }

        private void InitAchivementStackPanel()
        {

            AchivementStackPanel.Children.Clear();
            var lessTwoMistakeAchivementBlock = new AchivementBlock();
            var withoutMistakeAchivementBlock = new AchivementBlock();
            var speedAchivementBlock = new AchivementBlock();
            lessTwoMistakeAchivementBlock.AchivementText = "less than 2 typos";
            withoutMistakeAchivementBlock.AchivementText = "exercise without typos";
            speedAchivementBlock.AchivementText = "speed more than 21 wpm";
            if (_typingTutorPageModel.IsExecuteWithoutMisclickCondition())
            {
                withoutMistakeAchivementBlock.AchivementImageSource = Application.Current.Resources["GoldTarget"] as BitmapImage;
                withoutMistakeAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Green;
                WithoutMissclickBrush = System.Windows.Media.Brushes.Green;

            }
            else
            {
                withoutMistakeAchivementBlock.AchivementImageSource = Application.Current.Resources["LightGrayTarget"] as BitmapImage;
                withoutMistakeAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Silver;
                WithoutMissclickBrush = System.Windows.Media.Brushes.Silver;
            }
            if (_typingTutorPageModel.IsExecuteLessTwoErrorCondition())
            {
                lessTwoMistakeAchivementBlock.AchivementImageSource = Application.Current.Resources["GoldStar"] as BitmapImage;
                lessTwoMistakeAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Orange;
                LessTwoMissclickBrush = System.Windows.Media.Brushes.Orange;

            }
            else
            {
                lessTwoMistakeAchivementBlock.AchivementImageSource = Application.Current.Resources["LightGrayStar"] as BitmapImage;
                lessTwoMistakeAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Silver;
                LessTwoMissclickBrush = System.Windows.Media.Brushes.Silver;
            }
            if (_typingTutorPageModel.IsExecuteWithoutMisclickCondition())
            {
                speedAchivementBlock.AchivementImageSource = Application.Current.Resources["GoldFlash"] as BitmapImage;
                speedAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Blue;
                SpeedBrush = System.Windows.Media.Brushes.Blue;

            }
            else
            {
                speedAchivementBlock.AchivementImageSource = Application.Current.Resources["LightGrayFlash"] as BitmapImage;
                speedAchivementBlock.AchivementTextForeground = System.Windows.Media.Brushes.Silver;
                SpeedBrush = System.Windows.Media.Brushes.Silver;
            }
            AchivementStackPanel.Children.Add(lessTwoMistakeAchivementBlock);
            AchivementStackPanel.Children.Add(withoutMistakeAchivementBlock);
            AchivementStackPanel.Children.Add(speedAchivementBlock);


        }
        #endregion

    }
}
