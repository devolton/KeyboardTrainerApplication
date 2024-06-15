using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.View.Windows;
using CourseProjectKeyboardApplication.ViewModel.Commands;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class UserSingupPageViewModel : RegistrationFormViewModel
    {
        private readonly MultiCommand _singUpClickButtonMultiCommand;
        private readonly ICommand _passwordVisibilityCommand;
        private readonly ICommand _confirmPasswordVisibilityCommand;
        private readonly UserSingUpPageModel _model;
        public UserSingupPageViewModel()
        {
            _singUpClickButtonMultiCommand = new MultiCommand();
            _passwordVisibilityCommand = new RelayCommand(OnPasswordVisibilityCommand);
            _confirmPasswordVisibilityCommand = new RelayCommand(OnConfirmPasswordVisibilityCommand);
            _model = new UserSingUpPageModel();
            InitStyles();
            _singUpClickButtonMultiCommand.Add(new RelayCommand(OnTryRegisterUser, IsUserRegistrationCommandExecuted));

        }
       
    

        //commands
        #region

        //проверка на возможность выполнения попытки регестрации пользователя 
     
        //метод регестрации пользователя 
        private void OnTryRegisterUser(object param = null)
        {
            if (_model.IsUniqueCredentials(UserEmail, UserLogin))
            {
               var newUser= _model.RegisterUser(UserName, UserLogin, UserEmail, Password);
                if(newUser is null)
                {
                    MessageBox.Show("Invalid registerUserOperation");
                    //creating handler invalid registration operation
                    return;
                }
                MessageBox.Show(newUser.ToString());
                ClearAllFields();
                new MainWindow().Show();

                foreach (Window oneWindow in Application.Current.Windows)
                {
                    if (oneWindow is AuthorizationWindow)
                    {
                        oneWindow.Close();
                        return;
                    }
                }
            }
            else
            {
                if (!_model.IsUniqueLogin())
                {
                    ChangeLoginTextBoxStyleAsync();

                }
                if (!_model.IsUniqueEmail())
                {
                    ChangeEmailTextBoxStyleAsync();
                }
            }

        }

        private Task ChangeEmailTextBoxStyleAsync()
        {
            return Task.Run(async() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    EmailTextBoxStyle = _errorTextBoxStyle;

                });
                await Task.Delay(1500);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    EmailTextBoxStyle = _defaultTextBoxStyle;
                });

            });
        }

        private Task ChangeLoginTextBoxStyleAsync()
        {
            return Task.Run(async () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LoginTextBoxStyle = _errorTextBoxStyle;

                });
                await Task.Delay(1500);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LoginTextBoxStyle = _defaultTextBoxStyle;

                });
                
            });
        }

        protected override void OnPasswordVisibilityCommand(object param)
        {
            if (_isPasswordVisible)
            {
                _isPasswordVisible = false;
                PasswordTextBoxVisibility = Visibility.Collapsed;
                PasswordBoxVisibility = Visibility.Visible;
                PasswordTextDecorationCollection = TextDecorations.Strikethrough;
            }
            else
            {
                _isPasswordVisible = true;
                PasswordBoxVisibility = Visibility.Collapsed;
                PasswordTextBoxVisibility = Visibility.Visible;
                PasswordTextDecorationCollection = null;
                
            }

        }
        protected override void OnConfirmPasswordVisibilityCommand(object param)
        {
            if (_isConfirmPasswordVisible)
            {
                _isConfirmPasswordVisible = false;
                ConfirmPasswordTextBoxVisibility = Visibility.Collapsed;
                ConfirmPasswordBoxVisibility = Visibility.Visible;
                ConfirmPasswordTextDecorationCollection = TextDecorations.Strikethrough;
            }
            else
            {
                _isConfirmPasswordVisible = true;
                ConfirmPasswordBoxVisibility = Visibility.Collapsed;
                ConfirmPasswordTextBoxVisibility = Visibility.Visible;
                ConfirmPasswordTextDecorationCollection = null;

            }

        }
        #endregion
        //command predicates
        #region
        private bool IsUserRegistrationCommandExecuted(object param = null)
        {
            return _model.IsValidName(UserName) && _model.IsValidEmail(UserEmail)
                && _model.IsValidLogin(UserLogin) && _model.IsValidPassword(Password) &&
                Password.Equals(ConfirmPassword);
        }
        private void CanEnabledRegistrationButton() => IsEnabledRegistrationButton = IsUserRegistrationCommandExecuted();
        #endregion

        //sing up proporties
        #region
        public ICommand SingUpClickCommand => _singUpClickButtonMultiCommand;
        public ICommand PasswordVisibilityCommand => _passwordVisibilityCommand;
        public ICommand ConfirmPasswordVisivilityCommand => _confirmPasswordVisibilityCommand;
        public string UserName
        {
            get { return _name; }
            set
            {
                _name = value;
                NameTextBoxStyle=ChangeTextBoxStyle(_model.IsValidName, UserName);
                CanEnabledRegistrationButton();
                OnPropertyChanged(nameof(UserName));

            }
        }
        public string UserLogin
        {
            get { return _login; }
            set
            {
                _login = value;
                LoginTextBoxStyle=ChangeTextBoxStyle(_model.IsValidLogin, UserLogin);
                CanEnabledRegistrationButton();
                OnPropertyChanged(nameof(UserLogin));
            }
        }
        public string UserEmail
        {
            get { return _email; }
            set
            {
                _email = value;
                EmailTextBoxStyle=ChangeTextBoxStyle(_model.IsValidEmail, UserEmail);
                CanEnabledRegistrationButton();
                OnPropertyChanged(nameof(UserEmail));
            }
        }
        public string Password
        {
            get { return _password;}
            set
            {
                _password = value;
                PasswordBoxStyle = ChangePasswordBoxStyle(_model.IsValidPassword, Password);
                PasswordTextBoxStyle = ChangeTextBoxStyle(_model.IsValidPassword, Password);
                CanEnabledRegistrationButton();
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                ConfirmPasswordBoxStyle = ChangePasswordBoxStyle(_model.IsValidPassword, ConfirmPassword);
                PasswordTextBoxStyle = ChangeTextBoxStyle(_model.IsValidPassword, ConfirmPassword);
                CanEnabledRegistrationButton();
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public bool IsEnabledRegistrationButton
        {
            get { return _isEnabledRegistrationButton; }
            set
            {
                _isEnabledRegistrationButton = value;
                OnPropertyChanged(nameof(IsEnabledRegistrationButton));
            }

        }

        public Visibility PasswordBoxVisibility
        {
            get =>_passwordBoxVisibility;
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
        public Visibility ConfirmPasswordBoxVisibility
        {
            get => _confirmPasswordBoxVisibility;
            set
            {
                _confirmPasswordBoxVisibility = value;
                OnPropertyChanged(nameof(ConfirmPasswordBoxVisibility));
            }
        }
        public Visibility ConfirmPasswordTextBoxVisibility
        {
            get => _confirmPasswordTextBoxVisibility;
            set
            {
                _confirmPasswordTextBoxVisibility = value;
                OnPropertyChanged(nameof(ConfirmPasswordTextBoxVisibility));
            }
        }

        #endregion


    }
}
