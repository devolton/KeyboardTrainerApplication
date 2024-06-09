using CourseProjectKeyboardApplication.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Model
{
    public abstract class RegistrationFormModel
    {
        public bool IsValidName(string name) => AuthorizationFieldsValidator.IsValidName(name);
        public bool IsValidPassword(string password) => AuthorizationFieldsValidator.IsValidPassword(password);
        public bool IsValidEmail(string email) => AuthorizationFieldsValidator.IsValidEmail(email);
        public bool IsValidLogin(string login) => AuthorizationFieldsValidator.IsValidLogin(login);
    }
}
