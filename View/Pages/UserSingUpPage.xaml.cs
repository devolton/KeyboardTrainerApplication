using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProjectKeyboardApplication.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserSingInPage.xaml
    /// </summary>
    public partial class UserSingInPage : Page
    {
        private UserSingupPageViewModel _viewModel;
        public UserSingInPage()
        {
            InitializeComponent();
            _viewModel = new UserSingupPageViewModel();
            DataContext = _viewModel;
        }

        private void InfoIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            if (img is not null)
            {
                string tagValue= img.Tag as string;
                if (!string.IsNullOrEmpty(tagValue)) {

                    NotifyType notifyType = FromStrToNotifyType(tagValue);
                    _viewModel.NotificationCommand.Execute(notifyType);
                }
            }
            
        }
        private NotifyType FromStrToNotifyType(string tag)
        {
            switch (tag)
            {
                case "Login":
                    {
                        return NotifyType.InvalidLogin;
                    }
                case "Email":
                    {
                        return NotifyType.InvalidEmail;
                    }
                case "Password":
                    {
                        return NotifyType.InvalidPassword;
                    }
                case "Name":
                    {
                        return NotifyType.InvalidName;
                    }
                default:
                    {
                        return NotifyType.InvalidName;
                    }
            }
        }
    }
}
