using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Mediators;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingTestResultPageModel
    {
        private const double FULL_PERCENT = 100;
        private int _allSymbolsCount = 0;
        private int _misclickCount = 0;
        private int _typingSpeed = 0;
        private double _accuracyPercent = 0;

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
        public void InitStat()
        {
            _allSymbolsCount = TypingTestResultController.AllSymbolCount;
            _misclickCount = TypingTestResultController.MiscliskCount;
            _typingSpeed = TypingTestResultController.TypingSpeed;
            _accuracyPercent = FULL_PERCENT - ((double)_allSymbolsCount / (double)_misclickCount);
            SaveInDb();
        }
        private void SaveInDb()
        {
            var newTest = new TypingTestResult
            {
                Date = DateTime.Now,
                User = UserController.CurrentUser,
                UserId = UserController.CurrentUser.Id,
                AccuracyPercent = _accuracyPercent,
                LayoutType = Shared.Enums.LayoutType.English,
                Speed = _typingSpeed
            };
            DatabaseModelMediator.TypingTestResultModel.AddNewTypingTestResult(newTest);
            DatabaseModelMediator.TypingTestResultModel.SaveChanges();
        }
    }
}
