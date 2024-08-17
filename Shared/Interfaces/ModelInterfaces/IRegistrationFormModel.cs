using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces
{
    public interface IRegistrationFormModel
    {
        bool IsUniqueEmail();
        bool IsUniqueLogin();
        bool IsValidName(string name);
        bool IsValidPassword(string password);
        bool IsValidEmail(string email);
        bool IsValidLogin(string login);
        Task<bool> IsUniqueCredentialsAsync(string email, string login);
    }
}
