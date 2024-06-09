using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.ViewModel;

namespace CourseProjectKeyboardApplication.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для TypingCertificationResultsPage.xaml
    /// </summary>
    public partial class TypingCertificationResultsPage : Page
    {
        private TypingCertificationResultPageViewModel _typingCertificationResultPageViewModel;
 
        public TypingCertificationResultsPage()
        {
            InitializeComponent();
            _typingCertificationResultPageViewModel = TypingCertificationResultPageViewModel.Instance();
            DataContext = _typingCertificationResultPageViewModel;




        }
    }
}
