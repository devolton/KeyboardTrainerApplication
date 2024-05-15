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
    /// Логика взаимодействия для EducationResultsLessonNumberButton.xaml
    /// </summary>
    public partial class EducationResultsLessonNumberButton : UserControl,IEducationResultLessonButton
    {
  
        /// <summary>
        /// Outer circle background
        /// </summary>
        public SolidColorBrush LessThanTwoTyposCircleBackground
        {
            get {; return (SolidColorBrush)(GetValue(LessThanTwoTyposCircleBackgroundProperty)); }
            set { SetValue(LessThanTwoTyposCircleBackgroundProperty, value); }
        }
        public static readonly DependencyProperty LessThanTwoTyposCircleBackgroundProperty =
            DependencyProperty.Register("LessThanTwoTyposCircleBackground", typeof(SolidColorBrush), typeof(EducationResultsLessonNumberButton), new PropertyMetadata(Brushes.Silver));
        /// <summary>
        /// Middle circle background 
        /// </summary>
        public SolidColorBrush WithoutErrorsCircleBackground
        {
            get { return (SolidColorBrush)GetValue(WithoutErrorsCircleBackgroundProperty); }
            set { SetValue(WithoutErrorsCircleBackgroundProperty, value);}
        }
        public static readonly DependencyProperty WithoutErrorsCircleBackgroundProperty =
            DependencyProperty.Register("WithoutErrorsCircleBackground", typeof(SolidColorBrush), typeof(EducationResultsLessonNumberButton), new PropertyMetadata(Brushes.Silver));
        /// <summary>
        /// Interior circle background
        /// </summary>
        public SolidColorBrush SpeedCircleBackground
        {
            get { return(SolidColorBrush) GetValue(SpeedCircleBackgroundProperty); }
            set { SetValue(SpeedCircleBackgroundProperty, value);}
        }
        public static readonly DependencyProperty SpeedCircleBackgroundProperty=
            DependencyProperty.Register("SpeedCircleBackground",typeof(SolidColorBrush),typeof(EducationResultsLessonNumberButton),new PropertyMetadata(Brushes.Silver));
        /// <summary>
        /// Lesson number 
        /// </summary>
         public string LessonNumber {
            get { return(string)GetValue(LessonNumberProperty); }
            set { SetValue(LessonNumberProperty, value);}
        }
        public EducationLesson EducationLesson { get; set; }

        public static readonly DependencyProperty LessonNumberProperty =
           DependencyProperty.Register("LessonNumber", typeof(string), typeof(EducationResultsLessonNumberButton), new PropertyMetadata(" "));
        public EducationResultsLessonNumberButton(EducationLesson educationLesson)
        {
            InitializeComponent();
            EducationLesson = educationLesson;
            LessonNumber = educationLesson.Ordinal.ToString();
            DataContext = this;
        }
    }
}
