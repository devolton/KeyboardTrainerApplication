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
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;
using CourseProjectKeyboardApplication.Tools;
using CourseProjectKeyboardApplication.ViewModel;
using KeyboardApplicationToolsLibrary.AuthorizationTools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace CourseProjectKeyboardApplication.Model
{
    public class EditUserProfilePageModel : RegistrationFormModel,IEditUserProfilePageModel
    {
        private string _openFileDialogImageFilter;
        private OpenFileDialog _openFileDialog;
        private string _oldEmail = string.Empty;
        private string _oldLogin = string.Empty;
        private User _user;
        private ImageSource _defaultUserAvatarImageSource;
        private ImageSource _userAvatarImageSource;
        public EditUserProfilePageModel()
        {
            _openFileDialogImageFilter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            _openFileDialog = new OpenFileDialog()
            {
                Filter = _openFileDialogImageFilter
            };


        }
        public ImageSource GetDefaultUserAvatarImageSource()
        {
            _defaultUserAvatarImageSource ??= AppImageSourceProvider.DefaultUserAvatarImageSource;
            return _defaultUserAvatarImageSource;
        }

        public async Task<ImageSource?> LoadNewAvatar()
        {
            if (_openFileDialog.ShowDialog() == true)
            {
                string remoteAvatarFileName = await ContentApiClientProvider.UserAvatarImageApiClient.UploadFileAsync(_openFileDialog.FileName, _user.Login);
                _userAvatarImageSource = await ContentApiClientProvider.UserAvatarImageApiClient.GetUserAvatarAsync(remoteAvatarFileName);
                UpdateUserAvatarPath(remoteAvatarFileName);
                UserService.UpdateUser(_user);
                return _userAvatarImageSource;
            }
            return null;

        }
        public void RemoveAvatar()
        {
            UpdateUserAvatarPath(string.Empty);
            UserService.UpdateUser(_user);

        }
        public User GetUserInfo()
        {
            _user = UserController.CurrentUser;
            _oldEmail = _user.Email;
            _oldLogin = _user.Login;
            return _user;
        }
        public async Task<ImageSource?> GetUserAvatarSource()
        {
            if (_user.AvatarPath != string.Empty)
            {
                _userAvatarImageSource ??= await ContentApiClientProvider.UserAvatarImageApiClient.GetUserAvatarAsync(_user.AvatarPath);
                return _userAvatarImageSource;
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
            UserService.UpdateUser(_user);
        }
        public override async Task<bool> IsUniqueCredentialsAsync(string email, string login)
        {
            _isUniqueEmail = _oldEmail == email || (await UserService.IsUniqueEmailAsync(email));
            _isUniqueLogin = _oldLogin == login || (await UserService.IsUniqueLoginAsync(login));
            return _isUniqueEmail && _isUniqueLogin;
        }
        private void UpdateUserAvatarPath(string avatarPath)
        {
            _user.AvatarPath = avatarPath;

        }
      


    }
}
