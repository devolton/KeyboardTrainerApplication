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
    /// Логика взаимодействия для TypingTestTextDisplayerBlock.xaml
    /// </summary>
    public partial class TypingTestTextDisplayerBlock : UserControl
    {
        private TypingTestPageViewModel _typingTestViewModel;
        public TypingTestTextDisplayerBlock()
        {
            _typingTestViewModel = TypingTestPageViewModel.Instance();
            InitializeComponent();
            DataContext = _typingTestViewModel;
        }

        private void WordsTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            _typingTestViewModel.TestSetupCommand.Execute(WordsTextBlock);
        }
    }
}
