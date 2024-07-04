using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Tools;
using Encrypter;
using KeyboardApplicationToolsLibrary.AuthorizationTools;
using Microsoft.Win32;

namespace CourseProjectKeyboardApplication.Model
{
    public class LoginUserPageModel
    {
        private RegistryKey _currentUserRegistrieSubKey;
        private RegistryKey _applicationSubKey;
        private string _applicatoinSubKeyName;
        private string _userLoginRegistryKeyName;
        private string _userPasswordRegistryKeyName;
        private string _userPasswordRegistryCodeKeyName;
        private DevoltonEncrypter _devoltonEncrypter;
        private UserModel _userModel;

        public LoginUserPageModel()
        {
            InitRegistryInfo();
            _devoltonEncrypter = DevoltonEncrypter.GetInstance();
            _userModel = DatabaseModelMediator.UserModel;

        }
        public bool IsValidLogin(string login) => AuthorizationFieldsValidator.IsValidLogin(login);
        public bool IsValidPassword(string password) => AuthorizationFieldsValidator.IsValidPassword(password);
        public bool IsValidEmail(string email) => AuthorizationFieldsValidator.IsValidEmail(email);
        public void InitUserInUserController(User user)
        {
            UserController.CurrentUser = user;
        }
        private void InitRegistryInfo()
        {
            _currentUserRegistrieSubKey = Registry.CurrentUser;
            _applicatoinSubKeyName = "Keyboard Trainer Data";
            _userLoginRegistryKeyName = "Login";
            _userPasswordRegistryCodeKeyName = "Key";
            _userPasswordRegistryKeyName = "Password";
            _applicationSubKey = _currentUserRegistrieSubKey.CreateSubKey(_applicatoinSubKeyName);

        }
        /// <summary>
        /// check is user with this login or password exist
        /// </summary>
        /// <param name="emailOrLogin">Login or email entered by the user</param>
        /// <returns></returns>
        public bool IsUserExist(string emailOrLogin)
        {
            return _userModel.IsUserExistByEmail(emailOrLogin) || _userModel.IsUserExistByLogin(emailOrLogin);
        }
        public User? GetUserByLoginOrEmailAndPassword(string loginOrEmail, string password)
        {
            var encryptSha256Password = PasswordSHA256Encrypter.EncryptPassword(password);
            return _userModel.GetUserByLoginOrEmailAndPassword(loginOrEmail,encryptSha256Password);
        }
        /// <summary>
        /// Writing users credentials to quickly enter the application when logging in
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        public  void WriteDataInRegister(string login, string password)
        {
            Task.Run(() =>
            {
                 string sha256KeyCode = PasswordSHA256Encrypter.EncryptPassword(password);
                _applicationSubKey.SetValue(_userLoginRegistryKeyName, login);
                _applicationSubKey.SetValue(_userPasswordRegistryCodeKeyName, sha256KeyCode);
                _applicationSubKey.SetValue(_userPasswordRegistryKeyName, _devoltonEncrypter.Encrypt(password, sha256KeyCode));

            });
     
        }
        /// <summary>
        /// Get login from register if the user saved it earlier
        /// </summary>
        /// <returns>User login from register or empty str</returns>
        public string GetLoginFromRegister()
        {
            var login = _applicationSubKey.GetValue(_userLoginRegistryKeyName);

            if (login is not null)
            {
                return Convert.ToString(login);
            }
            return string.Empty;

        }
        public string GetPasswordFromRegister()
        {
            var obj = _applicationSubKey.GetValue(_userPasswordRegistryKeyName);
            if (obj is not null)
            {
                string encryptPassword = Convert.ToString(obj);
                return _devoltonEncrypter.Decrypt(encryptPassword, GetKeyFromRegister());
            }
            return string.Empty;

        }
        private string GetKeyFromRegister()
        {
            var keyObj = _applicationSubKey.GetValue(_userPasswordRegistryCodeKeyName);
            if (keyObj is not null)
            {
                return Convert.ToString(keyObj);

            }
            return string.Empty;
        }




    }
}
