using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingTestResultPageModel
    {
        private const double FULL_PERCENT = 100;
        private int _allSymbolsCount = 0;
        private int _misclickCount = 0;
        private int _typingSpeed = 0;
        private double _accuracyPercent = 0;
        private ImageSource _keyboardIconImageSource;
     

        public TypingTestResultPageModel()
        {


        }
        //вычисление процента точности печати и перевод его в валидную строку
        public string GetAccuracyPercentStr()
        {
            return _accuracyPercent.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

        }
      
        public string GetSpeedStr()
        {
            return _typingSpeed.ToString();
        }
        public ImageSource GetKeyboardIconImageSource()
        {
            _keyboardIconImageSource ??= AppImageSourceProvider.KeyboardIconImageSource;
            return _keyboardIconImageSource;
        }
        public  void InitStat()
        {
            _allSymbolsCount = TypingTestResultController.AllSymbolCount;
            _misclickCount = TypingTestResultController.MiscliskCount;
            _typingSpeed = TypingTestResultController.TypingSpeed;
            _accuracyPercent = FULL_PERCENT - ((double)_allSymbolsCount / (double)_misclickCount);
             AddNewResult();
        }
        private  void AddNewResult()
        {
            var newTest = new TypingTestResult
            {
                Date = DateTime.Now,
                User = UserController.CurrentUser,
                UserId = UserController.CurrentUser.Id,
                AccuracyPercent = Math.Round(_accuracyPercent,2),
                LayoutType = Shared.Enums.LayoutType.English,
                Speed = _typingSpeed
            };
            TypingTestResultService.AddNewTypingTestLocal(newTest);
     
        }
    }
}
