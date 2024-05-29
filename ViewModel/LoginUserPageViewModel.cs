using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.View.Pages;
using CourseProjectKeyboardApplication.View.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class LoginUserPageViewModel : ViewModelBase
    {
        private Style _loginTextBoxStyle;
        private Style _passwordBoxStyle;
        private Style _loginDefaultStyle;
        private Style _passwordBoxDefaultStyle;
        private Style _loginInvalidStyle;
        private Style _passwordBoxInvalidStyle;
        private Visibility _passwordBoxVisibility = Visibility.Collapsed;
        private Visibility _passwordTextBoxVisibility = Visibility.Visible;
        private TextDecorationCollection _passwordEyeButtonDecoration = TextDecorations.Strikethrough;
        private readonly MultiCommand _multiCommand = new MultiCommand();
        private readonly ICommand _passwordVisibilityCommand;
        private LoginUserPageModel _loginUserPageModel = new LoginUserPageModel();
        //поля для состояний 
        private bool _isChecked = false;
        private bool _isButtonEnable;
        private bool _isPasswordVisible = false;
        private string _loginOrEmail;
        private string _password = string.Empty;
        private PasswordBox _loginPasswordBox;
        public LoginUserPageViewModel(PasswordBox passwordBox) : this()
        {
            _loginPasswordBox = passwordBox;
            _loginPasswordBox.Password = _loginUserPageModel.GetPasswordFromRegister();
            LoginOrEmail = _loginUserPageModel.GetLoginFromRegister();



        }
        public LoginUserPageViewModel()
        {
            //делегируем методы (метод который срабатывает при нажатии кнопки)(который проверяет условия выполнения метода)
            _multiCommand.Add(new RelayCommand(OnLoginUserCommand, CanExecuteButtonCommand));
            _multiCommand.Add(new RelayCommand(OnWriteInRegisterCommand));
            _passwordVisibilityCommand = new RelayCommand(OnPasswordVisibilityCommand);
            _loginDefaultStyle = (Style)Application.Current.Resources["CustomDefaultLoginPageTextBox"];
            LoginTextBoxStyle = _loginDefaultStyle;
            _loginInvalidStyle = (Style)Application.Current.Resources["CustomInvalidLoginPageTextBox"];
            _passwordBoxDefaultStyle = (Style)Application.Current.Resources["CustomDefaultLoginPagePasswordBox"];
            PasswordBoxStyle = _passwordBoxDefaultStyle;
            _passwordBoxInvalidStyle = (Style)Application.Current.Resources["CustomInvalidLoginPagePasswordBox"];

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
        private void OnWriteInRegisterCommand(object parameter)
        {
            if (_isChecked)
                _loginUserPageModel.WriteDataInRegister(LoginOrEmail, Password);
            else
                _loginUserPageModel.WriteDataInRegister(string.Empty, string.Empty);

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
            return (_loginUserPageModel.IsValidLogin(LoginOrEmail) || _loginUserPageModel.IsValidEmail(LoginOrEmail)) && _loginUserPageModel.IsValidPassword(Password);
        }
        private void UpdateButtonEnagleState()
        {
            IsButtonEnable = CanExecuteButtonCommand(null); // обновления состояния кнопки

        }
        #endregion



        private void ChangeLoginTextBoxStyle()
        {
            if (LoginOrEmail == string.Empty || _loginUserPageModel.IsValidLogin(LoginOrEmail) || _loginUserPageModel.IsValidEmail(LoginOrEmail))
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
            if (Password.Equals(string.Empty) || _loginUserPageModel.IsValidPassword(Password))
            {
                PasswordBoxStyle = _passwordBoxDefaultStyle;

            }
            else
            {
                PasswordBoxStyle = _passwordBoxInvalidStyle;
            }



        }



    }
}
