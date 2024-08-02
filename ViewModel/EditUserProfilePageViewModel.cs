using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CourseProjectKeyboardApplication.Model;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class EditUserProfilePageViewModel : RegistrationFormViewModel
    {
        private EditUserProfilePageModel _model;
        private static EditUserProfilePageViewModel _instance;
        private ImageSource _avatarSource;
        private ImageSource _defaultAvatar;
        private bool _isSetDefaultAvatar = true;
        private bool _isRemoveAvatarButtonEnabled = false;

        private ICommand _loadUserInfoCommand;
        private ICommand _removeAvatarCommand;
        private ICommand _changeAvatarCommand;
        private ICommand _saveChangeCommand;


        private EditUserProfilePageViewModel()
        {
            _model = new EditUserProfilePageModel();
            _removeAvatarCommand = new RelayCommand(OnRemoveAvatarCommand, CanRemoveAvatarCommandExecute);
            _changeAvatarCommand = new RelayCommand(OnChangeAvatarCommmand);
            _saveChangeCommand = new RelayCommand(OnSaveChangeCommand, CanSaveChangeCommandExecute);
            _loadUserInfoCommand = new RelayCommand(OnLoadUserInfoCommand);
            _passwordVisibilityCommand = new RelayCommand(OnPasswordVisibilityCommand);
            _confirmPasswordVisibilityCommand = new RelayCommand(OnConfirmPasswordVisibilityCommand);
            this.InitStyles();
            _defaultAvatar = Application.Current.Resources["ApplicationLogo"] as ImageSource; ;

        }



        public static EditUserProfilePageViewModel Instance()
        {
            _instance ??= new EditUserProfilePageViewModel();
            return _instance;
        }

        //properties
        #region

        public ICommand LoadUserInfoCommand => _loadUserInfoCommand;

        public ICommand RemoveAvatarCommand => _removeAvatarCommand;
        public ICommand ChangeAvatarCommand => _changeAvatarCommand;
        public ICommand SaveChangeCommand => _saveChangeCommand;
        public ICommand PasswordVisibilityCommand => _passwordVisibilityCommand;
        public ICommand ConfirmPasswordVisibilityCommand => _confirmPasswordVisibilityCommand;

        public bool IsSaveButtonEnabled
        {
            get => _isEnabledRegistrationButton;
            set
            {
                _isEnabledRegistrationButton = value;
                OnPropertyChanged(nameof(IsSaveButtonEnabled));
            }

        }
        public bool IsRemoveAvatarButtonEnabled
        {
            get => _isRemoveAvatarButtonEnabled;
            set
            {
                _isRemoveAvatarButtonEnabled = value;
                OnPropertyChanged(nameof(IsRemoveAvatarButtonEnabled));
            }
        }

        public ImageSource AvatarSource
        {
            get => _avatarSource;
            set
            {
                _avatarSource = value;
                OnPropertyChanged(nameof(AvatarSource));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                CanEnabledSaveButton();
                NameTextBoxStyle = ChangeTextBoxStyle(_model.IsValidName, Name);
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                CanEnabledSaveButton();
                LoginTextBoxStyle = ChangeTextBoxStyle(_model.IsValidLogin, Login);
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                CanEnabledSaveButton();
                EmailTextBoxStyle = ChangeTextBoxStyle(_model.IsValidEmail, Email);
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                CanEnabledSaveButton();
                PasswordBoxStyle = ChangePasswordBoxStyle(_model.IsValidPassword, Password);
                PasswordTextBoxStyle = ChangeTextBoxStyle(_model.IsValidPassword, Password);
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                CanEnabledSaveButton();
                ConfirmPasswordBoxStyle = ChangePasswordBoxStyle(_model.IsValidPassword, ConfirmPassword);
                ConfirmPasswordTextBoxStyle = ChangeTextBoxStyle(_model.IsValidPassword, ConfirmPassword);
                OnPropertyChanged(nameof(ConfirmPassword));
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
        //command 
        #region
        private void OnRemoveAvatarCommand(object param)
        {
            AvatarSource = _defaultAvatar;
            IsRemoveAvatarButtonEnabled = false;
            _isSetDefaultAvatar = true;
            _model.RemoveAvatar();
        }
        private void OnChangeAvatarCommmand(object param)
        {
            var newAvatarSource = _model.LoadNewAvatar();
            if (newAvatarSource != null)
            {
                AvatarSource = newAvatarSource;
                IsRemoveAvatarButtonEnabled = true;
                _isSetDefaultAvatar = false;
                
            }

        }
        private async void OnSaveChangeCommand(object param)
        {
            if (await _model.IsUniqueCredentialsAsync(Email, Login))
            {
                _model.SaveUpdateUser(Name, Login, Email, Password);
            }
            else if (!_model.IsUniqueEmail())
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ChangeEmailTextBoxStyleAsync();
                });

            }
            else if (!_model.IsUniqueLogin())
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ChangeLoginTextBoxStyleAsync();
                });
            }


        }
        private void OnLoadUserInfoCommand(object param)
        {
            var user = _model.GetUserInfo();
            Login = user.Login;
            Name = user.Name;
            Email = user.Email;
            var userAvatar = _model.GetUserAvatarSource();
            if (userAvatar is null)
            {
                AvatarSource = _defaultAvatar;
            }
            else
            {
                AvatarSource = userAvatar;
                _isSetDefaultAvatar = false;
                IsRemoveAvatarButtonEnabled = true;
            }


        }
        protected override void OnConfirmPasswordVisibilityCommand(object param)
        {
            if (_isConfirmPasswordVisible)
            {
                _isConfirmPasswordVisible = false;
                ConfirmPasswordTextBoxVisibility = Visibility.Collapsed;
                ConfirmPasswordTextDecorationCollection = TextDecorations.Strikethrough;
                ConfirmPasswordBoxVisibility = Visibility.Visible;
            }
            else
            {
                _isConfirmPasswordVisible = true;
                ConfirmPasswordBoxVisibility = Visibility.Collapsed;
                ConfirmPasswordTextBoxVisibility = Visibility.Visible;
                ConfirmPasswordTextDecorationCollection = null;

            }

        }


        protected override void OnPasswordVisibilityCommand(object param)
        {
            if (_isPasswordVisible)
            {
                _isPasswordVisible = false;
                PasswordTextBoxVisibility = Visibility.Collapsed;
                PasswordTextDecorationCollection = TextDecorations.Strikethrough;
                PasswordBoxVisibility = Visibility.Visible;
            }
            else
            {
                _isPasswordVisible = true;
                PasswordBoxVisibility = Visibility.Collapsed;
                PasswordTextBoxVisibility = Visibility.Visible;
                PasswordTextDecorationCollection = null;

            }

        }
        #endregion
        //command predicate
        #region 
        private bool CanRemoveAvatarCommandExecute(object param)
        {
            return !_isSetDefaultAvatar;
        }
        private bool CanSaveChangeCommandExecute(object param)
        {
            return _model.IsValidName(Name) && _model.IsValidEmail(Email) && _model.IsValidLogin(Login) && ((_model.IsValidPassword(Password)
                && ConfirmPassword.Equals(Password)) || (Password.Equals(string.Empty) && ConfirmPassword.Equals(string.Empty)));
        }

        private void CanEnabledSaveButton() => IsSaveButtonEnabled = CanSaveChangeCommandExecute(null);

        #endregion

        protected override void InitStyles()
        {
            _defaultTextBoxStyle = Application.Current.Resources["CustomDefaultEditProfileTextBox"] as Style;
            _errorTextBoxStyle = Application.Current.Resources["CustomErrorEditProfileTextBox"] as Style;
            _defaultPasswordBoxStyle = Application.Current.Resources["CustomDefaultEditUserProfilePasswordBox"] as Style;
            _errorPasswordBoxStyle = Application.Current.Resources["CustomErrorEditUserProfilePasswordBox"] as Style;
            NameTextBoxStyle = _defaultTextBoxStyle;
            LoginTextBoxStyle = _defaultTextBoxStyle;
            EmailTextBoxStyle = _defaultTextBoxStyle;
            PasswordBoxStyle = _defaultPasswordBoxStyle;
            PasswordTextBoxStyle = _defaultTextBoxStyle;
            ConfirmPasswordBoxStyle = _defaultPasswordBoxStyle;
            ConfirmPasswordTextBoxStyle = _defaultTextBoxStyle;


        }
        private Task ChangeLoginTextBoxStyleAsync()
        {
            return Task.Run(async () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LoginTextBoxStyle = _errorTextBoxStyle;

                });
                await Task.Delay(2000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LoginTextBoxStyle = _defaultTextBoxStyle;
                });

            });
        }
        private Task ChangeEmailTextBoxStyleAsync()
        {
            return Task.Run(async () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    EmailTextBoxStyle = _errorTextBoxStyle;

                });
                await Task.Delay(2000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    EmailTextBoxStyle = _defaultTextBoxStyle;
                });

            });
        }
    }
}
