using CourseProjectKeyboardApplication.AppPages.Pages;
using CourseProjectKeyboardApplication.Tools;
using CourseProjectKeyboardApplication.View.Pages;
using CourseProjectKeyboardApplication.View.Windows;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProjectKeyboardApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LearnPage _learnPage;
        private TypingTestPage _typingTestPage;
        private TypingTutorPage _typingTutorPage;
        private TypingCertificatesPage _typingCertificatesPage;
        private TypingCertificationResultsPage _typingCertificationResultsPage;
        private EducationResultsPage _educationResultsPage;
        private TypingTestResultPage _typingTestResultPage;
        private EditUserProfilPage _editUserProfilPage;
        private TypingTutorResultPage _typingTutorResultPage;

        public MainWindow()
        {
            InitializeComponent();
            _learnPage = new LearnPage(MainFrame);
            _typingTutorPage = new TypingTutorPage();
            _typingTestPage = new TypingTestPage();
            _typingCertificatesPage = new TypingCertificatesPage(); // перенести весь блок в  Window_Loaded
            _typingCertificationResultsPage = new TypingCertificationResultsPage();
            _educationResultsPage = new EducationResultsPage();
            _typingTestResultPage = new TypingTestResultPage(10,120,10);
            _editUserProfilPage = new EditUserProfilPage();
            //_typingTutorResultPage = new TypingTutorResultPage(22, 0);

            FrameMediator.MainFrame = MainFrame;
            FrameMediator.InitPages(new List<Page>() { 
                _typingTutorPage,
                _learnPage,
                _typingTestPage,
                _typingCertificatesPage,
                _typingCertificationResultsPage,
                _educationResultsPage,
                _editUserProfilPage,

            });

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();

        }

        private void TypingTutorMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _typingTutorPage;
        }

        private void TypingTestMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _typingTestPage;
        }

        private void LearnMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _learnPage;

        }

        private void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _typingTestPage;
        }

        private void CertificatesButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Content = _typingCertificatesPage;
        }

        private void CertificationResultButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _typingCertificationResultsPage;
        }

        private void EducationalResultsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _educationResultsPage;
        }

        private void TempTestResultButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _typingTestResultPage;
        }



        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _editUserProfilPage;
        }


  
    }
}