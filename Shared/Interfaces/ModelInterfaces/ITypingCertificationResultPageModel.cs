using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Enums;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces
{
    public interface ITypingCertificationResultPageModel
    {
        ImageSource GetSilverCalendarImageSource();
        ImageSource GetSilverFlashImageSource();
        ImageSource GetSilverTargetImageSource();
        ImageSource GetStatIconImageSource();
        public List<TypingTestResult> GetSortTypingResultList(TypingStatisticsPeriodTime periodTime, bool isForTable);
        Task InitTypingTests();
        public Tuple<SeriesCollection, List<string>> GetStatistics(TypingStatisticsPeriodTime periodTime);
    }
}
