using System;
using System.CodeDom;
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

namespace CourseProjectKeyboardApplication.View.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для TypingStatLine.xaml
    /// </summary>
    public partial class TypingStatLine : UserControl
    {
        /// <summary>
        /// Speed value
        /// </summary>
        
        public string SpeedValue { 
            get { return (string)GetValue(SpeedValueProperty); }
            set { SetValue(SpeedValueProperty, value);}
        }
        public static readonly DependencyProperty SpeedValueProperty =
            DependencyProperty.Register("SpeedValue", typeof(string), typeof(TypingStatLine), new FrameworkPropertyMetadata("0"));
        /// <summary>
        /// Speed unit value 
        /// </summary>
        public string SpeedUnitValue
        {
            get { return (string)GetValue(SpeedUnitValueProperty);}
            set {  SetValue(SpeedUnitValueProperty, value);}
        }
        public static readonly DependencyProperty SpeedUnitValueProperty =
            DependencyProperty.Register("SpeedUnitValue", typeof(string), typeof(TypingStatLine), new FrameworkPropertyMetadata("wpm"));
        /// <summary>
        /// Accuracy value
        /// </summary>
        
        public string AccuracyValue {
            get { return(string)GetValue(AccuracyValueProperty); }
            set { SetValue(AccuracyValueProperty, value);}
        }
        public static readonly DependencyProperty AccuracyValueProperty =
            DependencyProperty.Register("AccuracyValue", typeof(string), typeof(TypingStatLine), new FrameworkPropertyMetadata("0"));

        /// <summary>
        /// Accuracy unit 
        /// </summary>

        public string AccuracyUnitValue
        {
            get { return(string)GetValue(AccuracyUnitValueProperty); }
            set { SetValue(AccuracyUnitValueProperty, value);}
        }
        public static readonly DependencyProperty AccuracyUnitValueProperty =
            DependencyProperty.Register("AccuracyUnitValue", typeof(string), typeof(TypingStatLine), new PropertyMetadata("%"));

        /// <summary>
        /// Date value
        /// </summary>     
        public string DateValue
        {
            get { return (string)GetValue(DateValueProperty); }
            set { SetValue(DateValueProperty, value);}
        }
        public static readonly DependencyProperty DateValueProperty =
            DependencyProperty.Register("DateValue", typeof(string), typeof(TypingStatLine), new PropertyMetadata("-"));

        public TypingStatLine()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
