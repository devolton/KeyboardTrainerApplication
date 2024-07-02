using CourseProjectKeyboardApplication.AppPages.Pages;
using CourseProjectKeyboardApplication.Shared.Mediators;
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
        private EditUserProfilPage _editUserProfilPage;
        private TypingTestResultPage _typingTestResultPage;
        private TypingTutorResultPage _typingTutorResultPage;

        public MainWindow()
        {
            InitializeComponent();

            //придумать асинхронную иницализацию для уменьшения времени загрузки приложения 
            _learnPage = new LearnPage();
            _typingTutorPage = new TypingTutorPage();
            _typingTestPage = new TypingTestPage();
            _typingCertificatesPage = new TypingCertificatesPage(); 
            _typingCertificationResultsPage = new TypingCertificationResultsPage();
            _educationResultsPage = new EducationResultsPage();
            _editUserProfilPage = new EditUserProfilPage();
            //_typingTestResultPage = new TypingTestResultPage();
            _typingTutorResultPage = new TypingTutorResultPage();

            FrameMediator.MainFrame = MainFrame;
            FrameMediator.InitPages(new List<Page>() {
                _typingTutorPage,
                _learnPage,
                _typingTestPage,
                _typingCertificatesPage,
                _typingCertificationResultsPage,
                _educationResultsPage,
                _editUserProfilPage,
                _typingTutorResultPage,
                _typingTestResultPage

            });


        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();

        }

        private void TypingTutorMainButton_Click(object sender, RoutedEventArgs e)
        {
            FrameMediator.DisplayTypingTutorPage();
        }

        private void TypingTestMainButton_Click(object sender, RoutedEventArgs e)
        {
            FrameMediator.DisplayTypingTestPage();
        }

        private void LearnMainButton_Click(object sender, RoutedEventArgs e)
        {

            FrameMediator.DisplayLearnPage();

        }

        private void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            
            FrameMediator.DisplayTypingTestPage();
        }

        private void CertificatesButton_Click(object sender, RoutedEventArgs e)
        {
            FrameMediator.DisplayTypingCertificatesPage();
          
        }

        private void CertificationResultButton_Click(object sender, RoutedEventArgs e)
        {
            FrameMediator.DisplayTypingCertificatesResultPage();
        }

        private void EducationalResultsButton_Click(object sender, RoutedEventArgs e)
        {
            FrameMediator.DisplayEducationResultsPage();
        }


        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            FrameMediator.DisplayEditUserProfilPage();
        }


  
    }
}