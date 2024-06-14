using CourseProjectKeyboardApplication.Shared.Enums;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.VisualBasic;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingCertificationResultPageModel
    {
        private List<ResultTemp> _typingUserStatisticList;
        private SeriesCollection _typingUserStatisticSeriesCollection;
        private LineSeries _typingAccuracyLineSeries;
        private LineSeries _typingSpeedLineSeries;
        private List<string> _typingDateList;
        public TypingCertificationResultPageModel()
        {
            _typingUserStatisticSeriesCollection = new SeriesCollection();
            _typingUserStatisticList = GetTypingUserStatistic();

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

        private List<ResultTemp> GetTypingUserStatistic()
        {

            //запрос с бд на получение всех данных (а еще лучше топ 10 по результату и все результаты за месяц и сегодня)
            List<ResultTemp> resultList = new List<ResultTemp>();

            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                resultList.Add(new ResultTemp
                {
                    TypingSpeed = random.Next(51), // Генерируем случайное число от 0 до 50
                    TypingAccuracy = random.NextDouble() * 100, // Генерируем случайное число от 0 до 100
                    TypingDate = DateTime.Now.AddDays(-i) // Устанавливаем дату, уменьшая на каждой итерации
                });
            }
            return resultList;
        }
        public Tuple<SeriesCollection, List<string>> GetStatistics(TypingStatisticsPeriodTime periodTime)
        {
            List<ResultTemp> selectedResults = GetSortTypingResultList(periodTime,false);
           
            var seriesCollection = InitSeriesCollection(selectedResults);
            var dateList = new List<string>();
            foreach (var item in selectedResults)
            {
                dateList.Add(item.TypingDate.ToString("dd MMM yyyy",new CultureInfo("en-US")));
            }
            return Tuple.Create(seriesCollection, dateList);
        }
        private SeriesCollection InitSeriesCollection(List<ResultTemp> statList)
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

        private ChartValues<int> GetSpeedChartValues(List<ResultTemp> collection)
        {
            var chartValues = new ChartValues<int>();
            foreach (var stat in collection)
            {
                chartValues.Add(stat.TypingSpeed);
            }
            return chartValues;
        }
        private ChartValues<double> GetAccuracyChartValues(List<ResultTemp> collection)
        {
            var chartValues = new ChartValues<double>();
            foreach (var stat in collection)
            {
                chartValues.Add(stat.TypingAccuracy);
            }
            return chartValues;
        }


        public List<ResultTemp> GetSortTypingResultList(TypingStatisticsPeriodTime periodTime,bool isForTable)
        {
            List<ResultTemp> selectedResults;
            switch (periodTime)
            {
                case TypingStatisticsPeriodTime.AllTime:
                    {
                        selectedResults = _typingUserStatisticList;
                        break;
                    }
                case TypingStatisticsPeriodTime.Month:
                    {
                        int currentMonth = DateTime.Now.Month;
                        selectedResults = _typingUserStatisticList.FindAll(element => element.TypingDate.Month.Equals(currentMonth));
                        break;
                    }
                case TypingStatisticsPeriodTime.Day:
                    {
                        int currentDay = DateTime.Now.Day;
                        selectedResults = _typingUserStatisticList.FindAll(element => element.TypingDate.Day.Equals(currentDay));
                        break;
                    }
                default:
                    {
                        selectedResults = _typingUserStatisticList;
                        break;
                    }

            }
            if (selectedResults.Count > 10)
            {
                selectedResults.Sort((first, second) => second.TypingSpeed.CompareTo(first.TypingSpeed));
                selectedResults = selectedResults.GetRange(0, 10);
                if (isForTable)
                {
                    return selectedResults;
                }
                    

            }
            if (isForTable)
            {
                selectedResults.Sort((first, second) => second.TypingSpeed.CompareTo(first.TypingSpeed));
                return selectedResults;
            }
            selectedResults.Sort((first, second) => first.TypingDate.CompareTo(second.TypingDate));
            return selectedResults;
        }





    }
    public class ResultTemp
    {
        public int TypingSpeed { get; set; }
        public double TypingAccuracy { get; set; }
        public DateTime TypingDate { get; set; }
    }
}
