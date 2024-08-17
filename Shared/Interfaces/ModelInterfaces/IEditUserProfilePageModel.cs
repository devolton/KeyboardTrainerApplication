using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces
{
    public interface IEditUserProfilePageModel:IRegistrationFormModel
    {
        ImageSource GetDefaultUserAvatarImageSource();
        Task<ImageSource?> LoadNewAvatar();
        Task<ImageSource?> GetUserAvatarSource();
        void RemoveAvatar();
        User GetUserInfo();
        void SaveUpdateUser(string updateName, string updateLogin, string updateEmail, string updatePassword);
    }
}
