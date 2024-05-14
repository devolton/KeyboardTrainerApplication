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
        private static string _foregrouncColorHex;
        private static BrushConverter _brushConverter;
        private static SolidColorBrush _foregroundBrush;
        private static DrawingVisual _drawingVisual;
        private static Typeface _typeface;
        private static BitmapImage _certificateBitmapImage;
        static CertificateGenerator()
        {
            _nameFontSize = 54;
            _typingSpeedFontSize = 30;
            _typingAccuracyFontSize = 30;
            _typingDateFontSize = 34;
            _foregrouncColorHex = "#0e477d";
            _typeface = new Typeface(new FontFamily("Noto serif"), FontStyles.Normal, FontWeights.SemiBold, FontStretches.Normal);
            _brushConverter = new BrushConverter();
            _drawingVisual = new DrawingVisual();
            _foregroundBrush = (SolidColorBrush)_brushConverter.ConvertFromString(_foregrouncColorHex);
        

        }
        public static RenderTargetBitmap RenderCertificate(string userName, string typingSpeed, string typingAccuracy, DateTime typingDate)
        {
            _certificateBitmapImage = (BitmapImage)Application.Current.Resources["CertificateTemplate"];

            using (DrawingContext drawingContext = _drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(_certificateBitmapImage, new Rect(0, 0, _certificateBitmapImage.Width, _certificateBitmapImage.Height));
                var nameFormattedText = GetFormatedText(userName, _typeface, _nameFontSize, _foregroundBrush);
                var typingSpeedFormattedText=GetFormatedText(typingSpeed,_typeface,_typingSpeedFontSize,_foregroundBrush);
                var typingAccuracyFormattedText= GetFormatedText(typingAccuracy,_typeface,_typingAccuracyFontSize,_foregroundBrush);
                var typingDateFormattedText = GetFormatedText(typingDate.ToString("dd.MM.yyyy"), _typeface, _typingDateFontSize, _foregroundBrush);
                double nameXCoordinate= (_certificateBitmapImage.Width - nameFormattedText.Width) / 2;
                drawingContext.DrawText(nameFormattedText, new Point(nameXCoordinate, 360)); 
                drawingContext.DrawText(typingSpeedFormattedText, new Point(692, 433));
                drawingContext.DrawText(typingAccuracyFormattedText, new Point(615, 468));
                drawingContext.DrawText(typingDateFormattedText, new Point(215, 565));
            }
            RenderTargetBitmap certificateRenderTargetBitmap = new RenderTargetBitmap((int)_certificateBitmapImage.Width, (int) _certificateBitmapImage.Height, 96, 96, PixelFormats.Default);
            certificateRenderTargetBitmap.Render(_drawingVisual);
            return certificateRenderTargetBitmap;
        }

        private static FormattedText GetFormatedText(string text, Typeface typeface, int fontSize, SolidColorBrush foreground)
        {
            return new FormattedText(text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, fontSize, foreground);
        }
    }
}
