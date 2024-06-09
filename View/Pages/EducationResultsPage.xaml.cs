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

namespace CourseProjectKeyboardApplication.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EducationResultsPage.xaml
    /// </summary>
    public partial class EducationResultsPage : Page
    {
        private EducationResultsPageViewModel _viewModel;
        public EducationResultsPage()
        {
            _viewModel = EducationResultsPageViewModel.Instance();
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.InitializationCommand.Execute(EducationResultMainStackPanel);
        }
    }
}
