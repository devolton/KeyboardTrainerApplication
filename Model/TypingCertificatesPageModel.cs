using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CourseProjectKeyboardApplication.Tools;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingCertificatesPageModel
    {
        private int _typingSpeed;
        private double _typingAccuracy;

        public TypingCertificatesPageModel()
        {
            _typingAccuracy = 99.45;
            _typingSpeed = 45;
         
        }
        public  void DisplayTestPage()
        {
            FrameMediator.DisplayTypingTestPage();
        }
        public RenderTargetBitmap GetUserCertificate()
        {
            //запрос в бд 
            return CertificateGenerator.RenderCertificate("Anton", _typingSpeed.ToString(), _typingAccuracy.ToString("0.0"), new DateTime(2024, 10, 10));
        }
        public string GetTypingSpeed() => _typingSpeed.ToString();
        public string GetTypingAccuracy() => _typingAccuracy.ToString("0.0");
    }
}
