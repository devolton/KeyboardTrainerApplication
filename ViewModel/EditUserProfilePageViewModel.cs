using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CourseProjectKeyboardApplication.Model;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class EditUserProfilePageViewModel : ViewModelBase
    {
        private EditUserProfilePageModel _model;
        private static EditUserProfilePageViewModel _instance;

        private bool _isPasswordVisible = false;
        private bool _isConfirmPasswordVisible = false;

        private string _name;
        private string _login;
        private string _email;
        private string _password;
        private string _confirmPassword;

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

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
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
            return true;
        }
        private bool IsSaveChangeButtonEnabled()
        {
            return CanSaveChangeCommandExecute(null);
        }
        private bool IsRemoveAvatarButtonEnabled()
        {
            return CanRemoveAvatarCommandExecute(null);
        }


        #endregion
    }
}
