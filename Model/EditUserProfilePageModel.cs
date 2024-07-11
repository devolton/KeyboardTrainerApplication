using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private string _remotePath = @"D:\C#WinForms\CourseProjectKeyboardApplication\Resources\UserAvatars\\";
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
                //1.Копировать фото в место хранения аватаров
                //1.1 Переименовываем на (userId).png
                //2. Создаем BitmapImage на основе нового Uri куда мы копировали фото
                //3. Возвращаем его
                MessageBox.Show(_openFileDialog.FileName);
                var fileName = Path.GetFileName(_openFileDialog.FileName);
                string remotePath = _remotePath + fileName;
                File.Copy(_openFileDialog.FileName, remotePath, true);
                var remoteUri = new Uri(remotePath);
                UpdateUserAvatarPath(remotePath);
                return new BitmapImage(remoteUri);
            }
            return null;

        }
        public void RemoveAvatar()
        {
            //removing remote avatar

        }
        public User GetUserInfo()
        {
            _user = UserController.CurrentUser;
            _oldEmail = _user.Email;
            _oldLogin = _user.Login;
            MessageBox.Show(_user.AvatarPath.ToString());
            return _user;
        }
        public ImageSource? GetUserAvatarSource()
        {
            if(_user.AvatarPath == string.Empty)
            {
                if (File.Exists(_user.AvatarPath))
                {
                    return new BitmapImage(new Uri(_user.AvatarPath)); 
                }
            }
            return null;
        }
        public void SaveUpdateUser(string updateName, string updateLogin, string updateEmail, string updatePassword)
        {
            _user.Login = updateLogin;
            _user.Email = updateEmail;
            _user.Name = updateName;
            if (updatePassword != string.Empty)
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
        private void UpdateUserAvatarPath(string avatarPath)
        {
            _user.AvatarPath = avatarPath;

        }

        //adding method which, if update is succesfull, updates the data in the register or delete data from register

    }
}
