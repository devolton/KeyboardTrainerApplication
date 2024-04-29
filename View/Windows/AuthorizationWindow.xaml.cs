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
        private bool _isLoginPageActive;
        public AuthorizationWindow()
        {
            InitializeComponent();
            _isLoginPageActive = true;
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

        private void OnLoginNavigatorButtonClick(object sender, RoutedEventArgs e)
        {            
            AuthorizationFrame.Content = _userLoginPage;
            _isLoginPageActive = !_isLoginPageActive;
            ChangeButtonsStyle();

        }

        private void OnSingUpNavigatorButtonClick(object sender, RoutedEventArgs e)
        {
            AuthorizationFrame.Content = _userSingInPage;
            _isLoginPageActive = !_isLoginPageActive;
            ChangeButtonsStyle();
        }
        private void ChangeButtonsStyle()
        {
            SolidColorBrush deepVioletBrush = (SolidColorBrush)Application.Current.Resources["CustomDeepBlueViolet"];
            SolidColorBrush coalBrush = (SolidColorBrush)Application.Current.Resources["CustomCoal"];
            SolidColorBrush whiteBrush = Brushes.White;
            ImageSource whiteLoginIcon = (ImageSource)Application.Current.Resources["LoginUserWhiteIcon"];
            ImageSource blueLoginIcon = (ImageSource)Application.Current.Resources["LoginUserBlueIcon"];
            ImageSource whiteSingUpIcon = (ImageSource)Application.Current.Resources["SingInUserWhiteIcon"];
            ImageSource blueSingUpIcon = (ImageSource)Application.Current.Resources["SingInUserBlueIcon"];
          
            if (_isLoginPageActive)
            {
                LoginNavigatorButton.Foreground = whiteBrush;
                LoginNavigatorButton.Background = deepVioletBrush;
                LoginNavigatorIconImage.Source= whiteLoginIcon;
                LoginNavigatorButtonTextBlock.TextDecorations = TextDecorations.Underline;
                SingUpNavigatorButton.Foreground = coalBrush;
                SingUpNavigatorButton.Background = null;
                SingUpNavigatorIconImage.Source= blueSingUpIcon;
                SingUpNavigatorButtonTextBlock.TextDecorations = null;
            }
            else
            {
                SingUpNavigatorButton.Foreground = whiteBrush;
                SingUpNavigatorButton.Background = deepVioletBrush;
                SingUpNavigatorButtonTextBlock.TextDecorations = TextDecorations.Underline;
                SingUpNavigatorIconImage.Source = whiteSingUpIcon;
                LoginNavigatorButton.Foreground = coalBrush;
                LoginNavigatorIconImage.Source = blueLoginIcon;
                LoginNavigatorButton.Background = null;
                LoginNavigatorButtonTextBlock.TextDecorations = null;


            }

        }
    }
}
