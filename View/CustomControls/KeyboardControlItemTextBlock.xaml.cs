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
    public partial class KeyboardControlItemTextBlock : UserControl
    {
        public string TextValue { get; set; } = string.Empty;
        public KeyboardControlItemTextBlock()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
