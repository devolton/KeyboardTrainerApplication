using CourseProjectKeyboardApplication.View.CustomControls;
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
    /// Логика взаимодействия для TypingTestResultPage.xaml
    /// </summary>
    public partial class TypingTestResultPage : Page
    {
        private TypingTestResultPageViewModel _typingTestResultPageViewModel;
        public TypingTestResultPage(int resultSpeed, int allTextSymbolsCount, int misclickCount)
        {
            InitializeComponent();
            _typingTestResultPageViewModel = new TypingTestResultPageViewModel(resultSpeed,allTextSymbolsCount,misclickCount);
            DataContext = _typingTestResultPageViewModel;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _typingTestResultPageViewModel.DisplayResultCommand.Execute(new KeyValuePair<LanguageLayotStatisticBlock, LanguageLayotStatisticBlock>(SpeedBlock, AccuracyBlock));

        }
    }
}
