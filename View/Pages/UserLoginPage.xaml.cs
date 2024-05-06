using Microsoft.Xaml.Behaviors;
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
    /// Логика взаимодействия для UserLoginPage.xaml
    /// </summary>
    public partial class UserLoginPage : Page
    {
        LoginUserPageViewModel _loginViewModel;
        public UserLoginPage()
        {

            InitializeComponent();
            _loginViewModel = new LoginUserPageViewModel(LoginPasswordBox);
            DataContext = _loginViewModel;

        }

        private void UserLoginPagePasswordVisibiletyButton_Click(object sender, RoutedEventArgs e)
        {
            UserLoginPagePasswordVisibilityIconTextBlock.TextDecorations = (UserLoginPagePasswordVisibilityIconTextBlock.TextDecorations is null) ?
            TextDecorations.Strikethrough : null;

        }

        private void LoginPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            _loginViewModel.Password = passwordBox.Password;


        }
    }
}
