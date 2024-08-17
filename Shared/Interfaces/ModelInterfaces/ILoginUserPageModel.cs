using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces
{
    public interface ILoginUserPageModel
    {
        string GetPasswordFromRegister();
        string GetLoginFromRegister();
        void WriteInRegister(string loginOrEmail, string password, bool isChecked);
        Task<User?> GetUserByLoginOrEmailAndPassword(string loginOrEmail, string password);
        Task<bool> IsUserExist(string emailOrLogin);
        Task InitUserInUserController(User user);
        bool IsValidEmail(string email);
        bool IsValidLogin(string login);
        bool IsValidPassword(string password);
    }
}
