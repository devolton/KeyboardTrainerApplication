using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces
{
    public interface ITypingCertificatesPageModel
    {
        void DisplayTestPage();
        ImageSource GetCertificateIconImageSource();
        RenderTargetBitmap? GetUserCertificate();
        void SaveImage();
        string GetTypingSpeed();
        string GetTypingAccuracy();
        void InitBestUserTestResult();
    }
}
