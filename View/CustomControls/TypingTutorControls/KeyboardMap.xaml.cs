using CourseProjectKeyboardApplication.ViewModel;
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
    /// Логика взаимодействия для KeyboardMap.xaml
    /// </summary>
    public partial class KeyboardMap : UserControl
    {
        private TypingTutorPageViewModel _typingTutorViewModel;
        public KeyboardMap()
        {
            InitializeComponent();
            _typingTutorViewModel = TypingTutorPageViewModel.Instance();
            DataContext = _typingTutorViewModel;
            _typingTutorViewModel.KeyboardGrid = KeyboardGrid;
        }

    }
}
