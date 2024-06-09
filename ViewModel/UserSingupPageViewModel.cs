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
        private readonly UserSingUpPageModel _userSingUpPageModel;
        public UserSingupPageViewModel()
        {
            _singUpClickButtonMultiCommand = new MultiCommand();
            _passwordVisibilityCommand = new RelayCommand(OnPasswordVisibilityCommand);
            _confirmPasswordVisibilityCommand = new RelayCommand(OnConfirmPasswordVisibilityCommand);
            _userSingUpPageModel = new UserSingUpPageModel();
            InitStyles();
            _singUpClickButtonMultiCommand.Add(new RelayCommand(OnTryRegisterUser, IsUserRegistrationCommandExecuted));

        }
       
    

        //commands
        #region

        //проверка на возможность выполнения попытки регестрации пользователя 
     
        //метод регестрации пользователя 
        private void OnTryRegisterUser(object param = null)
        {
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
            return _userSingUpPageModel.IsValidName(UserName) && _userSingUpPageModel.IsValidEmail(UserEmail)
                && _userSingUpPageModel.IsValidLogin(UserLogin) && _userSingUpPageModel.IsValidPassword(Password) &&
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
                NameTextBoxStyle=ChangeTextBoxStyle(_userSingUpPageModel.IsValidName, UserName);
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
                LoginTextBoxStyle=ChangeTextBoxStyle(_userSingUpPageModel.IsValidLogin, UserLogin);
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
                EmailTextBoxStyle=ChangeTextBoxStyle(_userSingUpPageModel.IsValidEmail, UserEmail);
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
                PasswordBoxStyle = ChangePasswordBoxStyle(_userSingUpPageModel.IsValidPassword, Password);
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
                ConfirmPasswordBoxStyle = ChangePasswordBoxStyle(_userSingUpPageModel.IsValidPassword, ConfirmPassword);
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
