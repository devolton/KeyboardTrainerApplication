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
            InitBestUserTestResult();
        }
        public  void DisplayTestPage()
        {
            FrameMediator.DisplayTypingTestPage();
        }
        public RenderTargetBitmap GetUserCertificate()
        {
            //change "Anton" to user name 
            return CertificateGenerator.RenderCertificate("Anton", _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"),_bestUserTest.Date);
        }
        public string GetTypingSpeed() => _bestUserTest.Speed.ToString();
        public string GetTypingAccuracy() => _bestUserTest.AccuracyPercent.ToString("0.0");
        private void InitBestUserTestResult()
        {
            _bestUserTest = _typingTestResultModel.GetBestUserTestResult(1);//CurrentUserId
        }

    }
}
