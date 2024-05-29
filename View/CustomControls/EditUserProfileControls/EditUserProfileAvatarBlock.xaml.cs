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
using CourseProjectKeyboardApplication.ViewModel;
namespace CourseProjectKeyboardApplication.View.CustomControls.EditUserProfileControls
{
    /// <summary>
    /// Логика взаимодействия для EditUserProfileAvatarBlock.xaml
    /// </summary>
    public partial class EditUserProfileAvatarBlock : UserControl
    {
        private EditUserProfilePageViewModel _viewModel;
        public EditUserProfileAvatarBlock()
        {
            InitializeComponent();
            _viewModel = EditUserProfilePageViewModel.Instance();
            this.DataContext = _viewModel;
        }
    }
}
