using CourseProjectKeyboardApplication.Tools;
using Encrypter;
using KeyboardApplicationToolsLibrary.AuthorizationTools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

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

        public LoginUserPageModel()
        {
            InitRegistryInfo();
            _devoltonEncrypter = DevoltonEncrypter.GetInstance();

        }
        public bool IsValidLogin(string login) => AuthorizationFieldsValidator.IsValidLogin(login);
        public bool IsValidPassword(SecureString secureString) => AuthorizationFieldsValidator.IsValidPassword(secureString);
        public bool IsValidPassword(string password) => AuthorizationFieldsValidator.IsValidPassword(password);
        public bool IsValidEmail(string email) => AuthorizationFieldsValidator.IsValidEmail(email);
        private void InitRegistryInfo()
        {
            _currentUserRegistrieSubKey = Registry.CurrentUser;
            _applicatoinSubKeyName = "Keyboard Trainer Data";
            _userLoginRegistryKeyName = "Login";
            _userPasswordRegistryCodeKeyName = "Key";
            _userPasswordRegistryKeyName = "Password";
            _applicationSubKey = _currentUserRegistrieSubKey.CreateSubKey(_applicatoinSubKeyName);

        }
        public void WriteDataInRegister(string login, string password)
        {
            string sha256KeyCode = PasswordSHA256Encrypter.EncryptPassword(password);
            _applicationSubKey.SetValue(_userLoginRegistryKeyName, login);
            _applicationSubKey.SetValue(_userPasswordRegistryCodeKeyName, sha256KeyCode);
            _applicationSubKey.SetValue(_userPasswordRegistryKeyName, _devoltonEncrypter.Encrypt(password, sha256KeyCode));
        }
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
