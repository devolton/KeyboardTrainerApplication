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
    /// Логика взаимодействия для UserLoginPage.xaml
    /// </summary>
    public partial class UserLoginPage : Page
    {
        public UserLoginPage()
        {
            InitializeComponent();
        }

        private void UserLoginPagePasswordVisibiletyButton_Click(object sender, RoutedEventArgs e)
        {
            UserLoginPagePasswordVisibilityIconTextBlock.TextDecorations = (UserLoginPagePasswordVisibilityIconTextBlock.TextDecorations is null) ?
            TextDecorations.Strikethrough : null;
         
            


        }

    }
}
