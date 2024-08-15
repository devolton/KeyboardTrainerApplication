using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Shared.Providers;
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
        private bool _isLoginPageActive = true;
        private SolidColorBrush _deepVioletBrush;
        private SolidColorBrush _coalBrush;
        private SolidColorBrush _whiteBrush;
        private ImageSource _whiteLoginIcon;
        private ImageSource _blueLoginIcon;
        private ImageSource _whiteSingUpIcon;
        private ImageSource _blueSingUpIcon;
        private ImageSource _exitIconImageSource;
        private ImageSource _windowIconImageSource;


        private StaticImageApiClient _staticImageApiClient;

        public AuthorizationWindow()
        {
            InitializeComponent();
            _deepVioletBrush = (SolidColorBrush)Application.Current.Resources["CustomDeepBlueViolet"];
            _coalBrush = (SolidColorBrush)Application.Current.Resources["CustomCoal"];
            _whiteBrush = (SolidColorBrush)Application.Current.Resources["CustomWhite"];
            _staticImageApiClient = ContentApiClientProvider.StaticImageApiClient;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _windowIconImageSource = await _staticImageApiClient.GetImageSourceAsync("AuthorizationLogo.png");
            LoginWindow.Icon = _windowIconImageSource;
            _userLoginPage = new UserLoginPage();
            _userSingInPage = new UserSingInPage();
            AuthorizationFrame.Content = _userLoginPage;
            _whiteLoginIcon = await _staticImageApiClient.GetImageSourceAsync("LoginUserWhiteIcon.png");
            _blueLoginIcon = await _staticImageApiClient.GetImageSourceAsync("LoginUserBlueIcon.png");
            _whiteSingUpIcon = await _staticImageApiClient.GetImageSourceAsync("SingUpUserWhiteIcon.png");
            _blueSingUpIcon = await _staticImageApiClient.GetImageSourceAsync("SingUpUserBlueIcon.png");
            _exitIconImageSource = await _staticImageApiClient.GetImageSourceAsync("ExitIcon.png");
            

            LoginNavigatorIconImage.Source = _whiteLoginIcon;
            SingUpNavigatorIconImage.Source = _blueSingUpIcon;
            ExitImageBrush.ImageSource = _exitIconImageSource;
           
           
        }

        private void OnLoginNavigatorButtonClick(object sender, RoutedEventArgs e)
        {
            if (!_isLoginPageActive)
            {
               
                AuthorizationFrame.Content = _userLoginPage;
                _isLoginPageActive = !_isLoginPageActive;
                ChangeButtonsStyle();
                _isLoginPageActive = true;
            }


        }

        private void OnSingUpNavigatorButtonClick(object sender, RoutedEventArgs e)
        {
            if (_isLoginPageActive)
            {
                
                AuthorizationFrame.Content = _userSingInPage;
                _isLoginPageActive = !_isLoginPageActive;
                ChangeButtonsStyle();
                _isLoginPageActive = false;
            }


        }

        private void ChangeButtonsStyle()
        {

            Task.Run(() =>
            {
                if (_isLoginPageActive)
                {
                    Dispatcher.Invoke(() =>
                    {
                        LoginNavigatorButton.Foreground = _whiteBrush;
                        LoginNavigatorButton.Background = _deepVioletBrush;
                        LoginNavigatorIconImage.Source = _whiteLoginIcon;
                        LoginNavigatorButtonTextBlock.TextDecorations = TextDecorations.Underline;
                        SingUpNavigatorButton.Foreground = _coalBrush;
                        SingUpNavigatorButton.Background = null;
                        SingUpNavigatorIconImage.Source = _blueSingUpIcon;
                        SingUpNavigatorButtonTextBlock.TextDecorations = null;

                    });
                  
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        SingUpNavigatorButton.Foreground = _whiteBrush;
                        SingUpNavigatorButton.Background = _deepVioletBrush;
                        SingUpNavigatorButtonTextBlock.TextDecorations = TextDecorations.Underline;
                        SingUpNavigatorIconImage.Source = _whiteSingUpIcon;
                        LoginNavigatorButton.Foreground = _coalBrush;
                        LoginNavigatorIconImage.Source = _blueLoginIcon;
                        LoginNavigatorButton.Background = null;
                        LoginNavigatorButtonTextBlock.TextDecorations = null;

                    });
                }

            });
          

        }




    }
}
