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

namespace CourseProjectKeyboardApplication.View.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для KeyboardControlItemTextBlock.xaml
    /// </summary>
    public partial class KeyboardControlItemTextBlock : UserControl, IKeyboardItem
    {
        public string KeyTag
        {
            get { return (string)GetValue(KeyTagProperty); }
            set { SetValue(KeyTagProperty, value);}
        }
        public static readonly DependencyProperty KeyTagProperty =
            DependencyProperty.Register(nameof(KeyTag),typeof(string), typeof(KeyboardControlItemTextBlock), new PropertyMetadata(" "));
        /// <summary>
        /// TextValue
        /// </summary>
        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }
        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register("TextValue", typeof(string), typeof(KeyboardControlItemTextBlock), new PropertyMetadata(string.Empty));
        /// <summary>
        /// Margin
        /// </summary>
        /// 
        public Thickness MarginValue
        {
            get { return (Thickness)GetValue(MarginValueProperty); }
            set { SetValue(MarginValueProperty, value); }
        }
        public static readonly DependencyProperty MarginValueProperty =
            DependencyProperty.Register("MarginValue", typeof(Thickness), typeof(KeyboardControlItemTextBlock), new PropertyMetadata(new Thickness(1)));
        /// <summary>
        /// BackColor
        /// </summary>

        public SolidColorBrush BackColorValue
        {
            get { return (SolidColorBrush)GetValue(BackColorValueProperty); }
            set { SetValue(BackColorValueProperty, value); }
        }
        public static readonly DependencyProperty BackColorValueProperty =
            DependencyProperty.Register("BackColorValue", typeof(SolidColorBrush), typeof(KeyboardControlItemTextBlock), new PropertyMetadata(Brushes.White));
        /// <summary>
        /// Border Thickness
        /// </summary>

        public Thickness BorderThicknessValue
        {
            get { return (Thickness)GetValue(BorderThicknessValueProperty); }
            set { SetValue(BorderThicknessValueProperty, value); }
        }
        public static readonly DependencyProperty BorderThicknessValueProperty =
            DependencyProperty.Register("BorderThicknessValue", typeof(Thickness), typeof(KeyboardControlItemTextBlock), new PropertyMetadata(new Thickness(1)));
        /// <summary>
        /// Font Size
        /// </summary>
        public double FontSizeValue
        {
            get { return (double)GetValue(FontSizeValueProperty); }
            set { SetValue(FontSizeValueProperty, value); }
        }
        public static readonly DependencyProperty FontSizeValueProperty =
            DependencyProperty.Register("FontSizeValue", typeof(double), typeof(KeyboardControlItemTextBlock), new PropertyMetadata((double)22));
        /// <summary>
        /// Foreground
        /// </summary>

        public SolidColorBrush ForegroundValue
        {
            get { return (SolidColorBrush)GetValue(ForegroundValueProperty); }
            set { SetValue(ForegroundValueProperty, value); }
        }
        public static readonly DependencyProperty ForegroundValueProperty =
            DependencyProperty.Register("ForegroundValue", typeof(SolidColorBrush), typeof(KeyboardControlItemTextBlock), new PropertyMetadata(Brushes.Black));


        /// <summary>
        /// Border brush color
        /// </summary>
        public SolidColorBrush BorderBrushValue
        {
            get { return (SolidColorBrush)GetValue(BorderBrushValueProperty); }
            set { SetValue(BorderBrushValueProperty, value); }
        }

        public static DependencyProperty BorderBrushValueProperty =
            DependencyProperty.Register("BorderBrushValue", typeof(SolidColorBrush), typeof(KeyboardControlItemTextBlock), new PropertyMetadata(Brushes.Silver));
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
            DependencyProperty.Register("ShadowEffectOpacity", typeof(double), typeof(KeyboardControlItemTextBlock), new PropertyMetadata((double)0));

        /// <summary>
        /// Prorerty which defines whether the element is in focus
        /// </summary>

        public bool IsFocusKeyboardItem
        {
            get { return (bool)GetValue(IsFocusKeyboardItemProperty); }
            set { SetValue(IsFocusKeyboardItemProperty, value); }
        }
        public static readonly DependencyProperty IsFocusKeyboardItemProperty =
            DependencyProperty.Register("IsFocusKeyboardItem", typeof(bool), typeof(KeyboardControlItemTextBlock), new PropertyMetadata(false));

        /// <summary>
        /// Property which defines where keyboard item was pushed incorectly
        /// </summary>
        public bool IsErrorPushedKeyboardItem
        {
            get { return (bool)GetValue(IsErrorPushedKeyboardItemProperty); }
            set { SetValue(IsErrorPushedKeyboardItemProperty, value); }
        }
        public static readonly DependencyProperty IsErrorPushedKeyboardItemProperty =
            DependencyProperty.Register("IsErrorPushedKeyboardItem", typeof(bool), typeof(KeyboardControlItemTextBlock), new PropertyMetadata(false));
        public KeyboardControlItemTextBlock()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
