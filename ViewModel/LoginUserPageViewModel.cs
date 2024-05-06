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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        private readonly MultiCommand _multiCommand = new MultiCommand();
        private LoginUserPageModel _loginUserPageModel = new LoginUserPageModel();
        //поля для состояний 
        private bool _isChecked = false;
        private bool _isButtonEnable;
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
            _multiCommand.Add(new RelayCommand(ExecuteButtonCommand, CanExecuteButtonCommand));
            _multiCommand.Add(new RelayCommand(ExecuteWriteInRegisterCommand));
            _loginDefaultStyle = (Style)Application.Current.Resources["CustomDefaultLoginPageTextBox"];
            LoginTextBoxStyle = _loginDefaultStyle;
            _loginInvalidStyle = (Style)Application.Current.Resources["CustomInvalidLoginPageTextBox"];
            _passwordBoxDefaultStyle = (Style)Application.Current.Resources["CustomDefaultLoginPagePasswordBox"];
            PasswordBoxStyle = _passwordBoxDefaultStyle;
            _passwordBoxInvalidStyle = (Style)Application.Current.Resources["CustomInvalidLoginPagePasswordBox"];

        }

        //команда для выполнения нажатия кнопки
        public ICommand ButtonCommand => _multiCommand;

        //свойства 
        #region
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
            get { return _passwordBoxStyle;}
            set
            {
                _passwordBoxStyle = value;
                OnPropertyChanged(nameof(PasswordBoxStyle));
            }
        }
        #endregion


        private void ExecuteButtonCommand(object parameter)
        {
            new MainWindow().Show();
            foreach(Window oneWindow in Application.Current.Windows)
            {
                if(oneWindow is AuthorizationWindow)
                {
                    oneWindow.Close();
                    return;
                }
            }

        }
        private void ExecuteWriteInRegisterCommand(object parameter)
        {
            if (_isChecked)
                _loginUserPageModel.WriteDataInRegister(LoginOrEmail,Password );
            else
                _loginUserPageModel.WriteDataInRegister(string.Empty, string.Empty);

        }

        private bool CanExecuteButtonCommand(object parameter)
        {
            //может не работать потому что эта команда вызывается только при изиминении LOGIN
            return (_loginUserPageModel.IsValidLogin(LoginOrEmail) || _loginUserPageModel.IsValidEmail(LoginOrEmail)) && _loginUserPageModel.IsValidPassword(Password);
        }

        private void UpdateButtonEnagleState()
        {
            IsButtonEnable = CanExecuteButtonCommand(null); // обновления состояния кнопки
            
        }
        private void ChangeLoginTextBoxStyle()
        {
            if (LoginOrEmail==string.Empty||_loginUserPageModel.IsValidLogin(LoginOrEmail) || _loginUserPageModel.IsValidEmail(LoginOrEmail))
            {
                LoginTextBoxStyle = _loginDefaultStyle;
            }
            else
            {
                LoginTextBoxStyle =_loginInvalidStyle ;
            }
            

        }
        private void ChangePasswordBoxStyle()
        {
            if(Password.Equals(string.Empty) || _loginUserPageModel.IsValidPassword(Password))
            {
                PasswordBoxStyle = _passwordBoxDefaultStyle;

            }
            else
            {
                PasswordBoxStyle=_passwordBoxInvalidStyle;
            }
            
            

        }



    }
}
