using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CourseProjectKeyboardApplication.Model;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class EditUserProfilePageViewModel : ViewModelBase
    {
        private EditUserProfilePageModel _model;
        private static EditUserProfilePageViewModel _instance;
        private PasswordBox _passwordBox;
        private PasswordBox _confirmPasswordBox;

        private string _name;
        private string _login;
        private string _email;
        private string _password;
        private string _confirmPassword;

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

        }

        public static EditUserProfilePageViewModel Instance()
        {
            _instance ??= new EditUserProfilePageViewModel();
            return _instance;
        }

        //properties
        #region
        public PasswordBox PasswordBox
        {
            get { return _passwordBox; }
            set
            {
                _passwordBox = value;
                OnPropertyChanged(nameof(PasswordBox));
            }

        }

        public PasswordBox ConfirmPasswordBox
        {
            get { return _confirmPasswordBox; }
            set
            {
                _confirmPasswordBox = value;
                OnPropertyChanged(nameof(ConfirmPasswordBox));
            }
        }
        public ICommand LoadUserInfoCommand => _loadUserInfoCommand;
        
        public ICommand RemoveAvatarCommand => _removeAvatarCommand;
        public ICommand ChangeAvatarCommand => _changeAvatarCommand;
        public ICommand SaveChangeCommand => _saveChangeCommand;

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
