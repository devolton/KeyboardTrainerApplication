﻿using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Shared.Providers;
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
        private readonly ICommand _notificationCommand;
        private readonly IUserSingUpPageModel _model;
        public UserSingupPageViewModel()
        {
            _singUpClickButtonMultiCommand = new MultiCommand();
            _passwordVisibilityCommand = new RelayCommand(OnPasswordVisibilityCommand);
            _confirmPasswordVisibilityCommand = new RelayCommand(OnConfirmPasswordVisibilityCommand);
            _notificationCommand = new RelayCommand(OnNotificationCommand);
            _model = new UserSingUpPageModel();
            InitStyles();
            _singUpClickButtonMultiCommand.Add(new RelayCommand(OnTryRegisterUser, IsUserRegistrationCommandExecuted));

        }
       
    

        //commands
        #region
        /// <summary>
        /// Command of displaying notification page
        /// </summary>
        /// <param name="param"></param>
        private void OnNotificationCommand(object param)
        {
            NotifyType notifyType = (NotifyType)param;
            NotificationMediator.ShowNotificationWindow(notifyType);
        }
     
        /// <summary>
        /// Command of trying registration oparation new user
        /// </summary>
        /// <param name="param"></param>
        private async void OnTryRegisterUser(object param = null)
        {
            if (await _model.IsUniqueCredentialsAsync(UserEmail, UserLogin))
            {
               var newUser= await _model.RegisterUser(UserName, UserLogin, UserEmail, Password);
                if(newUser is null)
                {
                    MessageBox.Show("Invalid registerUserOperation");
                   
                    return;
                }
                ClearAllFields();
                await AppImageSourceProvider.Init();
                KeyboardAppEducationProgressController.CurrentUser=newUser;
                new MainWindow().Show();

                foreach (Window oneWindow in Application.Current.Windows)
                {
                    if (oneWindow is AuthorizationWindow)
                    {
                        var autorizationWindow = oneWindow as AuthorizationWindow;
                        autorizationWindow.IsDisposableHttpClients = false;
                        autorizationWindow.Close();
                        return;
                    }
                }
            }
            else
            {
                if (!_model.IsUniqueLogin())
                {
                    NotificationMediator.ShowNotificationWindow(NotifyType.NotUniqueLogin);
                    ChangeLoginTextBoxStyleAsync();

                }
                if (!_model.IsUniqueEmail())
                {
                    NotificationMediator.ShowNotificationWindow(NotifyType.NotUniqueEmail);
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
                ConfirmPasswordBoxVisibility = Visibility.Visible;
                ConfirmPasswordTextBoxVisibility = Visibility.Collapsed;
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
        public ICommand NotificationCommand => _notificationCommand;
        public ICommand SingUpClickCommand => _singUpClickButtonMultiCommand;
        public ICommand PasswordVisibilityCommand => _passwordVisibilityCommand;
        public ICommand ConfirmPasswordVisibilityCommand => _confirmPasswordVisibilityCommand;
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
