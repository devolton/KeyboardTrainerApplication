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
using Microsoft.EntityFrameworkCore;
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
        private BitmapImage _userAvatarBitmapImage;

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
                var avatarFileName = Path.GetFileName(_openFileDialog.FileName); // имя файла 
                string remoteAvatarPathBeforeRename = _remotePath + avatarFileName; //полный удаленный путь файла
                File.Copy(_openFileDialog.FileName, remoteAvatarPathBeforeRename, true); // копирование файла в удаленный репозиторий 
                string remoteAvatarPathAfterRename = GetRenamedUserAvatarPath(remoteAvatarPathBeforeRename);

                var remoteRepositoryAvatarUri = new Uri(remoteAvatarPathAfterRename);
                UpdateUserAvatarPath(remoteAvatarPathAfterRename);
                _userAvatarBitmapImage = new BitmapImage(remoteRepositoryAvatarUri);
                return _userAvatarBitmapImage;
            }
            return null;

        }
        public void RemoveAvatar()
        {
            UpdateUserAvatarPath(string.Empty);

        }
        public User GetUserInfo()
        {
            _user = UserController.CurrentUser;
            _oldEmail = _user.Email;
            _oldLogin = _user.Login;
            return _user;
        }
        public ImageSource? GetUserAvatarSource()
        {
            if(_user.AvatarPath != string.Empty)
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
        private string GetRenamedUserAvatarPath(string remoteFilePathBeforeRename)
        {
            string newUserAvatarName = _user.Id.ToString()+"_" + Guid.NewGuid();//maybe update or using other 
            string avatarFileExtension = Path.GetExtension(remoteFilePathBeforeRename);
            string remoteAvatarPathAfterRename = Path.Combine(_remotePath, newUserAvatarName + avatarFileExtension);
            if (File.Exists(remoteAvatarPathAfterRename))
            {
                //выполнять удаление аватара удаленно после того как user закрыл приложение
            }
            File.Move(remoteFilePathBeforeRename, remoteAvatarPathAfterRename);
            return remoteAvatarPathAfterRename;
        }


    }
}
