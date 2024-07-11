using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.View.Pages;
using CourseProjectKeyboardApplication.View.Windows;
using CourseProjectKeyboardApplication.ViewModel.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

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
        private LoginUserPageModel _model = new LoginUserPageModel();
        //state fields
        private bool _isChecked = false;
        private bool _isValidUser = false;
        private bool _isButtonEnable;
        private bool _isPasswordVisible = false;
        // user credentiels fields
        private string _loginOrEmail;
        private string _password = string.Empty;
        private MainWindow _mainWindow;

        public LoginUserPageViewModel()
        {

            _multiCommand.Add(new RelayCommand(OnLoginUserCommand, CanExecuteButtonCommand));
            _multiCommand.Add(new RelayCommand(OnWriteInRegisterCommand));
            _passwordVisibilityCommand = new RelayCommand(OnPasswordVisibilityCommand);
            _loginDefaultStyle = (Style)Application.Current.Resources["CustomDefaultLoginPageTextBox"];
            LoginTextBoxStyle = _loginDefaultStyle;
            _loginInvalidStyle = (Style)Application.Current.Resources["CustomInvalidLoginPageTextBox"];
            _passwordBoxDefaultStyle = (Style)Application.Current.Resources["CustomDefaultLoginPagePasswordBox"];
            PasswordBoxStyle = _passwordBoxDefaultStyle;
            _passwordBoxInvalidStyle = (Style)Application.Current.Resources["CustomInvalidLoginPagePasswordBox"];
            LoginOrEmail = _model.GetLoginFromRegister();
            Password = _model.GetPasswordFromRegister();
            if (LoginOrEmail!=string.Empty && Password != string.Empty)
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
        private void OnLoginUserCommand(object parameter)
        {
            if (!_model.IsUserExist(LoginOrEmail))
            {
                ChangeLoginTextBoxAsync();
                return;

            }
            User? user = _model.GetUserByLoginOrEmailAndPassword(LoginOrEmail, Password);
            if (user is null)
            {
                ChangePasswordBoxStyleAsync();
                return;
            }
            _isValidUser = true;
            _model.InitUserInUserController(user);
            OpenMainWindow();
        }


        private void OnWriteInRegisterCommand(object parameter)
        {
            if (_isChecked && _isValidUser)
                _model.WriteDataInRegister(LoginOrEmail, Password);
            else
                _model.WriteNakedDataInRegister();

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
            //может не работать потому что эта команда вызывается только при изиминении LOGIN
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
        private void OpenMainWindow()
        {
            _mainWindow.Show();
            foreach (Window oneWindow in Application.Current.Windows)
            {
                if (oneWindow is AuthorizationWindow)
                {
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
