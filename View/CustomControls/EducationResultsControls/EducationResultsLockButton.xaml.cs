using CourseProjectKeyboardApplication.Entities;
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
    /// Логика взаимодействия для EducationResultsLockButton.xaml
    /// </summary>
    public partial class EducationResultsLockButton : UserControl,IEducationResultLessonButton
    {
        public string LessonNumber { 
            get { return(string)GetValue(LessonNumberProperty);}
            set { SetValue(LessonNumberProperty, value); }
        }

        public EducationLesson EducationLesson { get; set; }

        public static readonly DependencyProperty LessonNumberProperty =
            DependencyProperty.Register("LessonNumber",typeof(string),typeof(EducationResultsLockButton),new PropertyMetadata(" "));
        public EducationResultsLockButton(EducationLesson educationLesson)
        {
            InitializeComponent();
            EducationLesson = educationLesson;
            LessonNumber = educationLesson.Ordinal.ToString();
            DataContext = this;
        }
    }
}
