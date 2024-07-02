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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProjectKeyboardApplication.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для TypingTutorResultPage.xaml
    /// </summary>
    public partial class TypingTutorResultPage : Page
    {
        private TypingTutorResultPageViewModel _typingTutorResultPageViewModel;
        public TypingTutorResultPage()
        {
            _typingTutorResultPageViewModel = TypingTutorResultPageViewModel.Instance();
            InitializeComponent();

            DataContext = _typingTutorResultPageViewModel;
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _typingTutorResultPageViewModel.AchivementStackPanel = AchevementBlocksStackPanel;
            _typingTutorResultPageViewModel.LoadedPageCommand.Execute(null);
        }
    }
}
