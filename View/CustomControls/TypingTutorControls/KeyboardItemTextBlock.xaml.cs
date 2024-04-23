using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для KeyboardItemLabel.xaml
    /// </summary>
    public partial class KeyboardItemTextBlock : UserControl
    {
        public FontWeight FontWeightValue { get; set; }
        public static readonly DependencyProperty FontWeightValueProperty =
            DependencyProperty.Register("FontWeightValue", typeof(FontWeight), typeof(KeyboardItemTextBlock), new PropertyMetadata(new FontWeight()));
        /// <summary>
        /// Margin 
        /// </summary>
        public Thickness MarginValue
        {
            get { return (Thickness)GetValue(MarginValueProperty); }
            set { SetValue(MarginValueProperty, value); }
        }
        public static readonly DependencyProperty MarginValueProperty =
            DependencyProperty.Register("MarginValue", typeof(Thickness), typeof(KeyboardItemTextBlock), new PropertyMetadata(new Thickness(5)));

        /// <summary>
        /// Text
        /// </summary>
        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }
        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register("TextValue", typeof(string), typeof(KeyboardItemTextBlock), new PropertyMetadata(""));


        /// <summary>
        /// BackgroundColor
        /// </summary>
        public SolidColorBrush BackColorValue
        {
            get { return (SolidColorBrush)GetValue(BackColorValueProperty); }
            set { SetValue(BackColorValueProperty, value); }
        }
        public static readonly DependencyProperty BackColorValueProperty =
           DependencyProperty.Register("BackColorValue", typeof(SolidColorBrush), typeof(KeyboardItemTextBlock), new PropertyMetadata(Brushes.White));

        /// <summary>
        /// Border thickness
        /// </summary>
        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(KeyboardItemTextBlock), new PropertyMetadata(new Thickness(1)));

        /// <summary>
        /// FontSize
        /// </summary>
        public double FontSizeValue
        {
            get { return (double)GetValue(FontSizeValueProperty); }
            set { SetValue(FontSizeValueProperty, value); }
        }
        public static DependencyProperty FontSizeValueProperty =
            DependencyProperty.Register("FontSizeValue", typeof(double), typeof(KeyboardItemTextBlock), new PropertyMetadata((double)18));

        /// <summary>
        /// Foreground color
        /// </summary>
        public SolidColorBrush ForegroundColorValue
        {
            get { return (SolidColorBrush)GetValue(ForegroundColorValueProperty); }
            set { SetValue(ForegroundColorValueProperty, value); }
        }

        public static DependencyProperty ForegroundColorValueProperty =
            DependencyProperty.Register("ForegroundColorValue", typeof(SolidColorBrush), typeof(KeyboardItemTextBlock), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// Border brush color
        /// </summary>
        public SolidColorBrush BorderBrushValue
        {
            get { return (SolidColorBrush)GetValue(BorderBrushValueProperty); }
            set { SetValue(BorderBrushValueProperty, value); }
        }

        public static DependencyProperty BorderBrushValueProperty =
            DependencyProperty.Register("BorderBrushValue", typeof(SolidColorBrush), typeof(KeyboardItemTextBlock), new PropertyMetadata(Brushes.Silver));

        /// <summary>
        /// DropShadowEffectOpacicy
        /// </summary>
        public double ShadowEffectOpacity
        {
            get { return (double)(GetValue(ShadowEffectOpacityProperty)); }
            set
            {
                if (value >= 0 && value <= 1)
                    SetValue(ShadowEffectOpacityProperty, value);
                else
                    throw new ArgumentException("Invalid property value " + nameof(value));
            }
        }
        public static DependencyProperty ShadowEffectOpacityProperty =
            DependencyProperty.Register("ShadowEffectOpacity", typeof(double), typeof(KeyboardItemTextBlock), new PropertyMetadata((double)0));

        /// <summary>
        /// Prorerty which defines whether the element is in focus
        /// </summary>

        public bool IsFocusKeyboardItem
        {
            get { return (bool)GetValue(IsFocusKeyboardItemProperty); }
            set { SetValue(IsFocusKeyboardItemProperty, value); }
        }
        public static readonly DependencyProperty IsFocusKeyboardItemProperty =
            DependencyProperty.Register("IsFocusKeyboardItem", typeof(bool), typeof(KeyboardItemTextBlock), new PropertyMetadata(false));

        /// <summary>
        /// Property which defines where keyboard item was pushed incorectly
        /// </summary>
        public bool IsErrorPushedKeyboardItem
        {
            get { return (bool)GetValue(IsErrorPushedKeyboardItemProperty); }
            set { SetValue(IsErrorPushedKeyboardItemProperty, value); }
        }
        public static readonly DependencyProperty IsErrorPushedKeyboardItemProperty =
            DependencyProperty.Register("IsErrorPushedKeyboardItem", typeof(bool), typeof(KeyboardItemTextBlock), new PropertyMetadata(false));




        public KeyboardItemTextBlock()
        {
            InitializeComponent();
            DataContext = this;

        }


    }
}
