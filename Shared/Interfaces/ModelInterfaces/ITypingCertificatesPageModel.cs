using CourseProjectKeyboardApplication.Shared.Enums;
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
        Task<RenderTargetBitmap?> GetUserTestCertificate();
        Task<RenderTargetBitmap?> GetCourseCompletionUserCertificate();
        void SaveImage(CertificateType certificateType);
        string GetTypingSpeed();
        string GetTypingAccuracy();
        void InitBestUserTestResult();
    }
}
