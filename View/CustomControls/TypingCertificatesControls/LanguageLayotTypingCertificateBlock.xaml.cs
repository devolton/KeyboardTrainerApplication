using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProjectKeyboardApplication.Tools;
using System.Windows;
using System.Windows.Controls;
using CourseProjectKeyboardApplication.ViewModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProjectKeyboardApplication.View.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для LanguageLayotTypingCertificateBlock.xaml
    /// </summary>
    public partial class LanguageLayotTypingCertificateBlock : UserControl
    {
        private TypingCertificatesPageViewModel _typingCertificatesPageViewModel;
        public LanguageLayotTypingCertificateBlock()
        {
            InitializeComponent();
            _typingCertificatesPageViewModel = TypingCertificatesPageViewModel.Instance();
            DataContext = _typingCertificatesPageViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _typingCertificatesPageViewModel.DrawInfoCommand.Execute(new KeyValuePair<LanguageLayotStatisticBlock,LanguageLayotStatisticBlock>(SpeedBlock,AccuracyBlock));
        }
    }
}
