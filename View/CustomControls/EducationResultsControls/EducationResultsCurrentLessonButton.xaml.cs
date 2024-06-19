using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Interfaces;
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
    public partial class EducationResultsCurrentLessonButton : UserControl,IEducationResultLessonButton
    {
        public string LessonNumber
        {
            get { return (string)GetValue(LessonNumberProperty); }
            set { SetValue(LessonNumberProperty, value); }
        }

        public static readonly DependencyProperty LessonNumberProperty =
            DependencyProperty.Register("LessonNumber", typeof(string), typeof(EducationResultsCurrentLessonButton), new PropertyMetadata(" "));
        public EnglishLayoutLesson EducationLesson { get; set; }
        public EducationUsersProgress EducationUserProgress { get; set; }
        public EducationResultsCurrentLessonButton(EnglishLayoutLesson educationLesson, EducationUsersProgress educationUsersProgress)
        {
            InitializeComponent();
            EducationLesson= educationLesson;
            EducationUserProgress = educationUsersProgress;
            LessonNumber = educationLesson.Ordinal.ToString();
            DataContext = this;
        }
    }
}
