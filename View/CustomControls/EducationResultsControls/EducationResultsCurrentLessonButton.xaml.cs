using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Interfaces;
using CourseProjectKeyboardApplication.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProjectKeyboardApplication.View.CustomControls.EducationResults
{
    /// <summary>
    /// Логика взаимодействия для EducationResultsCurrentLessonButton.xaml
    /// </summary>
    public partial class EducationResultsCurrentLessonButton : UserControl, IEducationResultLessonButton
    {
        public string LessonNumber
        {
            get { return (string)GetValue(LessonNumberProperty); }
            set { SetValue(LessonNumberProperty, value); }
        }
        public ImageSource LeftArrowImageSource
        {
            get => (ImageSource)GetValue(LeftArrowImageSourceProperty);
            set { SetValue(LeftArrowImageSourceProperty, value); }

        }

        public static readonly DependencyProperty LessonNumberProperty =
            DependencyProperty.Register("LessonNumber", typeof(string), typeof(EducationResultsCurrentLessonButton), new PropertyMetadata(" "));
        public static readonly DependencyProperty LeftArrowImageSourceProperty =
            DependencyProperty.Register(nameof(LeftArrowImageSource), typeof(ImageSource), typeof(EducationResultsCurrentLessonButton), new PropertyMetadata(null));
        public EnglishLayoutLesson EducationLesson { get; set; }
        public EducationUsersProgress EducationUserProgress { get; set; }
        public EducationResultsCurrentLessonButton(EnglishLayoutLesson educationLesson, EducationUsersProgress educationUsersProgress = null)
        {
            InitializeComponent();
            EducationLesson = educationLesson;
            EducationUserProgress = educationUsersProgress;
            LessonNumber = educationLesson.Ordinal.ToString();
            LeftArrowImageSource = AppImageSourceProvider.LeftArrowCurrentLessonImageSource;
            DataContext = this;
        }
    }
}
