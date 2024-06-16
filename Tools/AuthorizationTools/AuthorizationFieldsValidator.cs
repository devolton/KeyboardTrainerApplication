using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Tools
{
    public static  class AuthorizationFieldsValidator
    {
        private static Regex _loginRegex;
        private static Regex _passwordRegex;
        private static Regex _emailRegex;
        private static  Regex _nameRegex;
        private static Regex _fullNameRegex;
   
        static AuthorizationFieldsValidator()
        {
            _loginRegex = new Regex("^[a-zA-Z0-9_]{3,16}$");
            _passwordRegex = new Regex("^(?=.*[A-Z])(?=.*\\d)[A-Za-z0-9_]{8,32}$");
            _emailRegex = new Regex("^[\\w\\d\\.\\-\\+]{1,24}\\@[a-z]{1,10}\\.[a-z]{2,8}$");
            _fullNameRegex = new Regex("^(?=.{1,32}$)[А-ЯЁA-Z][а-яёa-z]+\\s[А-ЯЁA-Z][а-яёa-z]+\\s[А-ЯЁA-Z][а-яёa-z]+$");
            _nameRegex = new Regex("^(?=.{2,32}$)[А-ЯЁA-Z][а-яёa-z]+$");
        }
        public static bool IsValidLogin(string login) => _loginRegex.IsMatch(login);
        public static bool IsValidPassword(string password) => _passwordRegex.IsMatch(password);
        public static bool IsValidEmail(string email) => _emailRegex.IsMatch(email);
        public static bool IsValidFullName(string fullName) => _fullNameRegex.IsMatch(fullName);
        public static bool IsValidName(string name) => _nameRegex.IsMatch(name);
    }
}
