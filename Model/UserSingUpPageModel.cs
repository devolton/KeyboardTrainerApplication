using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Tools;
using KeyboardApplicationToolsLibrary.AuthorizationTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Model
{
    public class UserSingUpPageModel:RegistrationFormModel
    {
        public UserSingUpPageModel()
        {
          
        }
        public bool IsUniqueCreditails(string email, string login)
        {
            return _userModel.IsUniqueLogin(login) && _userModel.IsUniqueEmail(email);

        }
        public User RegisterUser(string name,string login,string email, string password)
        {
            var user = new User
            {
                Name = name,
                Email = email,
                Password = PasswordSHA256Encrypter.EncryptPassword(password),
                EnglishLayoutLessonId=1,
                EnglishLayoutLevelId=1,
                AvatarPath=""

            };
            _userModel.AddNewUser(user);
            return user;
        }

     
    }
}
