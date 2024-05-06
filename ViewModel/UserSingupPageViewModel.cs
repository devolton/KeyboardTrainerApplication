using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class UserSingupPageViewModel : ViewModelBase
    {
        private bool _isEnabledRegistraionButton;
        private Style _singUpDefaultTextBoxStyle;
        private Style _singUpInvalidTextBoxStyle;
        private Style _singUpDefaultPasswordBoxStyle;
        private Style _singUpInvalidPasswordBoxStyle;
        private Style _singUpNameTextBoxStyle;
        private Style _singUpLoginTextBoxStyle;
        private Style _singUpEmailTextBoxStyle;
        private Style _singUpPasswordBoxStyle;
        private Style _singUpConfirmPasswordBoxStyle;
        private string _userName;
        private string _userLogin;
        private string _userEmail;
        private string _password;
        private string _confirmPassword;
        private PasswordBox _passwordBox;
        private PasswordBox _confirmPasswordBox;
        private readonly MultiCommand _singUpClickButtonMultiCommand;
        private readonly UserSingUpPageModel _userSingUpPageModel;
        public UserSingupPageViewModel(PasswordBox passwordBox, PasswordBox confirmPasswordBox) : base()
        {
            _passwordBox = passwordBox;
            _confirmPasswordBox = confirmPasswordBox;


        }
        public UserSingupPageViewModel()
        {
            _singUpClickButtonMultiCommand = new MultiCommand();
            _userSingUpPageModel = new UserSingUpPageModel();
            InitStyles();
            IsEnabledRegistrationButton = false;
            UserName = string.Empty;
            UserLogin = string.Empty;
            UserEmail = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;

            _singUpClickButtonMultiCommand.Add(new RelayCommand(TryRegisterUser, IsUserRegistrationCommandExecuted));

        }
       
        public ICommand SingUpClickCommand => _singUpClickButtonMultiCommand;

        //проверка на возможность выполнения попытки регестрации пользователя 
        private bool IsUserRegistrationCommandExecuted(object param = null)
        {
            return _userSingUpPageModel.IsValidName(UserName) && _userSingUpPageModel.IsValidEmail(UserEmail)
                && _userSingUpPageModel.IsValidLogin(UserLogin) && _userSingUpPageModel.IsValidPassword(Password) &&
                Password.Equals(ConfirmPassword);
        }
        //метод регестрации пользователя 
        private void TryRegisterUser(object param = null)
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
        private bool CanEnabledRegistrationButton() => IsEnabledRegistrationButton = IsUserRegistrationCommandExecuted();

        //sing up proporties
        #region

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
