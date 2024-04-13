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
    /// Логика взаимодействия для KeyboardItemLabel.xaml
    /// </summary>
    public partial class KeyboardItemTextBlock : UserControl
    {
        public string TextValue { get; set; }
        public SolidColorBrush BackColorValue { get; set; }
        public KeyboardItemTextBlock()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
