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
        private Uri _learnPageUri;
        private Uri _typingTutorUri;
        private Uri _typingTestUri;
        public MainWindow()
        {
            InitializeComponent();
            _typingTutorUri = new Uri("/AppPages/Pages/TypingTutorPage.xaml", UriKind.Relative);
            _typingTestUri = new Uri("/AppPages/Pages/TypingTestPage.xaml", UriKind.Relative);
            _learnPageUri = new Uri("/AppPages/Pages/LearnPage.xaml", UriKind.Relative);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();

        }

        private void TypingTutorMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_typingTutorUri);
        }

        private void TypingTestMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_typingTestUri);
        }

        private void LearnMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_learnPageUri);

        }
    }
}