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
                NotifyType notifyType=(NotifyType) img.Tag;
                _viewModel.NotificationCommand.Execute(notifyType);
            }
            
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                _viewModel.SingUpClickCommand.Execute(null);
            }
        }
    }
}
