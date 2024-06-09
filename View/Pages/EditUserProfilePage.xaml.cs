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
using CourseProjectKeyboardApplication.ViewModel;

namespace CourseProjectKeyboardApplication.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditUserProfilPage.xaml
    /// </summary>
    public partial class EditUserProfilPage : Page
    {
        private EditUserProfilePageViewModel _viewModel;

        public EditUserProfilPage()
        {
            InitializeComponent();
            _viewModel = EditUserProfilePageViewModel.Instance();
            this.DataContext= _viewModel;
        }
    }
}
