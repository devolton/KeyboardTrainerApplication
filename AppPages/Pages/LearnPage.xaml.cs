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

namespace CourseProjectKeyboardApplication.AppPages.Pages
{
    /// <summary>
    /// Логика взаимодействия для LearnPage.xaml
    /// </summary>
    public partial class LearnPage : Page
    {
        private LearnPageEnInfoModel _enLocalisation;
        private readonly Uri _typingTutorUri;
        private readonly Uri _typingTestUri;
        private Frame _mainFrame;
        public LearnPage(Frame mainFrame)
        {
            InitializeComponent();
            _enLocalisation = new LearnPageEnInfoModel();
            DataContext = _enLocalisation;
            _typingTutorUri = new Uri("/AppPages/Pages/TypingTutorPage.xaml", UriKind.Relative);
            _typingTestUri = new Uri("/AppPages/Pages/TypingTestPage.xaml", UriKind.Relative);
            _mainFrame = mainFrame;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
    

        }

        private void TestSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(_typingTestUri);


        }

        private void StartLearnButton_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(_typingTutorUri);

        }
    }
}
