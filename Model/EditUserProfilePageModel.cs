using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Tools;
using CourseProjectKeyboardApplication.ViewModel;
using KeyboardApplicationToolsLibrary.AuthorizationTools;
using Microsoft.Win32;

namespace CourseProjectKeyboardApplication.Model
{
    public class EditUserProfilePageModel : RegistrationFormModel
    {
        private string _openFileDialogImageFilter;
        private OpenFileDialog _openFileDialog;
        private string _oldEmail = string.Empty;
        private string _oldLogin = string.Empty;
        private User _user;

        public EditUserProfilePageModel()
        {
            _openFileDialogImageFilter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            _openFileDialog = new OpenFileDialog()
            {
                Filter = _openFileDialogImageFilter
            };
            _userModel = DatabaseModelMediator.UserModel;



        }

        public ImageSource? LoadNewAvatar()
        {
            if (_openFileDialog.ShowDialog() == true)
            {
                //adding local
                return new BitmapImage(new Uri(_openFileDialog.FileName));
            }
            return null;

        }
        public void RemoveAvatar()
        {
            //removing local avatar

        }
        public User GetUserInfo()
        {
            _user = UserController.CurrentUser;
            _oldEmail = _user.Email;
            _oldLogin = _user.Login;
            return _user;
        }
        public void SaveUpdateUser(string updateName, string updateLogin, string updateEmail, string updatePassword)
        {
            _user.Login = updateLogin;
            _user.Email = updateEmail;
            _user.Name = updateName;
            if (updatePassword != "")
            {
                _user.Password = PasswordSHA256Encrypter.EncryptPassword(updatePassword);
            }
            _userModel.SaveChangesAsync();
        }
        public override bool IsUniqueCredentials(string email, string login)
        {
            _isUniqueEmail = _oldEmail == email || _userModel.IsUniqueEmail(email);
            _isUniqueLogin = _oldLogin == login || _userModel.IsUniqueLogin(login);
            return _isUniqueEmail && _isUniqueLogin;
        }

        //adding method which, if update is succesfull, updates the data in the register or delete data from register

    }
}
