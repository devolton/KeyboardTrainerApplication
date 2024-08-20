using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CourseProjectKeyboardApplication.Tools
{
    public static class CertificateGenerator
    {
        private static readonly int _nameFontSize;
        private static readonly int _typingSpeedFontSize;
        private static readonly int _typingAccuracyFontSize;
        private static readonly int _typingDateFontSize;
        private static string _blueForegrouncColorHex;
        private static string _goldForegroundColorHex;
        private static string _blackForegroundColorHex;
        private static string _platinumForegroundColorHex;
        private static string _silverForegroundColorHex;
        private static string _darkBlueForegroundColorHex;
        private static BrushConverter _brushConverter;
        private static DrawingVisual _drawingVisual;
        private static Typeface _typeface;
        private static BitmapImage _testCertificateBitmapImage;
        private static BitmapImage _courseCompletedCertificateBitpamImage;
        
        static CertificateGenerator()
        {
            _nameFontSize = 56;
            _typingSpeedFontSize = 30;
            _typingAccuracyFontSize = 30;
            _typingDateFontSize = 34;
            _blueForegrouncColorHex = "#0e477d";
            _darkBlueForegroundColorHex = "#415a77";
            _goldForegroundColorHex = "#fdc238";
            _silverForegroundColorHex = "#c0c0c0";
            _platinumForegroundColorHex = "#e5e4e2";
            _blackForegroundColorHex = "#000000";



            _typeface = new Typeface(new FontFamily("Impact"), FontStyles.Normal, FontWeights.Thin, FontStretches.Normal);
            _brushConverter = new BrushConverter();
            _drawingVisual = new DrawingVisual();
        

        }
        public static async Task<RenderTargetBitmap> RenderCertificate(TestCertificateType testCertificateType,string userName, string typingSpeed, string typingAccuracy, DateTime typingDate)
        {
            _testCertificateBitmapImage ??= await GetCertificateTemplateBitmapImage(testCertificateType);
            var foreground = GetForegroundBrush(testCertificateType);
            using (DrawingContext drawingContext = _drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(_testCertificateBitmapImage, new Rect(0, 0, _testCertificateBitmapImage.Width, _testCertificateBitmapImage.Height));
                var nameFormattedText = GetFormatedText(userName, _typeface, _nameFontSize, foreground);
                var typingSpeedFormattedText=GetFormatedText(typingSpeed,_typeface,_typingSpeedFontSize, foreground);
                var typingAccuracyFormattedText= GetFormatedText(typingAccuracy,_typeface,_typingAccuracyFontSize, foreground);
                var typingDateFormattedText = GetFormatedText(typingDate.ToString("dd.MM.yyyy"), _typeface, _typingDateFontSize, foreground);
                double nameXCoordinate= (_testCertificateBitmapImage.Width - nameFormattedText.Width) / 2;
                drawingContext.DrawText(nameFormattedText, new Point(nameXCoordinate, 355)); 
                drawingContext.DrawText(typingSpeedFormattedText, new Point(692, 430));
                drawingContext.DrawText(typingAccuracyFormattedText, new Point(615, 460));
                drawingContext.DrawText(typingDateFormattedText, new Point(215, 565));
            }
            RenderTargetBitmap certificateRenderTargetBitmap = new RenderTargetBitmap((int)_testCertificateBitmapImage.Width, (int) _testCertificateBitmapImage.Height, 96, 96, PixelFormats.Default);
            certificateRenderTargetBitmap.Render(_drawingVisual);
            return certificateRenderTargetBitmap;
        }
        public static async Task<RenderTargetBitmap> RenderCertificate(CourseComplitionCertificateType courseCertificateType, string userName)
        {
            _courseCompletedCertificateBitpamImage ??= await GetCertificateTemplateBitmapImage(courseCertificateType);
            var foreground = GetForegroundBrush(courseCertificateType);
            using (DrawingContext drawingContext = _drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(_courseCompletedCertificateBitpamImage, new Rect(0, 0, _courseCompletedCertificateBitpamImage.Width, _courseCompletedCertificateBitpamImage.Height));
                var nameFormattedText = GetFormatedText(userName, _typeface, _nameFontSize, foreground);
                double nameXCoordinate = (_courseCompletedCertificateBitpamImage.Width - nameFormattedText.Width) / 2;
                double nameYCoordinate = 330;
                drawingContext.DrawText(nameFormattedText, new Point(nameXCoordinate, nameYCoordinate));
            }
            RenderTargetBitmap certificateRenderTargetBitmap = new RenderTargetBitmap((int)_courseCompletedCertificateBitpamImage.Width, (int)_courseCompletedCertificateBitpamImage.Height, 96, 96, PixelFormats.Default);
            certificateRenderTargetBitmap.Render(_drawingVisual);
            return certificateRenderTargetBitmap;
        }

        private static FormattedText GetFormatedText(string text, Typeface typeface, int fontSize, SolidColorBrush foreground)
        {
            return new FormattedText(text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, fontSize, foreground);
        }
        private static async Task<BitmapImage> GetCertificateTemplateBitmapImage(TestCertificateType testCertificateType)
        {
            switch(testCertificateType)
            {
                case TestCertificateType.Blue:
                    {
                        return (BitmapImage)(await ContentApiClientProvider.StaticImageApiClient.GetImageSourceAsync("EnglishTypingCertificate.png"));
                    }
                case TestCertificateType.Silver:
                    {
                        return (BitmapImage)(await ContentApiClientProvider.StaticImageApiClient.GetImageSourceAsync("EnglishTypingSilverCertificateTemplate.png"));
                    }
                case TestCertificateType.Gold:
                    {
                        return(BitmapImage)(await ContentApiClientProvider.StaticImageApiClient.GetImageSourceAsync("EngilshTypingGoldCertifitcate.png"));
                    }
                case TestCertificateType.Platinum:
                    {
                        return (BitmapImage)(await ContentApiClientProvider.StaticImageApiClient.GetImageSourceAsync("EnglishTypingPlatinumCertificate.png"));
                    }
                default:
                    {
                        return (BitmapImage)(await ContentApiClientProvider.StaticImageApiClient.GetImageSourceAsync("EnglishTypingCertificate.png"));
                    }
            }
        }
        private static async Task<BitmapImage> GetCertificateTemplateBitmapImage(CourseComplitionCertificateType testCertificateType)
        {
            switch (testCertificateType)
            {
                case CourseComplitionCertificateType.WitnoutDistinction:
                    {
                        return (BitmapImage)(await ContentApiClientProvider.StaticImageApiClient.GetImageSourceAsync("CourseCompletedCertificteTemplate.png"));
                    }
                case CourseComplitionCertificateType.Distinction:
                    {
                        return (BitmapImage)(await ContentApiClientProvider.StaticImageApiClient.GetImageSourceAsync("CourseCompletedWithDistinctionCertificateTemplate.png"));
                    }
               
                default:
                    {
                        return (BitmapImage)(await ContentApiClientProvider.StaticImageApiClient.GetImageSourceAsync("CourseCompletedCertificteTemplate.png"));
                    }
            }
        }
        private static SolidColorBrush GetForegroundBrush(TestCertificateType testCertificateType)
        {
            switch (testCertificateType)
            {
                case TestCertificateType.Blue:
                    {
                        return (SolidColorBrush)_brushConverter.ConvertFromString(_blueForegrouncColorHex);
                    }
                case TestCertificateType.Silver:
                    {
                        return (SolidColorBrush)_brushConverter.ConvertFromString(_silverForegroundColorHex);
                    }
                case TestCertificateType.Gold:
                    {
                        return (SolidColorBrush)_brushConverter.ConvertFromString(_goldForegroundColorHex);
                    }
                case TestCertificateType.Platinum:
                    {
                        return (SolidColorBrush)_brushConverter.ConvertFromString(_platinumForegroundColorHex);
                    }
                default:
                    {
                        return (SolidColorBrush)_brushConverter.ConvertFromString(_blackForegroundColorHex);
                    }
            }
           
        }
        private static SolidColorBrush GetForegroundBrush(CourseComplitionCertificateType courseCertificationType)
        {
            switch (courseCertificationType)
            {
                case CourseComplitionCertificateType.WitnoutDistinction:
                    {
                        return (SolidColorBrush)_brushConverter.ConvertFromString(_darkBlueForegroundColorHex);
                    }
                case CourseComplitionCertificateType.Distinction:
                    {
                        return (SolidColorBrush)_brushConverter.ConvertFromString(_blackForegroundColorHex);
                    }

                default:
                    {
                        return (SolidColorBrush)_brushConverter.ConvertFromString(_blackForegroundColorHex);
                    }
            }
        }
    }
}
