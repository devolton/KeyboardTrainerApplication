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
        private Style _loginDefaultStyle;
        private Style _loginInvalidStyle;
        private readonly MultiCommand _multiCommand = new MultiCommand();
        private LoginUserPageModel _loginUserPageModel = new LoginUserPageModel();
        //поля для состояний 
        private bool _isChecked = false;
        private bool _isButtonEnable;
        private string _login;
        private string _password;
        private PasswordBox _loginPasswordBox;
        public LoginUserPageViewModel(PasswordBox passwordBox) : this()
        {
            _loginPasswordBox = passwordBox;
            _loginPasswordBox.Password = _loginUserPageModel.GetPasswordFromRegister();
            Login = _loginUserPageModel.GetLoginFromRegister();
            

            
        }
        public LoginUserPageViewModel()
        {
            //делегируем методы (метод который срабатывает при нажатии кнопки)(который проверяет условия выполнения метода)
            _multiCommand.Add(new RelayCommand(ExecuteButtonCommand, CanExecuteButtonCommand));
            _multiCommand.Add(new RelayCommand(ExecuteWriteInRegisterCommand));
            _loginDefaultStyle = (Style)Application.Current.Resources["CustomDefaultLoginPageTextBox"];
            LoginTextBoxStyle = _loginDefaultStyle;
            _loginInvalidStyle = (Style)Application.Current.Resources["CustomInvalidLoginPageTextBox"];

        }

        //команда для выполнения нажатия кнопки
        public ICommand ButtonCommand => _multiCommand;

        //свойства 
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
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                UpdateButtonEnagleState();
                ChangeLoginTextBoxStyle();
                OnPropertyChanged(nameof(Login));

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


        private void ExecuteButtonCommand(object parameter)
        {
            new MainWindow().ShowDialog();
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
            _password = _loginPasswordBox.Password;
            if (_isChecked)
                _loginUserPageModel.WriteDataInRegister(_login, _password);
            else
                _loginUserPageModel.WriteDataInRegister(string.Empty, string.Empty);

        }

        private bool CanExecuteButtonCommand(object parameter)
        {

            return _loginUserPageModel.IsValidLogin(Login) && _loginUserPageModel.IsValidPassword(_loginPasswordBox.Password);
        }

        private void UpdateButtonEnagleState()
        {
            IsButtonEnable = CanExecuteButtonCommand(null); // обновления состояния кнопки
            
        }
        private void ChangeLoginTextBoxStyle()
        {
            if (_loginUserPageModel.IsValidLogin(Login))
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

        }



    }
}
