using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.View.Pages;
using CourseProjectKeyboardApplication.View.Windows;
using CourseProjectKeyboardApplication.ViewModel.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using MaterialDesignThemes.Wpf;
using CourseProjectKeyboardApplication.Shared.Mediators;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class LoginUserPageViewModel : ViewModelBase
    {
        //style fields 
        private Style _loginTextBoxStyle;
        private Style _passwordBoxStyle;
        private Style _loginDefaultStyle;
        private Style _passwordBoxDefaultStyle;
        private Style _loginInvalidStyle;
        private Style _passwordBoxInvalidStyle;
        private Visibility _passwordBoxVisibility = Visibility.Visible;
        private Visibility _passwordTextBoxVisibility = Visibility.Collapsed;
        private TextDecorationCollection _passwordEyeButtonDecoration = TextDecorations.Strikethrough;
        //commands fields 
        private readonly MultiCommand _multiCommand = new MultiCommand();
        private readonly ICommand _passwordVisibilityCommand;
        private ILoginUserPageModel _model;
        //state fields
        private bool _isChecked = false;
        private bool _isButtonEnable;
        private bool _isPasswordVisible = false;
        // user credentiels fields
        private string _loginOrEmail;
        private string _password = string.Empty;
        private MainWindow _mainWindow;


        public LoginUserPageViewModel()
        {
            _model = new LoginUserPageModel();
            _multiCommand.Add(new RelayCommand(OnLoginUserCommand, CanExecuteButtonCommand));
            _passwordVisibilityCommand = new RelayCommand(OnPasswordVisibilityCommand);
            _loginDefaultStyle = (Style)Application.Current.Resources["CustomDefaultLoginPageTextBox"];
            LoginTextBoxStyle = _loginDefaultStyle;
            _loginInvalidStyle = (Style)Application.Current.Resources["CustomInvalidLoginPageTextBox"];
            _passwordBoxDefaultStyle = (Style)Application.Current.Resources["CustomDefaultLoginPagePasswordBox"];
            PasswordBoxStyle = _passwordBoxDefaultStyle;
            _passwordBoxInvalidStyle = (Style)Application.Current.Resources["CustomInvalidLoginPagePasswordBox"];
            LoginOrEmail = _model.GetLoginFromRegister();
            Password = _model.GetPasswordFromRegister();
            if (LoginOrEmail != string.Empty && Password != string.Empty)
            {
                IsChecked = true;
            }
            InitMainPage();
        }

        //свойства 
        #region

        //команда для выполнения нажатия кнопки
        public ICommand ButtonCommand => _multiCommand;

        public ICommand PasswordVisibilityCommand => _passwordVisibilityCommand;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;

                OnPropertyChanged(nameof(IsChecked)); //уведомляем о изминении свойства

            }
        }
        public bool IsButtonEnable
        {
            get { return _isButtonEnable; }
            set
            {
                _isButtonEnable = value;
                OnPropertyChanged(nameof(IsButtonEnable)); //уведомляем о изминении свойства
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                ChangePasswordBoxStyle();
                UpdateButtonEnagleState();
                OnPropertyChanged(nameof(Password));
            }
        }
        public string LoginOrEmail
        {
            get { return _loginOrEmail; }
            set
            {
                _loginOrEmail = value;
                UpdateButtonEnagleState();
                ChangeLoginTextBoxStyle();
                OnPropertyChanged(nameof(LoginOrEmail));

            }
        }
        public Style LoginTextBoxStyle
        {
            get { return _loginTextBoxStyle; }
            set
            {
                _loginTextBoxStyle = value;
                OnPropertyChanged(nameof(LoginTextBoxStyle));
            }
        }
        public Style PasswordBoxStyle
        {
            get { return _passwordBoxStyle; }
            set
            {
                _passwordBoxStyle = value;
                OnPropertyChanged(nameof(PasswordBoxStyle));
            }
        }
        public Visibility PasswordBoxVisibility
        {
            get => _passwordBoxVisibility;
            set
            {
                _passwordBoxVisibility = value;
                OnPropertyChanged(nameof(PasswordBoxVisibility));
            }
        }
        public Visibility PasswordTextBoxVisibility
        {
            get => _passwordTextBoxVisibility;
            set
            {
                _passwordTextBoxVisibility = value;
                OnPropertyChanged(nameof(PasswordTextBoxVisibility));
            }
        }
        public TextDecorationCollection EyeButtonDecorationCollection
        {
            get => _passwordEyeButtonDecoration;
            set
            {
                _passwordEyeButtonDecoration = value;
                OnPropertyChanged(nameof(EyeButtonDecorationCollection));
            }

        }
        #endregion

        //commnands
        #region
        private async void OnLoginUserCommand(object parameter)
        {
            var isUserExistAndStatusPair = await _model.IsUserExist(LoginOrEmail);
            if (!isUserExistAndStatusPair.Key)
            {
                if (isUserExistAndStatusPair.Value == NotifyType.ServerRequestTimeout)
                {
                    NotificationMediator.ShowNotificationWindow(NotifyType.ServerRequestTimeout);
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ChangeLoginTextBoxAsync();
                    });
                    NotificationMediator.ShowNotificationWindow(NotifyType.UserNotFoundByLoginOrEmail);
                }

                return;

            }
            User? user = await _model.GetUserByLoginOrEmailAndPassword(LoginOrEmail, Password);

            if (user is null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ChangePasswordBoxStyleAsync();
                });
                NotificationMediator.ShowNotificationWindow(NotifyType.IncorrectPassword);

                return;
            }
            _model.WriteInRegister(LoginOrEmail, Password, _isChecked);
            await _model.InitUserInUserController(user);
            OpenMainWindow();
        }


        private void OnPasswordVisibilityCommand(object obj)
        {
            if (_isPasswordVisible)
            {
                _isPasswordVisible = false;

                PasswordTextBoxVisibility = Visibility.Collapsed;
                EyeButtonDecorationCollection = TextDecorations.Strikethrough;
                PasswordBoxVisibility = Visibility.Visible;
            }
            else
            {
                _isPasswordVisible = true;
                PasswordBoxVisibility = Visibility.Collapsed;
                PasswordTextBoxVisibility = Visibility.Visible;
                EyeButtonDecorationCollection = null;

            }
        }
        #endregion

        //command predicate
        #region
        private bool CanExecuteButtonCommand(object parameter)
        {

            return (_model.IsValidLogin(LoginOrEmail) || _model.IsValidEmail(LoginOrEmail)) && _model.IsValidPassword(Password);
        }
        private void UpdateButtonEnagleState()
        {
            IsButtonEnable = CanExecuteButtonCommand(null); // обновления состояния кнопки

        }
        #endregion



        private void ChangeLoginTextBoxStyle()
        {
            if (LoginOrEmail == string.Empty || _model.IsValidLogin(LoginOrEmail) || _model.IsValidEmail(LoginOrEmail))
            {
                LoginTextBoxStyle = _loginDefaultStyle;
            }
            else
            {
                LoginTextBoxStyle = _loginInvalidStyle;
            }


        }
        private void ChangePasswordBoxStyle()
        {
            if (Password.Equals(string.Empty) || _model.IsValidPassword(Password))
            {
                PasswordBoxStyle = _passwordBoxDefaultStyle;

            }
            else
            {
                PasswordBoxStyle = _passwordBoxInvalidStyle;
            }

        }
        private Task ChangeLoginTextBoxAsync()
        {
            return Task.Run(async () =>
             {

                 Application.Current.Dispatcher.Invoke(() =>
                 {
                     LoginTextBoxStyle = _loginInvalidStyle;



                 });
                 await Task.Delay(1500);
                 Application.Current.Dispatcher.Invoke(() =>
                 {
                     LoginTextBoxStyle = _loginDefaultStyle;
                 });

             });
        }
        private Task ChangePasswordBoxStyleAsync()
        {
            return Task.Run(async () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    PasswordBoxStyle = _passwordBoxInvalidStyle;

                });
                await Task.Delay(1500);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    PasswordBoxStyle = _passwordBoxDefaultStyle;
                });

            });
        }
        private async void OpenMainWindow()
        {
            await AppImageSourceProvider.Init();
            _mainWindow.Show();
            foreach (Window oneWindow in Application.Current.Windows)
            {
                if (oneWindow is AuthorizationWindow)
                {
                    var autorizationWindow = oneWindow as AuthorizationWindow;
                    autorizationWindow.IsDisposableHttpClients = false;
                    oneWindow.Close();
                    return;
                }
            }
        }
        private Task InitMainPage()
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _mainWindow = new MainWindow();
                });
            });
        }




    }
}
