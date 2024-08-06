
using CourseProjectKeyboardApplication.Shared.Mediators;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public abstract class RegistrationFormViewModel : ViewModelBase
    {
        protected string _name = string.Empty;
        protected string _login = string.Empty;
        protected string _email = string.Empty;
        protected string _password = string.Empty;
        protected string _confirmPassword = string.Empty;

        protected bool _isPasswordVisible = false;
        protected bool _isEnabledRegistrationButton = false;
        protected bool _isConfirmPasswordVisible = false;

        protected Visibility _passwordBoxVisibility = Visibility.Visible;
        protected Visibility _passwordTextBoxVisibility = Visibility.Collapsed;
        protected Visibility _confirmPasswordBoxVisibility = Visibility.Visible;
        protected Visibility _confirmPasswordTextBoxVisibility = Visibility.Collapsed;
        protected TextDecorationCollection _passwordTextDecorationCollection = TextDecorations.Strikethrough;
        protected TextDecorationCollection _confirmPasswordTextDecorationCollection = TextDecorations.Strikethrough;

        protected ICommand _passwordVisibilityCommand;
        protected ICommand _confirmPasswordVisibilityCommand;

        


        protected Style _defaultTextBoxStyle;
        protected Style _errorTextBoxStyle;
        protected Style _defaultPasswordBoxStyle;
        protected Style _errorPasswordBoxStyle;
        protected Style _nameTextBoxStyle;
        protected Style _loginTextBoxStyle;
        protected Style _emailTextBoxStyle;
        protected Style _passwordBoxStyle;
        protected Style _passwordTextBoxStyle;
        protected Style _confirmPasswordBoxStyle;
        protected Style _confirmPasswordTextBoxStyle;

        //properties
        #region 
        public Style NameTextBoxStyle
        {
            get => _nameTextBoxStyle;
            set
            {
                _nameTextBoxStyle = value;
                OnPropertyChanged(nameof(NameTextBoxStyle));
            }
        }

        public Style LoginTextBoxStyle
        {
            get => _loginTextBoxStyle;
            set
            {
                _loginTextBoxStyle = value;
                OnPropertyChanged(nameof(LoginTextBoxStyle));
            }
        }
        public Style EmailTextBoxStyle
        {
            get =>_emailTextBoxStyle;
            set
            {
                _emailTextBoxStyle = value;
                OnPropertyChanged(nameof(EmailTextBoxStyle));
            }
        }
        public Style PasswordBoxStyle
        {
            get => _passwordBoxStyle;
            set
            {
                _passwordBoxStyle = value;
                OnPropertyChanged(nameof(PasswordBoxStyle));
            }
        }
        public Style PasswordTextBoxStyle
        {
            get => _passwordTextBoxStyle;
            set
            {
                _passwordTextBoxStyle = value;
                OnPropertyChanged(nameof(PasswordTextBoxStyle));
            }
        }

        public Style ConfirmPasswordBoxStyle
        {
            get => _confirmPasswordBoxStyle;
            set
            {
                _confirmPasswordBoxStyle = value;
                OnPropertyChanged(nameof(ConfirmPasswordBoxStyle));
            }
        }
        public Style ConfirmPasswordTextBoxStyle
        {
            get => _confirmPasswordTextBoxStyle;
            set
            {
                _confirmPasswordTextBoxStyle = value;
                OnPropertyChanged(nameof(ConfirmPasswordTextBoxStyle));
            }
        }
        public TextDecorationCollection PasswordTextDecorationCollection
        {
            get => _passwordTextDecorationCollection;
            set
            {
                _passwordTextDecorationCollection = value;
                OnPropertyChanged(nameof(PasswordTextDecorationCollection));
            }
        }
        public TextDecorationCollection ConfirmPasswordTextDecorationCollection
        {
            get => _confirmPasswordTextDecorationCollection;
            set
            {
                _confirmPasswordTextDecorationCollection = value;
                OnPropertyChanged(nameof(ConfirmPasswordTextDecorationCollection));
            }
        }

        #endregion
        //methods
        #region
        
        protected virtual void InitStyles()
        {
            _defaultTextBoxStyle = (Style)Application.Current.Resources["CustomDefaultSingUpPageTextBox"];
            _errorTextBoxStyle = (Style)Application.Current.Resources["CustomInvalidSingUpPageTextBox"];
            _defaultPasswordBoxStyle = (Style)Application.Current.Resources["CustomDefaultSingUpPagePasswordBox"];
            _errorPasswordBoxStyle = (Style)Application.Current.Resources["CustomInvalidSingUpPagePasswordBox"];
            NameTextBoxStyle = _defaultTextBoxStyle;
            LoginTextBoxStyle = _defaultTextBoxStyle;
            EmailTextBoxStyle = _defaultTextBoxStyle;
            PasswordBoxStyle = _defaultPasswordBoxStyle;
            PasswordTextBoxStyle = _defaultTextBoxStyle;
            ConfirmPasswordBoxStyle = _defaultPasswordBoxStyle;
            ConfirmPasswordTextBoxStyle = _defaultTextBoxStyle;
        }
        protected virtual void ClearAllFields()
        {
            _name = string.Empty;
            _login = string.Empty;
            _email = string.Empty;
            _password = string.Empty;
            _confirmPassword = string.Empty;
        }

        protected virtual Style ChangeTextBoxStyle(Func<string, bool> validationFunc, string data)
        {
            if (data.Equals(string.Empty) || validationFunc.Invoke(data))
            {
                return _defaultTextBoxStyle;

            }
            return _errorTextBoxStyle;

        }
        protected virtual Style ChangePasswordBoxStyle(Func<string, bool> validationFunc, string data)
        {
            string comparedValue = (nameof(data) == "Password") ? _confirmPassword : _password;
            if (data.Equals(string.Empty) || (validationFunc.Invoke(data) && data.Equals(comparedValue)))
            {
                return _defaultPasswordBoxStyle;
            }
            return _errorPasswordBoxStyle;

        }
        protected abstract void OnConfirmPasswordVisibilityCommand(object param);
        protected abstract void OnPasswordVisibilityCommand(object param);
        #endregion
    }
}
