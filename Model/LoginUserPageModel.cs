using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;
using CourseProjectKeyboardApplication.Tools;
using CourseProjectKeyboardApplication.Tools.AuthorizationTools;
using Encrypter;
using KeyboardApplicationToolsLibrary.AuthorizationTools;
using Microsoft.Win32;
using System.Windows;

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
        private DevoltonDecrypter _devoltonDecrypter;

        private bool _isValidUser = false;


        public LoginUserPageModel()
        {
            InitRegistryInfo();
            _devoltonEncrypter = DevoltonEncrypter.Instance();
            _devoltonDecrypter = DevoltonDecrypter.Instance();

        }
        public bool IsValidLogin(string login) => AuthorizationFieldsValidator.IsValidLogin(login);
        public bool IsValidPassword(string password) => AuthorizationFieldsValidator.IsValidPassword(password);
        public bool IsValidEmail(string email) => AuthorizationFieldsValidator.IsValidEmail(email);
        public async Task InitUserInUserController(User user)
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
        public async Task<bool> IsUserExist(string emailOrLogin)
        {
            return await UserService.IsUserExistByLoginOrEmail(emailOrLogin);
            
        }
        /// <summary>
        /// Get user by login (or email) and password 
        /// </summary>
        /// <param name="loginOrEmail">User login or email</param>
        /// <param name="password">user password</param>
        /// <returns></returns>
        public async Task<User?> GetUserByLoginOrEmailAndPassword(string loginOrEmail, string password)
        {
            var encryptSha256Password = PasswordSHA256Encrypter.EncryptPassword(password);
            User? user= await UserService.GetUserByLoginOrEmailAndPasswordAsync(loginOrEmail, encryptSha256Password);
            if(user is not null)
            {
                _isValidUser = true;
            }
            return user;
            
        }
        /// <summary>
        /// Writing users credentials to quickly enter the application when logging in
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        
        public void WriteInRegister(string loginOrEmail,string password, bool isChecked)
        {
            if (_isValidUser)
            {
                if (isChecked)
                    WriteDataInRegister(loginOrEmail, password);
                else
                   WriteNakedDataInRegister();
            }
        }
        private  void WriteDataInRegister(string login, string password)
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
        /// Write credentials in register is user pressed 'Remember me' button and user data is valid
        /// </summary>
        private void WriteNakedDataInRegister()
        {
             Task.Run(() =>
             {
                 _applicationSubKey.SetValue(_userLoginRegistryKeyName, string.Empty);
                 _applicationSubKey.SetValue(_userPasswordRegistryCodeKeyName, string.Empty);
                 _applicationSubKey.SetValue(_userPasswordRegistryKeyName, string.Empty);

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
        /// <summary>
        /// Get password from register 
        /// </summary>
        /// <returns></returns>
        public string GetPasswordFromRegister()
        {
            var obj = _applicationSubKey.GetValue(_userPasswordRegistryKeyName);
            if (obj is not null)
            {
                string encryptPassword = Convert.ToString(obj);
                if (encryptPassword != string.Empty)
                    return _devoltonDecrypter.Decrypt(encryptPassword, GetKeyFromRegister());
            }
            return string.Empty;

        }
        /// <summary>
        /// Get encryptor key from register
        /// </summary>
        /// <returns>Encryptor key</returns>
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
