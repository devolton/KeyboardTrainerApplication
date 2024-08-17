
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;
using CourseProjectKeyboardApplication.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Model
{
    public abstract class RegistrationFormModel:IRegistrationFormModel
    {
        protected bool _isUniqueEmail = false;
        protected bool _isUniqueLogin = false;
        public RegistrationFormModel()
        {

        }
        public bool IsUniqueEmail() => _isUniqueEmail;
        public bool IsUniqueLogin() => _isUniqueLogin;
        public bool IsValidName(string name) => AuthorizationFieldsValidator.IsValidName(name);
        public bool IsValidPassword(string password) => AuthorizationFieldsValidator.IsValidPassword(password);
        public bool IsValidEmail(string email) => AuthorizationFieldsValidator.IsValidEmail(email);
        public bool IsValidLogin(string login) => AuthorizationFieldsValidator.IsValidLogin(login);
        public virtual async Task<bool> IsUniqueCredentialsAsync(string email, string login)
        {
            _isUniqueEmail = await UserService.IsUniqueEmailAsync(email);
            _isUniqueLogin = await UserService.IsUniqueLoginAsync(login);
            return _isUniqueLogin && _isUniqueEmail;
        }
    }
}
