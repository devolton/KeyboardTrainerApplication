using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.View.CustomControls;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingCertificatesPageViewModel : ViewModelBase
    {
        private static TypingCertificatesPageViewModel _instance;
        private TypingCertificatesPageModel _typingCertifcatesPageModel;
        private ICommand _improveResultCommand;
        private ICommand _drawInfoCommand;
        private RenderTargetBitmap _certificateRenderTargetBitmap;
        private string _typingSpeed;
        private string _typingAccuracy;

        private TypingCertificatesPageViewModel()
        {
            _typingCertifcatesPageModel = new TypingCertificatesPageModel();
            _improveResultCommand = new RelayCommand(OnImproveResultCommand);
            _drawInfoCommand = new RelayCommand(OnDrawInfoCommand);
            _typingAccuracy = "45";
            _typingSpeed = "90";

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



        #endregion

        //command
        #region
        public void OnImproveResultCommand(object param)
        {
            _typingCertifcatesPageModel.DisplayTestPage();
        }
        public void OnDrawInfoCommand(object param)
        {
            try
            {
                var elementPair = (KeyValuePair<LanguageLayotStatisticBlock, LanguageLayotStatisticBlock>)param;
                _typingAccuracy = _typingCertifcatesPageModel.GetTypingAccuracy();
                _typingSpeed = _typingCertifcatesPageModel.GetTypingSpeed();
                var speedBlock = elementPair.Key;
                var accuracyBlock = elementPair.Value;
                speedBlock.StatValue = TypingSpeed;
                accuracyBlock.StatValue = TypingAccuracy;
                _certificateRenderTargetBitmap = _typingCertifcatesPageModel.GetUserCertificate();
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion
    }
}
