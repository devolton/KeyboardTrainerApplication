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

namespace CourseProjectKeyboardApplication.View.CustomControls
{
    public partial class CertificateResultsFilterBlock : UserControl
    {
        private TypingCertificationResultPageViewModel _viewModel;
        public CertificateResultsFilterBlock()
        {
            _viewModel = TypingCertificationResultPageViewModel.Instance();
            InitializeComponent();
           
            DataContext = _viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.DrawStatisticsCommand.Execute(null);

        }
    }
}
