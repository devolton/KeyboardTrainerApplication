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

namespace CourseProjectKeyboardApplication.View.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для LanguageLayotStatisticBlock.xaml
    /// </summary>
    public partial class LanguageLayotStatisticBlock : UserControl
    {
        /// <summary>
        /// Header Color brush
        /// </summary>
        public Color HeaderColor {
            get { return (Color)GetValue(HeaderColorProperty); }
            set { SetValue(HeaderColorProperty,value); }
        }
        public static readonly DependencyProperty HeaderColorProperty =
            DependencyProperty.Register("HeaderColor", typeof(Color),typeof(LanguageLayotStatisticBlock),new PropertyMetadata(Colors.DarkOrange));
        /// <summary>
        /// Body Color brush
        /// </summary>
        public Color BodyColor
        {
            get { return (Color)GetValue(BodyColorProperty); }
            set { SetValue(BodyColorProperty, value); }
        }
        public static readonly DependencyProperty BodyColorProperty =
            DependencyProperty.Register("BodyColor", typeof(Color), typeof(LanguageLayotStatisticBlock), new PropertyMetadata(Colors.Orange));
        /// <summary>
        /// Icon source
        /// </summary>
        public ImageSource IconSource {
            get { return(ImageSource) GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }
        public static readonly DependencyProperty IconSourceProperty=
            DependencyProperty.Register("IconSource",typeof(ImageSource),typeof(LanguageLayotStatisticBlock),new PropertyMetadata(null));
        /// <summary>
        /// Header foreground
        /// </summary>
        public SolidColorBrush HeaderForeground
        {
            get { return(SolidColorBrush)  GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value);}
        }
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register("HeaderForeground", typeof(SolidColorBrush), typeof(LanguageLayotStatisticBlock), new PropertyMetadata(Brushes.White));
        /// <summary>
        /// Header text
        /// </summary>
        public string HeaderText
        {
            get { return(string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value);}
        }
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(LanguageLayotStatisticBlock), new PropertyMetadata(" "));
        /// <summary>
        /// Body stat value
        /// </summary>
        public string StatValue {
            get { return(string)GetValue(StatValueProperty); }
            set { SetValue(StatValueProperty, value);}
        }
        public static readonly DependencyProperty StatValueProperty =
            DependencyProperty.Register("StatValue", typeof(string), typeof(LanguageLayotStatisticBlock), new PropertyMetadata("0"));
        /// <summary>
        /// Body unit value
        /// </summary>
        public string StatUnitValue { 
            get { return (string) GetValue(StatUnitValueProperty); }
            set { SetValue(StatUnitValueProperty, value);}
        }
        public static readonly DependencyProperty StatUnitValueProperty =
            DependencyProperty.Register("StatUnitValue", typeof(string), typeof(LanguageLayotStatisticBlock), new PropertyMetadata("%"));



        

        public LanguageLayotStatisticBlock()
        {
            InitializeComponent();
            DataContext = this;


            
        }
    }
}
