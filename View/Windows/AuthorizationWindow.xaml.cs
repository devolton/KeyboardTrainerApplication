using CourseProjectKeyboardApplication.View.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace CourseProjectKeyboardApplication.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private UserLoginPage _userLoginPage;
        private UserSingInPage _userSingInPage;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _userLoginPage = new UserLoginPage();
            _userSingInPage = new UserSingInPage();
            AuthorizationFrame.Content = _userLoginPage;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AuthorizationFrame.Content = _userLoginPage;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AuthorizationFrame.Content = _userSingInPage;
        }
    }
}
