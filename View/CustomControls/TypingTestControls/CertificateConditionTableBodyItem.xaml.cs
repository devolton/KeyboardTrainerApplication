using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Providers;
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
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value);}
        }
        public MedalIconType MedalType
        {
            get => (MedalIconType)GetValue(MedalTypeProperty);
            set
            {
                SetValue(MedalTypeProperty, value);
            }

        }
        public static readonly DependencyProperty MedalTypeProperty =
            DependencyProperty.Register(nameof(MedalType),typeof(MedalIconType),typeof(CertificateConditionTableBodyItem),new PropertyMetadata(MedalIconType.Silver));
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(CertificateConditionTableBodyItem), new PropertyMetadata(null));
        public CertificateConditionTableBodyItem()
        {
            
            InitializeComponent();
            DataContext = this;

            
          
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            switch (MedalType)
            {
                case MedalIconType.Silver:
                    {
                        ImageSource = AppImageSourceProvider.SilverMedalImageSource;
                        break;
                    }
                case MedalIconType.Gold:
                    {
                        ImageSource = AppImageSourceProvider.GoldMedalImageSource;
                        break;
                    }
                case MedalIconType.Platinum:
                    {
                        ImageSource = AppImageSourceProvider.PlatinumMedalImageSource;
                        break;
                    }
            }




        }
    }
}
