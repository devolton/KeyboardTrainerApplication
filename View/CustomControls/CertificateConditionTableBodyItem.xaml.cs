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
    /// <summary>
    /// Логика взаимодействия для CertificateConditionTableBodyItem.xaml
    /// </summary>
    public partial class CertificateConditionTableBodyItem : UserControl
    {
        public  string SpeedLevelText {
            get { return  (string)GetValue(SpeedLevelTextProperty);}
            set { SetValue(SpeedLevelTextProperty, value);}
        }
        public static readonly DependencyProperty SpeedLevelTextProperty =
            DependencyProperty.Register("SpeedLevelText", typeof(string), typeof(CertificateConditionTableBodyItem), new PropertyMetadata(string.Empty));
        public string SpeedValueText {
            get { return(string) GetValue(SpeedValueTextProperty);}
            set { SetValue(SpeedValueTextProperty,value);}
        }
        public static readonly DependencyProperty SpeedValueTextProperty =
            DependencyProperty.Register("SpeedValueText", typeof(string), typeof(CertificateConditionTableBodyItem), new PropertyMetadata(string.Empty));
        public string AccuracyText
        {
            get { return(string) GetValue(AccuracyTextProperty);}
            set { SetValue(AccuracyTextProperty, value);}
        }
        public static readonly DependencyProperty AccuracyTextProperty =
            DependencyProperty.Register("AccuracyText",typeof(string),typeof(CertificateConditionTableBodyItem),new PropertyMetadata(string.Empty));
        public string ImageUri { get; set; }
        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri", typeof(string), typeof(CertificateConditionTableBodyItem), new PropertyMetadata(string.Empty));//исправить на дефолтную картинку
        public CertificateConditionTableBodyItem()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
