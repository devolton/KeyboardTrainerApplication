using CourseProjectKeyboardApplication.Tools;
using CourseProjectKeyboardApplication.View.Pages;
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

namespace CourseProjectKeyboardApplication.View.CustomControls.TypingTutorResultControls
{
    /// <summary>
    /// Логика взаимодействия для AchivementBlock.xaml
    /// </summary>
    public partial class AchivementBlock : UserControl
    {
        public string AchivementText
        {
            get { return (string)GetValue(AchivementTextProperty); }
            set { SetValue(AchivementTextProperty, value); }
        }
        public static readonly DependencyProperty AchivementTextProperty =
            DependencyProperty.Register(nameof(AchivementText), typeof(string), typeof(TypingTutorResultPage), new PropertyMetadata(" "));
        public ImageSource AchivementImageSource
        {
            get { return (ImageSource)GetValue(AchivementImageSourceProperty); }
            set
            {
                SetValue(AchivementImageSourceProperty, value);
            }
        }
        public static readonly DependencyProperty AchivementImageSourceProperty =
            DependencyProperty.Register(nameof(AchivementImageSource), typeof(ImageSource), typeof(TypingTutorResultPage), new PropertyMetadata(null));
        public SolidColorBrush AchivementTextForeground
        {
            get { return (SolidColorBrush)GetValue(AchivementTextForegroundProperty); }
            set { SetValue(AchivementTextForegroundProperty, value); }
        }
        public static readonly DependencyProperty AchivementTextForegroundProperty =
            DependencyProperty.Register(nameof(AchivementTextForeground), typeof(SolidColorBrush), typeof(TypingTutorResultPage), new PropertyMetadata(Brushes.Silver));

        public AchivementBlock()
        {

            InitializeComponent();
            DataContext = this;



        }



    }
}
