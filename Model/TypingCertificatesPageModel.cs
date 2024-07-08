using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Tools;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingCertificatesPageModel
    {
        private TypingTestResultModel _typingTestResultModel;
        private TypingTestResult _bestUserTest;

        public TypingCertificatesPageModel()
        {
            _typingTestResultModel = DatabaseModelMediator.TypingTestResultModel;
        }
        public  void DisplayTestPage()
        {
            FrameMediator.DisplayTypingTestPage();
        }
        public RenderTargetBitmap GetUserCertificate()
        {
   
            return CertificateGenerator.RenderCertificate(UserController.CurrentUser.Name, _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"),_bestUserTest.Date);
        }
        public string GetTypingSpeed() => _bestUserTest.Speed.ToString();
        public string GetTypingAccuracy() => _bestUserTest.AccuracyPercent.ToString("0.0");
        public void InitBestUserTestResult()
        {
            _bestUserTest = _typingTestResultModel.GetBestUserTestResult(UserController.CurrentUser.Id);
        }

    }
}
