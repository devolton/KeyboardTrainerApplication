using System;
using System.Collections.Generic;
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
    public class EditUserProfilePageViewModel : ViewModelBase
    {
        private EditUserProfilePageModel _model;
        private static EditUserProfilePageViewModel _instance;
        private ImageSource _avatarSource;

        private bool _isPasswordVisible = false;
        private bool _isConfirmPasswordVisible = false;
        private bool _isSaveButtonEnabled = false;

        private string _name = string.Empty;
        private string _login = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;

        private ICommand _loadUserInfoCommand;
        private ICommand _removeAvatarCommand;
        private ICommand _changeAvatarCommand;
        private ICommand _saveChangeCommand;
        private ICommand _passwordVisibilityCommand;
        private ICommand _confirmPasswordVisibilityCommand;

        private Visibility _passwordBoxVisibility = Visibility.Visible;
        private Visibility _passwordTextBoxVisibility = Visibility.Collapsed;
        private Visibility _confirmPasswordBoxVisibility = Visibility.Visible;
        private Visibility _confirmPasswordTextBoxVisibility = Visibility.Collapsed;

        private TextDecorationCollection _passwordEyeButtonTextDecorationCollection = TextDecorations.Strikethrough;
        private TextDecorationCollection _confirmPasswordEyeButtonTextDecorationCollection = TextDecorations.Strikethrough;



        private EditUserProfilePageViewModel()
        {
            _model = new EditUserProfilePageModel();
            _removeAvatarCommand = new RelayCommand(OnRemoveAvatarCommand, CanRemoveAvatarCommandExecute);
            _changeAvatarCommand = new RelayCommand(OnChangeAvatarCommmand);
            _saveChangeCommand = new RelayCommand(OnSaveChangeCommand, CanSaveChangeCommandExecute);
            _loadUserInfoCommand = new RelayCommand(OnLoadUserInfoCommand);
            _passwordVisibilityCommand = new RelayCommand(OnPasswordVisibilityCommand);
            _confirmPasswordVisibilityCommand = new RelayCommand(OnConfirmPasswordVisibilityCommand);
            _avatarSource = Application.Current.Resources["ApplicationLogo"] as ImageSource;

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
        public ICommand PasswordVisibilityCommand=> _passwordVisibilityCommand;
        public ICommand ConfirmPasswordVisibilityCommand => _confirmPasswordVisibilityCommand;

        public bool IsSaveButtonEnabled
        {
            get => _isSaveButtonEnabled;
            set
            {
                _isSaveButtonEnabled = value;
                OnPropertyChanged(nameof(IsSaveButtonEnabled));
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
            get=> _confirmPasswordBoxVisibility;
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
        public TextDecorationCollection PasswordEyeButtonTextDecorationCollection
        {
            get => _passwordEyeButtonTextDecorationCollection;
            set
            {
                _passwordEyeButtonTextDecorationCollection = value;
                OnPropertyChanged(nameof(PasswordEyeButtonTextDecorationCollection));
            }
        }

        public TextDecorationCollection ConfirmPasswordTextDecorationCollection
        {
            get => _confirmPasswordEyeButtonTextDecorationCollection;
            set
            {
                _confirmPasswordEyeButtonTextDecorationCollection = value;
                OnPropertyChanged(nameof(ConfirmPasswordTextDecorationCollection));

            }
        }
        #endregion
        //command 
        #region
        private void OnRemoveAvatarCommand(object param)
        {

        }
        private void OnChangeAvatarCommmand(object param)
        {

        }
        private void OnSaveChangeCommand(object param)
        {

        }
        private void OnLoadUserInfoCommand(object param)
        {
            Login = _model.UserInfo.Login;
            Password = _model.UserInfo.Password;
            ConfirmPassword = _model.UserInfo.Password;
            Name= _model.UserInfo.Name;
            Email = _model.UserInfo.Email;


        }
        private void OnConfirmPasswordVisibilityCommand(object param)
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

        private void OnPasswordVisibilityCommand(object param)
        {
            if (_isPasswordVisible)
            {
                _isPasswordVisible = false;
                PasswordTextBoxVisibility = Visibility.Collapsed;
                PasswordEyeButtonTextDecorationCollection = TextDecorations.Strikethrough;
                PasswordBoxVisibility = Visibility.Visible;
            }
            else
            {
                _isPasswordVisible = true;
                PasswordBoxVisibility = Visibility.Collapsed;
                PasswordTextBoxVisibility = Visibility.Visible;
                PasswordEyeButtonTextDecorationCollection = null;

            }

        }
        #endregion
        //command predicate
        #region 
        private bool CanRemoveAvatarCommandExecute(object param)
        {
            return true;
        }
        private bool CanSaveChangeCommandExecute(object param)
        {
            return _model.IsValidName(Name) && _model.IsValidEmail(Email) && _model.IsValidPassword(Password) && _model.IsValidLogin(Login)
                && Password.Equals(ConfirmPassword);
        }
        private bool IsSaveChangeButtonEnabled()
        {
            return CanSaveChangeCommandExecute(null);
        }
        private bool IsRemoveAvatarButtonEnabled()
        {
            return CanRemoveAvatarCommandExecute(null);
        }
        private void CanEnabledSaveButton() => IsSaveButtonEnabled = CanSaveChangeCommandExecute(null);

        #endregion
    }
}
