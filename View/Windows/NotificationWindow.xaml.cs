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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseProjectKeyboardApplication.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public string Notification
        {
            get => (string)GetValue(NotificationPropery);
            set
            {
                SetValue(NotificationPropery, value);
            }

        }
        public static readonly DependencyProperty NotificationPropery =
            DependencyProperty.Register(nameof(Notification), typeof(string), typeof(NotificationWindow), new PropertyMetadata("Error!"));
        public NotificationWindow()
        {
           
            InitializeComponent();
            DataContext = this;
        }

    }
}
