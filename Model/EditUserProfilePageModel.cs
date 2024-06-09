using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CourseProjectKeyboardApplication.Tools;
using CourseProjectKeyboardApplication.ViewModel;
using Microsoft.Win32;

namespace CourseProjectKeyboardApplication.Model
{
    public class EditUserProfilePageModel:RegistrationFormModel
    {
        private UserInfo _userInfo;
        private string _openFileDialogImageFilter;
        private OpenFileDialog _openFileDialog;
      
        public EditUserProfilePageModel()
        {
            _openFileDialogImageFilter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            _openFileDialog = new OpenFileDialog()
            {
                Filter = _openFileDialogImageFilter
            };

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
        public ImageSource? LoadNewAvatar()
        {
            if (_openFileDialog.ShowDialog() == true)
            {
                //adding local
                return new BitmapImage(new Uri(_openFileDialog.FileName));  
            }
            return null;

        }
        public void RemoveAvatar()
        {
            //removing local avatar

        }
        
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarSource { get; set; }

    }
}
