using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Model
{
    public abstract class RegistrationFormModel
    {
        protected UserModel _userModel;
        public RegistrationFormModel()
        {
            _userModel = DatabaseModelMediator.UserModel;
        }
        public bool IsValidName(string name) => AuthorizationFieldsValidator.IsValidName(name);
        public bool IsValidPassword(string password) => AuthorizationFieldsValidator.IsValidPassword(password);
        public bool IsValidEmail(string email) => AuthorizationFieldsValidator.IsValidEmail(email);
        public bool IsValidLogin(string login) => AuthorizationFieldsValidator.IsValidLogin(login);
    }
}
