using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CourseProjectKeyboardApplication.Tools;
using CourseProjectKeyboardApplication.ViewModel;

namespace CourseProjectKeyboardApplication.Model
{
    public class EditUserProfilePageModel:RegistrationFormModel
    {
        private UserInfo _userInfo;
      
        public EditUserProfilePageModel()
        {
            _userInfo = new UserInfo()
            {
                Id=1,
                Email = "baleb@gmail.com",
                Login = "baza",
                Name = "Antonio",
                Password = "Password123"
            };
            
             
        }
        public UserInfo UserInfo { get { return _userInfo; } set { _userInfo = value; } }
        
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
