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
        private const int _PLATINUM_CERTIFICATE_SPEED_CONDITION = 60;
        private const int _GOLD_CERTIFICATE_SPEED_CONDITION = 55;
        private const int _SILVER_CERTIFICATE_SPEED_CONDITION = 40;
        private const double _PLATINUM_CERTIFICATE_ACCURACY_CONDITION = 96;
        private const double _GOLD_CERTIFICATE_ACCURACY_CONDITION = 92.5;
        private const double _SILVER_CERTIFICATE_ACCURACY_CONDITION = 88;
        public TypingCertificatesPageModel()
        {
            _saveFileDialog = new SaveFileDialog()
            {
                Filter = "PNG Files|*.png|All Files|*.*",
                DefaultExt = "png"

            };
        }
        /// <summary>
        /// Delegete show typing test page to FrameMediator 
        /// </summary>
        public void DisplayTestPage()
        {
            FrameMediator.DisplayTypingTestPage();
        }

        public ImageSource GetCertificateIconImageSource()
        {
            _certificateIconImageSource ??= AppImageSourceProvider.CertificateIconImageSource;
            return _certificateIconImageSource;
        }

        /// <summary>
        /// Get created certificate for user typing test result 
        /// </summary>
        /// <returns>RenderTargetBitmap certificate</returns>
        public async Task<RenderTargetBitmap?> GetUserTestCertificate()
        {
            if (_bestUserTest != null)
            {
                if (IsPlatinumCertificateConditionMet())
                {
                    _testCertificate = await CertificateGenerator.RenderCertificate(TestCertificateType.Platinum, KeyboardAppEducationProgressController.CurrentUser.Name, _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"), _bestUserTest.Date);
                }
                else if (IsGoldCertificateConditionMet())
                {
                    _testCertificate = await CertificateGenerator.RenderCertificate(TestCertificateType.Gold, KeyboardAppEducationProgressController.CurrentUser.Name, _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"), _bestUserTest.Date);
                }
                else if (IsSilverCertificateConditionMet())
                {
                    _testCertificate = await CertificateGenerator.RenderCertificate(TestCertificateType.Silver, KeyboardAppEducationProgressController.CurrentUser.Name, _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"), _bestUserTest.Date);
                }
                else
                {
                    _testCertificate = await CertificateGenerator.RenderCertificate(TestCertificateType.Blue, KeyboardAppEducationProgressController.CurrentUser.Name, _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"), _bestUserTest.Date);
                }

            }
            return _testCertificate;
        }

        /// <summary>
        /// Get created certificate for course completion  
        /// </summary>
        /// <returns>RenderTargetBitmap certificate</returns>
        public async Task<RenderTargetBitmap?> GetCourseCompletionUserCertificate()
        {
            if (IsCourceCompletionPerfectlyConditoinMet())
            {
                _courseCompletionCertificate = await CertificateGenerator.RenderCertificate(CourseComplitionCertificateType.Distinction, KeyboardAppEducationProgressController.CurrentUser.Name);
            }
            else if (IsCourseCompletionConditionMet())
            {
                _courseCompletionCertificate = await CertificateGenerator.RenderCertificate(CourseComplitionCertificateType.WitnoutDistinction, KeyboardAppEducationProgressController.CurrentUser.Name);
            }
            return _courseCompletionCertificate;
        }
        /// <summary>
        /// Save certificate to local storage
        /// </summary>
        /// <param name="certificateType">Type of certificat</param>
        public void SaveImage(CertificateType certificateType)
        {
            var certificateToSave = (certificateType == CertificateType.TestCertificate) ? _testCertificate : _courseCompletionCertificate;

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveRenderTargetBitmapToPng(certificateToSave, _saveFileDialog.FileName);
            }
        }
        /// <summary>
        /// Get user best typing speed
        /// </summary>
        /// <returns>Best typing speed string</returns>
        public string GetTypingSpeed()
        {
            return (_bestUserTest is null) ? "0" : _bestUserTest.Speed.ToString();
        }

        /// <summary>
        /// Get best user typing accuracy
        /// </summary>
        /// <returns>Best accuracy string</returns>
        public string GetTypingAccuracy()
        {
            return (_bestUserTest is null) ? "0" : _bestUserTest.AccuracyPercent.ToString("0.0");
        }
        /// <summary>
        /// Initializing best user test 
        /// </summary>
        public void InitBestUserTestResult()
        {

            _bestUserTest = TypingTestResultService.GetBestUserTestAsync(KeyboardAppEducationProgressController.CurrentUser.Id);

        }
        /// <summary>
        /// Method which conver renderTargetBitmap to png and saving to path
        /// </summary>
        /// <param name="renderTargetBitmap">RenderTargetBitmap image which we want save</param>
        /// <param name="filename">Image name which we want</param>
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
