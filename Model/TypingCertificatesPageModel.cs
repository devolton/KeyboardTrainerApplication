using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;
using CourseProjectKeyboardApplication.Tools;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingCertificatesPageModel : ITypingCertificatesPageModel
    {
        private TypingTestResult? _bestUserTest;
        private RenderTargetBitmap _testCertificate;
        private RenderTargetBitmap _courseCompletionCertificate;
        private ImageSource _certificateIconImageSource;
        private SaveFileDialog _saveFileDialog;
        private const int _PLATINUM_CERTIFICATE_SPEED_CONDITION = 70;
        private const int _GOLD_CERTIFICATE_SPEED_CONDITION = 50;
        private const int _SILVER_CERTIFICATE_SPEED_CONDITION = 40;
        private const double _PLATINUM_CERTIFICATE_ACCURACY_CONDITION = 99.5;
        private const double _GOLD_CERTIFICATE_ACCURACY_CONDITION = 98.7;
        private const double _SILVER_CERTIFICATE_ACCURACY_CONDITION = 96;
        public TypingCertificatesPageModel()
        {
            _saveFileDialog = new SaveFileDialog()
            {
                Filter = "PNG Files|*.png|All Files|*.*",
                DefaultExt = "png"

            };
        }
        public void DisplayTestPage()
        {
            FrameMediator.DisplayTypingTestPage();
        }
        public ImageSource GetCertificateIconImageSource()
        {
            _certificateIconImageSource ??= AppImageSourceProvider.CertificateIconImageSource;
            return _certificateIconImageSource;
        }
        public async Task<RenderTargetBitmap?> GetUserTestCertificate()
        {
            if (_bestUserTest != null)
            {
                if (IsPlatinumCertificateConditionMet())
                {
                    _testCertificate = await CertificateGenerator.RenderCertificate(TestCertificateType.Platinum, UserController.CurrentUser.Name, _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"), _bestUserTest.Date);
                }
                else if (IsGoldCertificateConditionMet())
                {
                    _testCertificate = await CertificateGenerator.RenderCertificate(TestCertificateType.Gold, UserController.CurrentUser.Name, _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"), _bestUserTest.Date);
                }
                else if (IsSilverCertificateConditionMet())
                {
                    _testCertificate = await CertificateGenerator.RenderCertificate(TestCertificateType.Silver, UserController.CurrentUser.Name, _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"), _bestUserTest.Date);
                }
                else
                {
                    _testCertificate = await CertificateGenerator.RenderCertificate(TestCertificateType.Blue, UserController.CurrentUser.Name, _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"), _bestUserTest.Date);
                }

            }
            return _testCertificate;
        }
        public async Task<RenderTargetBitmap?> GetCourseCompletionUserCertificate()
        {
            if (IsCourceCompletionPerfectlyConditoinMet())
            {
                _courseCompletionCertificate = await CertificateGenerator.RenderCertificate(CourseComplitionCertificateType.Distinction, UserController.CurrentUser.Name);
            }
            else if (IsCourseCompletionConditionMet())
            {
                _courseCompletionCertificate = await CertificateGenerator.RenderCertificate(CourseComplitionCertificateType.WitnoutDistinction, UserController.CurrentUser.Name);
            }
            return _courseCompletionCertificate;
        }

        public void SaveImage(CertificateType certificateType)
        {
            var certificateToSave = (certificateType == CertificateType.TestCertificate) ? _testCertificate : _courseCompletionCertificate;

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveRenderTargetBitmapToPng(certificateToSave, _saveFileDialog.FileName);
            }
        }
        public string GetTypingSpeed()
        {
            return (_bestUserTest is null) ? "0" : _bestUserTest.Speed.ToString();
        }
        public string GetTypingAccuracy()
        {
            return (_bestUserTest is null) ? "0" : _bestUserTest.AccuracyPercent.ToString("0.0");
        }
        public void InitBestUserTestResult()
        {

            _bestUserTest = TypingTestResultService.GetBestUserTestAsync(UserController.CurrentUser.Id);

        }
        private void SaveRenderTargetBitmapToPng(RenderTargetBitmap renderTargetBitmap, string filename)
        {
            PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using (var fs = File.OpenWrite(filename))
            {
                pngEncoder.Save(fs);
            }
        }
        private bool IsPlatinumCertificateConditionMet()
        {
            return _bestUserTest.Speed >= _PLATINUM_CERTIFICATE_SPEED_CONDITION && _bestUserTest.AccuracyPercent >= _PLATINUM_CERTIFICATE_ACCURACY_CONDITION;
        }
        private bool IsGoldCertificateConditionMet()
        {
            return _bestUserTest.Speed >= _GOLD_CERTIFICATE_SPEED_CONDITION && _bestUserTest.AccuracyPercent >= _GOLD_CERTIFICATE_ACCURACY_CONDITION;
        }
        private bool IsSilverCertificateConditionMet()
        {
            return _bestUserTest.Speed >= _SILVER_CERTIFICATE_SPEED_CONDITION && _bestUserTest.AccuracyPercent >= _SILVER_CERTIFICATE_ACCURACY_CONDITION;
        }
        private bool IsCourceCompletionPerfectlyConditoinMet()
        {
            return IsCourseCompletionConditionMet() && EducationUsersProgressService.IsAllPerfectlyCompleted();
        }
        private bool IsCourseCompletionConditionMet()
        {
            return EnglishLayoutLessonsService.GetLessonsCount() == EducationUsersProgressService.GetEducationUsersProgressesCount();
        }


    }
}
