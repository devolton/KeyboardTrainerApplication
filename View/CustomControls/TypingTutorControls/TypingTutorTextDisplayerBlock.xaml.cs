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
    /// Логика взаимодействия для TextDisplayerBlock.xaml
    /// </summary>
    public partial class TextDisplayerBlock : UserControl
    {
        private TypingTutorPageViewModel _typingTutorPageViewModel;
        public TextDisplayerBlock()
        {
            InitializeComponent();
            _typingTutorPageViewModel = TypingTutorPageViewModel.Instance();
            DataContext = _typingTutorPageViewModel;
            
        }

   

        private void WordsTextBox_Loaded(object sender, RoutedEventArgs e)
        {

            _typingTutorPageViewModel.GenerateRunsBlocksCommand.Execute(WordsTextBox);
        }


    }
}
