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

namespace CourseProjectKeyboardApplication.View.CustomControls.EditUserProfileControls
{
    /// <summary>
    /// Логика взаимодействия для EditUserProfileBodyBlock.xaml
    /// </summary>
    public partial class EditUserProfileBodyBlock : UserControl
    {
        private EditUserProfilePageViewModel _viewModel;
        public EditUserProfileBodyBlock()
        {
            InitializeComponent();
            _viewModel = EditUserProfilePageViewModel.Instance();
            DataContext = _viewModel;
            _viewModel.PasswordBox = PasswordBox;
            _viewModel.ConfirmPasswordBox = ConfirmPasswordBox;

        }

        private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            _viewModel.Password = passwordBox.Password;
        }

        private void ConfirmPasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            _viewModel.ConfirmPassword = passwordBox.Password;

        }
    }
}
