using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces
{
    public interface IUserSingUpPageModel :IRegistrationFormModel
    {
        Task<User> RegisterUser(string name, string login, string email, string password);
        
    }
}
