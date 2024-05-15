using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProjectKeyboardApplication.View.CustomControls.EducationResults
{
    /// <summary>
    /// Логика взаимодействия для EducationResultsLessonHeader.xaml
    /// </summary>
    public partial class EducationResultsLessonHeader : UserControl
    {
        /// <summary>
        /// Lesson number field
        /// </summary>
        public string LessonNumber
        {
            get { return (string)GetValue(LessonNumberProperty); }
            set { SetValue(LessonNumberProperty, value); }
        }
        public static readonly DependencyProperty LessonNumberProperty =
            DependencyProperty.Register("LessonNumber", typeof(string), typeof(EducationResultsLessonHeader), new PropertyMetadata(" "));
        /// <summary>
        /// Lesson title
        /// </summary>
        public string LessonTitle
        {
            get { return (string)GetValue(LessonTitleProperty); }
            set { SetValue(LessonTitleProperty, value); }
        }
        public static readonly DependencyProperty LessonTitleProperty =
            DependencyProperty.Register("LessonTitle", typeof(string), typeof(EducationResultsLessonHeader), new PropertyMetadata("Typing lesson"));
        public EducationResultsLessonHeader()
        {
            InitializeComponent();
            DataContext = this;

        }
    }
}
