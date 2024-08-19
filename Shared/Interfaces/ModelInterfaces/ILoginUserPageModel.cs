using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Enums;
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
        Task<KeyValuePair<bool,NotifyType>> IsUserExist(string emailOrLogin);
        Task InitUserInUserController(User user);
        bool IsValidEmail(string email);
        bool IsValidLogin(string login);
        bool IsValidPassword(string password);
    }
}
