using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CourseProjectKeyboardApplication.Model;
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
        private RenderTargetBitmap? _certificateRenderTargetBitmap;
        private string _typingSpeed = string.Empty;
        private string _typingAccuracy = string.Empty;
        private bool _isSaveCertificateButtonEnabled = false;


        private ImageSource _cerficateIconImageSource;
        private TypingCertificatesPageViewModel()
        {
            _model = new TypingCertificatesPageModel();
            _improveResultCommand = new RelayCommand(OnImproveResultCommand);
            _drawInfoCommand = new RelayCommand(OnDrawInfoCommand);
            _saveCertificateCommand = new RelayCommand(OnSaveCertificateCommand,CanExecuteSaveCertificateCommand);

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
        public RenderTargetBitmap CertificateSource
        {
            get { return _certificateRenderTargetBitmap; }
            private set
            {
                _certificateRenderTargetBitmap = value;
                OnPropertyChanged(nameof(CertificateSource));
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
        public bool IsSaveCertificateButtonEnabled
        {
            get => _isSaveCertificateButtonEnabled;
            set
            {
                _isSaveCertificateButtonEnabled = value;
                OnPropertyChanged(nameof(IsSaveCertificateButtonEnabled));
            }
        }



        #endregion

        //command
        #region
        private void OnImproveResultCommand(object param)
        {
            _model.DisplayTestPage();
        }
        private void OnDrawInfoCommand(object param)
        {
            CertificateIconImageSource ??= _model.GetCertificateIconImageSource();
              _model.InitBestUserTestResult();
            try
            {
                var elementPair = (KeyValuePair<LanguageLayotStatisticBlock, LanguageLayotStatisticBlock>)param;
                _typingAccuracy = _model.GetTypingAccuracy();
                _typingSpeed=_model.GetTypingSpeed();
                _certificateRenderTargetBitmap= _model.GetUserCertificate();
                IsSaveCertificateButtonEnabled = _certificateRenderTargetBitmap != null;
                var speedBlock = elementPair.Key;
                var accuracyBlock = elementPair.Value;
                speedBlock.StatValue = TypingSpeed;
                accuracyBlock.StatValue = TypingAccuracy;
                CertificateSource = _certificateRenderTargetBitmap;
            }
            catch (Exception ex)
            {
                return;
            }
        }
        private void OnSaveCertificateCommand(object param)
        {
            _model.SaveImage();
         
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
