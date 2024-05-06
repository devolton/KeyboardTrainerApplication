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
    /// Логика взаимодействия для UserSingInPage.xaml
    /// </summary>
    public partial class UserSingInPage : Page
    {
        private UserSingupPageViewModel _userSingUpViewModel;
        public UserSingInPage()
        {
            InitializeComponent();
            _userSingUpViewModel = new UserSingupPageViewModel();
            DataContext = _userSingUpViewModel;
        }

        private void SingInPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBoxElement = sender as PasswordBox;
            if (passwordBoxElement is not null)
                _userSingUpViewModel.Password = passwordBoxElement.Password;
        }

        private void SingInConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBoxElement = sender as PasswordBox;
            if (passwordBoxElement is not null)
                _userSingUpViewModel.ConfirmPassword = passwordBoxElement.Password;


        }
    }
}
