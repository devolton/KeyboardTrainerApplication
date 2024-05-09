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
       

        public TypingTestResultPageModel()
        {
           
            
        }
        //вычисление процента точности печати и перевод его в валидную строку
        public string GetAccuracyPercentStr(int misclickCount,int allSymbolsCount)
        {
            double accuracyPercent = FULL_PERCENT - ((double)allSymbolsCount / (double)misclickCount);
            return accuracyPercent.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

        }
        // реализовать асинхронно записи результата локально
    }
}
