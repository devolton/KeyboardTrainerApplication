using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingCertificationResultPageModel: ITypingCertificationResultPageModel
    {
        private SeriesCollection _typingUserStatisticSeriesCollection;
        private LineSeries _typingAccuracyLineSeries;
        private LineSeries _typingSpeedLineSeries;
        private List<TypingTestResult> _userTypingTestsCollection;

        private ImageSource _statIconImageSource;
        private ImageSource _silverTargetImageSource;
        private ImageSource _silverFlashImageSource;
        private ImageSource _silverCalendarImageSource;
        public TypingCertificationResultPageModel()
        {
            _typingUserStatisticSeriesCollection = new SeriesCollection();

            _typingSpeedLineSeries = new LineSeries()
            {
                Title = "Speed(wpm)",
                Stroke = System.Windows.Media.Brushes.Orange,
                Fill = System.Windows.Media.Brushes.Transparent
            };
            _typingAccuracyLineSeries = new LineSeries()
            {
                Title = "Accuracy(%)",
                Stroke = System.Windows.Media.Brushes.Violet,
                Fill = System.Windows.Media.Brushes.Transparent
            };
            _typingSpeedLineSeries.Values = new ChartValues<int>();
            _typingAccuracyLineSeries.Values = new ChartValues<double>();

         


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="periodTime">period of time for which to take data</param>
        /// <returns>Tuple of two collection:
        /// [FIRST] - SeriesCollection that stores data about speed and accuracy of user typing test | 
        /// [SECOND] - List containing the date for passing these tests</returns>
        public Tuple<SeriesCollection, List<string>> GetStatistics(TypingStatisticsPeriodTime periodTime)
        {
            List<TypingTestResult> selectedResults = GetSortTypingResultList(periodTime, false);

            var seriesCollection = InitSeriesCollection(selectedResults);
            var dateList = new List<string>();
            foreach (var item in selectedResults)
            {
                dateList.Add(item.Date.ToString("dd MMM yyyy", new CultureInfo("en-US")));
            }
            return Tuple.Create(seriesCollection, dateList);
        }




        
        public async Task InitTypingTests()
        {
            _userTypingTestsCollection = (await TypingTestResultService.GetTypingTestResultsByUserIdAsync(UserController.CurrentUser.Id))?.ToList() ?? new();
        }

        /// <summary>
        /// Get sorting 
        /// </summary>
        /// <param name="periodTime"></param>
        /// <param name="isForTable"></param>
        /// <returns>Get sort TypingTestResults list</returns>
        public List<TypingTestResult> GetSortTypingResultList(TypingStatisticsPeriodTime periodTime, bool isForTable)
        {

            List<TypingTestResult> selectedResults = null;
            int currentDay = DateTime.Now.Day;
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            switch (periodTime)
            {
                case TypingStatisticsPeriodTime.AllTime:
                    {
                        selectedResults = _userTypingTestsCollection;
                        break;
                    }
                case TypingStatisticsPeriodTime.Month:
                    {

                        selectedResults = _userTypingTestsCollection.FindAll(element => element.Date.Month.Equals(currentMonth) && element.Date.Year.Equals(currentYear));
                        break;
                    }
                case TypingStatisticsPeriodTime.Day:
                    {

                        selectedResults = _userTypingTestsCollection.FindAll(element => element.Date.Day.Equals(currentDay)
                        && element.Date.Month.Equals(currentMonth)
                        && element.Date.Year.Equals(currentYear));
                        break;
                    }
                default:
                    {
                        selectedResults = _userTypingTestsCollection;
                        break;
                    }

            }
            if (selectedResults is not null)
            {
                if (selectedResults.Count > 10)
                {
                    selectedResults.Sort((first, second) => second.Speed.CompareTo(first.Speed));
                    selectedResults = selectedResults.GetRange(0, 10);
                    if (isForTable)
                    {
                        return selectedResults;
                    }


                }
                if (isForTable)
                {
                    selectedResults?.Sort((first, second) => second.Speed.CompareTo(first.Speed));
                    return selectedResults;
                }
                selectedResults.Sort((first, second) => first.Date.CompareTo(second.Date));
            }
            return selectedResults;
        }
        public ImageSource GetStatIconImageSource()
        {
            _statIconImageSource ??= AppImageSourceProvider.StatisticIconImageSource;
            return _statIconImageSource;
        }
        public ImageSource GetSilverTargetImageSource()
        {
            _silverTargetImageSource ??= AppImageSourceProvider.SilverTargetImageSource;
            return _silverTargetImageSource;
        }
        public ImageSource GetSilverFlashImageSource()
        {
            _silverFlashImageSource ??= AppImageSourceProvider.SilverFlashImageSource;
            return _silverFlashImageSource;

        }
        public ImageSource GetSilverCalendarImageSource()
        {
            _silverCalendarImageSource ??= AppImageSourceProvider.SilverCalendarImageSource;
            return _silverCalendarImageSource;

        }


        /// <summary>
        /// Create ChartValues and fill it with typing accuracy data
        /// </summary>
        /// <param name="collection">Typing test results collection of current user</param>
        /// <returns>ChartValues collection of typing accuracy data</returns>
        private ChartValues<double> GetAccuracyChartValues(List<TypingTestResult> collection)
        {
            var chartValues = new ChartValues<double>();
            foreach (var stat in collection)
            {
                chartValues.Add(stat.AccuracyPercent);
            }
            return chartValues;
        }
        /// <summary>
        /// Create ChartValues and fill it with typing speed data
        /// </summary>
        /// <param name="collection">Typing test results collection of current user</param>
        /// <returns>ChartValues collection of typing speed data</returns>
        private ChartValues<int> GetSpeedChartValues(List<TypingTestResult> collection)
        {
            var chartValues = new ChartValues<int>();
            foreach (var stat in collection)
            {
                chartValues.Add(stat.Speed);
            }
            return chartValues;
        }
        /// <summary>
        ///  Initialize seriesCollection for 
        /// </summary>
        /// <param name="statList">User typing test results list</param>
        /// <returns> SeriesCollection that stores data about speed and accuracy of user typing test </returns>
        private SeriesCollection InitSeriesCollection(List<TypingTestResult> statList)
        {
            _typingUserStatisticSeriesCollection.Clear();
            _typingSpeedLineSeries.Values.Clear();
            _typingAccuracyLineSeries.Values.Clear();
            var speedChartValues = GetSpeedChartValues(statList);
            var accuracyChartValues = GetAccuracyChartValues(statList);
            _typingSpeedLineSeries.Values = speedChartValues;
            _typingAccuracyLineSeries.Values = accuracyChartValues;
            _typingUserStatisticSeriesCollection.Add(_typingSpeedLineSeries);
            _typingUserStatisticSeriesCollection.Add(_typingAccuracyLineSeries);
            return _typingUserStatisticSeriesCollection;

        }



    }

}
