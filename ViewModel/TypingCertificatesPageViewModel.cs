using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.View.CustomControls;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingCertificatesPageViewModel : ViewModelBase
    {
        private static TypingCertificatesPageViewModel _instance;
        private ITypingCertificatesPageModel _model;
        private ICommand _improveResultCommand;
        private ICommand _drawInfoCommand;
        private ICommand _saveCertificateCommand;
        private RenderTargetBitmap? _testCertificateTargetBitmap;
        private RenderTargetBitmap? _courseCompletionCertificateTargetBitmap;
        private string _typingSpeed = string.Empty;
        private string _typingAccuracy = string.Empty;
        private Visibility _testCertificateVisibility = Visibility.Collapsed;
        private Visibility _courseCompletionCertificateVisibility = Visibility.Collapsed;


        private ImageSource _cerficateIconImageSource;
        private TypingCertificatesPageViewModel()
        {
            _model = new TypingCertificatesPageModel();
            _improveResultCommand = new RelayCommand(OnImproveResultCommand);
            _drawInfoCommand = new RelayCommand(OnDrawInfoCommand);
            _saveCertificateCommand = new RelayCommand(OnSaveCertificateCommand, CanExecuteSaveCertificateCommand);

        }
        public static TypingCertificatesPageViewModel Instance()
        {
            _instance ??= new TypingCertificatesPageViewModel();
            return _instance;
        }

        //properties
        #region
        public ICommand ImproveResultCommand => _improveResultCommand;
        public ICommand DrawInfoCommand => _drawInfoCommand;
        public ICommand SaveCertificateCommand => _saveCertificateCommand;
        public string TypingSpeed
        {
            get { return _typingSpeed; }
            set
            {
                _typingSpeed = value;
                OnPropertyChanged(nameof(TypingSpeed));

            }
        }
        public string TypingAccuracy
        {
            get { return _typingAccuracy; }
            set
            {
                _typingAccuracy = value;
                OnPropertyChanged(nameof(TypingAccuracy));

            }
        }
        public RenderTargetBitmap? TestCertificateTargetBitmap
        {
            get { return _testCertificateTargetBitmap; }
            private set
            {
                _testCertificateTargetBitmap = value;
                OnPropertyChanged(nameof(TestCertificateTargetBitmap));
            }
        }
        public ImageSource CertificateIconImageSource
        {
            get => _cerficateIconImageSource;
            set
            {
                _cerficateIconImageSource = value;
                OnPropertyChanged(nameof(CertificateIconImageSource));
            }
        }

        public Visibility SpeedCertificateVisibility
        {
            get => _testCertificateVisibility;
            set
            {
                _testCertificateVisibility = value;
                OnPropertyChanged(nameof(SpeedCertificateVisibility));
            }
        }
        public Visibility CourseCompletionCertificateVisibility
        {
            get => _courseCompletionCertificateVisibility;
            set
            {
                _courseCompletionCertificateVisibility = value;
                OnPropertyChanged(nameof(CourseCompletionCertificateVisibility));
            }
        }
        public RenderTargetBitmap? CourseCompletionCertificateTargetBitmap
        {
            get => _courseCompletionCertificateTargetBitmap;
            set
            {
                _courseCompletionCertificateTargetBitmap = value;
                OnPropertyChanged(nameof(CourseCompletionCertificateTargetBitmap));
            }
        }



        #endregion

        //command
        #region
        private void OnImproveResultCommand(object param)
        {
            _model.DisplayTestPage();
        }
        private async void OnDrawInfoCommand(object param)
        {
            CertificateIconImageSource ??= _model.GetCertificateIconImageSource();
            _model.InitBestUserTestResult();
            try
            {
                var elementPair = (KeyValuePair<LanguageLayotStatisticBlock, LanguageLayotStatisticBlock>)param;
                _typingAccuracy = _model.GetTypingAccuracy();
                _typingSpeed = _model.GetTypingSpeed();
                TestCertificateTargetBitmap =await _model.GetUserTestCertificate();
                CourseCompletionCertificateTargetBitmap = await _model.GetCourseCompletionUserCertificate();
                SpeedCertificateVisibility = (_testCertificateTargetBitmap is not null) ? Visibility.Visible : Visibility.Collapsed;
                CourseCompletionCertificateVisibility = (_courseCompletionCertificateTargetBitmap is not null) ? Visibility.Visible : Visibility.Collapsed;
                var speedBlock = elementPair.Key;
                var accuracyBlock = elementPair.Value;
                speedBlock.StatValue = TypingSpeed;
                accuracyBlock.StatValue = TypingAccuracy;
                

            }
            catch (Exception ex)
            {
                return;
            }
        }
        private void OnSaveCertificateCommand(object param)
        {
            CertificateType certificateType = (CertificateType)param;
            _model.SaveImage(certificateType);

        }
        #endregion
        //command predicate
        #region
        private bool CanExecuteSaveCertificateCommand(object param)
        {
            return true;
        }
        #endregion
    }
}
