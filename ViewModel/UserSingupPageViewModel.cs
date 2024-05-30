using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.View.Windows;
using CourseProjectKeyboardApplication.ViewModel.Commands;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class UserSingupPageViewModel : ViewModelBase
    {
        private bool _isEnabledRegistraionButton = false;
        private bool _isPasswordVisible = false;
        private bool _isConfirmPasswordVisible = false;

        private Style _singUpDefaultTextBoxStyle;
        private Style _singUpInvalidTextBoxStyle;
        private Style _singUpDefaultPasswordBoxStyle;
        private Style _singUpInvalidPasswordBoxStyle;
        private Style _singUpNameTextBoxStyle;
        private Style _singUpLoginTextBoxStyle;
        private Style _singUpEmailTextBoxStyle;
        private Style _singUpPasswordBoxStyle;
        private Style _singUpConfirmPasswordBoxStyle;
        private Visibility _passwordBoxVisibility = Visibility.Visible;
        private Visibility _passwordTextBoxVisibility = Visibility.Collapsed;
        private Visibility _confirmPasswordBoxVisibility = Visibility.Visible;
        private Visibility _confirmPasswordTextBoxVisibility = Visibility.Collapsed;
        private TextDecorationCollection _passwordTextDecorationCollection = TextDecorations.Strikethrough;
        private TextDecorationCollection _confirmPasswordTextDecorationCollection = TextDecorations.Strikethrough;
        private string _userName = string.Empty;
        private string _userLogin = string.Empty;
        private string _userEmail = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
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
            ClearAllField();
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
        private void OnPasswordVisibilityCommand(object param)
        {
            if (_isPasswordVisible)
            {
                _isPasswordVisible = false;
                PasswordTextBoxVisibility = Visibility.Collapsed;
                PasswordBoxVisibility = Visibility.Visible;
                PasswordTextDecorations = TextDecorations.Strikethrough;
            }
            else
            {
                _isPasswordVisible = true;
                PasswordBoxVisibility = Visibility.Collapsed;
                PasswordTextBoxVisibility = Visibility.Visible;
                PasswordTextDecorations = null;
                
            }

        }
        private void OnConfirmPasswordVisibilityCommand(object param)
        {
            if (_isConfirmPasswordVisible)
            {
                _isConfirmPasswordVisible = false;
                ConfirmPasswordTextBoxVisibility = Visibility.Collapsed;
                ConfirmPasswordBoxVisibility = Visibility.Visible;
                ConfirmPasswordTextDecorations = TextDecorations.Strikethrough;
            }
            else
            {
                _isConfirmPasswordVisible = true;
                ConfirmPasswordBoxVisibility = Visibility.Collapsed;
                ConfirmPasswordTextBoxVisibility = Visibility.Visible;
                ConfirmPasswordTextDecorations = null;

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
        private bool CanEnabledRegistrationButton() => IsEnabledRegistrationButton = IsUserRegistrationCommandExecuted();
        #endregion

        //sing up proporties
        #region
        public ICommand SingUpClickCommand => _singUpClickButtonMultiCommand;
        public ICommand PasswordVisibilityCommand => _passwordVisibilityCommand;
        public ICommand ConfirmPasswordVisivilityCommand => _confirmPasswordVisibilityCommand;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                UserNameTextBoxStyle=ChangeTextBoxStyle(_userSingUpPageModel.IsValidName, UserName);
                CanEnabledRegistrationButton();
                OnPropertyChanged(nameof(UserName));

            }
        }
        public string UserLogin
        {
            get { return _userLogin; }
            set
            {
                _userLogin = value;
                UserLoginTextBoxStyle=ChangeTextBoxStyle(_userSingUpPageModel.IsValidLogin, UserLogin);
                CanEnabledRegistrationButton();
                OnPropertyChanged(nameof(UserLogin));
            }
        }
        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                UserEmailTextBoxStyle=ChangeTextBoxStyle(_userSingUpPageModel.IsValidEmail, UserEmail);
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
                UserPasswordBoxStyle = ChangePasswordBoxStyle(_userSingUpPageModel.IsValidPassword, Password);
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
                UserConfirmPasswordBoxStyle = ChangePasswordBoxStyle(_userSingUpPageModel.IsValidPassword, ConfirmPassword);
                CanEnabledRegistrationButton();
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        public Style UserNameTextBoxStyle
        {
            get { return _singUpNameTextBoxStyle; }
            set
            {
                _singUpNameTextBoxStyle = value;
                OnPropertyChanged(nameof(UserNameTextBoxStyle));
            }
        }
        public Style UserLoginTextBoxStyle
        {
            get { return _singUpLoginTextBoxStyle; }
            set
            {
                _singUpLoginTextBoxStyle = value;
                OnPropertyChanged(nameof(UserLoginTextBoxStyle));
            }
        }
        public Style UserEmailTextBoxStyle
        {
            get { return _singUpEmailTextBoxStyle; }
            set
            {
                _singUpEmailTextBoxStyle = value;
                OnPropertyChanged(nameof(UserEmailTextBoxStyle));
            }
        }
        public Style UserPasswordBoxStyle
        {
            get { return _singUpPasswordBoxStyle; }
            set
            {
                _singUpPasswordBoxStyle = value;
                OnPropertyChanged(nameof(UserPasswordBoxStyle));
            }
        }
        public Style UserConfirmPasswordBoxStyle
        {
            get { return _singUpConfirmPasswordBoxStyle; }
            set
            {
                _singUpConfirmPasswordBoxStyle = value;
                OnPropertyChanged(nameof(UserConfirmPasswordBoxStyle));
            }
        }
        public bool IsEnabledRegistrationButton
        {
            get { return _isEnabledRegistraionButton; }
            set
            {
                _isEnabledRegistraionButton = value;
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
        public TextDecorationCollection PasswordTextDecorations
        {
            get => _passwordTextDecorationCollection;
            set
            {
                _passwordTextDecorationCollection = value;
                OnPropertyChanged(nameof(PasswordTextDecorations));
            }

        }
        public TextDecorationCollection ConfirmPasswordTextDecorations
        {
            get => _confirmPasswordTextDecorationCollection;
            set
            {
                _confirmPasswordTextDecorationCollection = value;
                OnPropertyChanged(nameof(ConfirmPasswordTextDecorations));
            }

        }

        #endregion

        //Change style methods

        //изминение стиля в TextBox в зависимости от данных
        private Style ChangeTextBoxStyle(Func<string, bool> validationFunc, string data)
        {
            if (data.Equals(string.Empty) || validationFunc.Invoke(data))
            {
                return _singUpDefaultTextBoxStyle;

            }
            return _singUpInvalidTextBoxStyle;

        }
        //изминение стиля в PasswordBox в зависимости от данных
        private Style ChangePasswordBoxStyle(Func<string, bool> validationFunc, string data)
        {
            string comparedValue = (nameof(data) == "Password") ? ConfirmPassword : Password;
            if (data.Equals(string.Empty) || (validationFunc.Invoke(data) && data.Equals(comparedValue)))
            {
                return _singUpDefaultPasswordBoxStyle;
            }
            return _singUpInvalidPasswordBoxStyle;

        }


        private void InitStyles()
        {
            _singUpDefaultTextBoxStyle = (Style)Application.Current.Resources["CustomDefaultSingUpPageTextBox"];
            _singUpInvalidTextBoxStyle = (Style)Application.Current.Resources["CustomInvalidSingUpPageTextBox"];
            _singUpDefaultPasswordBoxStyle = (Style)Application.Current.Resources["CustomDefaultSingUpPagePasswordBox"];
            _singUpInvalidPasswordBoxStyle = (Style)Application.Current.Resources["CustomInvalidSingUpPagePasswordBox"];
            UserNameTextBoxStyle = _singUpDefaultTextBoxStyle;
            UserLoginTextBoxStyle = _singUpDefaultTextBoxStyle;
            UserEmailTextBoxStyle = _singUpDefaultTextBoxStyle;
            UserPasswordBoxStyle = _singUpDefaultPasswordBoxStyle;
            UserConfirmPasswordBoxStyle = _singUpDefaultPasswordBoxStyle;

        }
        private void ClearAllField()
        {
            UserName = string.Empty;
            UserLogin=string.Empty;
            UserEmail=string.Empty;
            Password = string.Empty;
            ConfirmPassword=string.Empty;
        }
    }
}
