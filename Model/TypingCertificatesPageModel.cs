using System;
using System.Collections.Generic;
using System.IO;
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
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;
using CourseProjectKeyboardApplication.Tools;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingCertificatesPageModel
    {
        private TypingTestResult? _bestUserTest;
        private RenderTargetBitmap _certificate;

        public TypingCertificatesPageModel()
        {
        }
        public void DisplayTestPage()
        {
            FrameMediator.DisplayTypingTestPage();
        }
        public RenderTargetBitmap? GetUserCertificate()
        {
            if (_bestUserTest != null)
                _certificate= CertificateGenerator.RenderCertificate(UserController.CurrentUser.Name, _bestUserTest.Speed.ToString(), _bestUserTest.AccuracyPercent.ToString("0.0"), _bestUserTest.Date);
            return _certificate;
        }
        public void SaveImage()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Files|*.png|All Files|*.*",
                DefaultExt = "png"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveRenderTargetBitmapToPng(_certificate, saveFileDialog.FileName);
            }
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
        public string GetTypingSpeed()
        {
            return (_bestUserTest is null) ? "0" : _bestUserTest.Speed.ToString();
        }
        public string GetTypingAccuracy()
        {
            return (_bestUserTest is null) ? "0" : _bestUserTest.AccuracyPercent.ToString("0.0");
        }
        public async Task InitBestUserTestResult()
        {
            
            _bestUserTest = await TypingTestResultService.GetBestUserTestAsync(UserController.CurrentUser.Id);

        }

    }
}
