using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Shared.Mediators;
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
        private SeriesCollection _typingUserStatisticSeriesCollection;
        private LineSeries _typingAccuracyLineSeries;
        private LineSeries _typingSpeedLineSeries;
        private TypingTestResultModel _typingTestResultModel;
        private List<TypingTestResult> _userTypingTestsCollection;
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
            _typingTestResultModel = DatabaseModelMediator.TypingTestResultModel;
            _userTypingTestsCollection = _typingTestResultModel.GetTypingTestResultsByUserId(1).ToList();



        }

        public Tuple<SeriesCollection, List<string>> GetStatistics(TypingStatisticsPeriodTime periodTime)
        {
            List<TypingTestResult> selectedResults = GetSortTypingResultList(periodTime,false);
           
            var seriesCollection = InitSeriesCollection(selectedResults);
            var dateList = new List<string>();
            foreach (var item in selectedResults)
            {
                dateList.Add(item.Date.ToString("dd MMM yyyy",new CultureInfo("en-US")));
            }
            return Tuple.Create(seriesCollection, dateList);
        }
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

        private ChartValues<int> GetSpeedChartValues(List<TypingTestResult> collection)
        {
            var chartValues = new ChartValues<int>();
            foreach (var stat in collection)
            {
                chartValues.Add(stat.Speed);
            }
            return chartValues;
        }
        private ChartValues<double> GetAccuracyChartValues(List<TypingTestResult> collection)
        {
            var chartValues = new ChartValues<double>();
            foreach (var stat in collection)
            {
                chartValues.Add(stat.AccuracyPercent);
            }
            return chartValues;
        }


        public List<TypingTestResult> GetSortTypingResultList(TypingStatisticsPeriodTime periodTime,bool isForTable)
        {
            List<TypingTestResult> selectedResults;
            switch (periodTime)
            {
                case TypingStatisticsPeriodTime.AllTime:
                    {
                        selectedResults = _userTypingTestsCollection;
                        break;
                    }
                case TypingStatisticsPeriodTime.Month:
                    {
                        int currentMonth = DateTime.Now.Month;
                        selectedResults = _userTypingTestsCollection.FindAll(element => element.Date.Month.Equals(currentMonth));
                        break;
                    }
                case TypingStatisticsPeriodTime.Day:
                    {
                        int currentDay = DateTime.Now.Day;
                        selectedResults = _userTypingTestsCollection.FindAll(element => element.Date.Day.Equals(currentDay));
                        break;
                    }
                default:
                    {
                        selectedResults = _userTypingTestsCollection;
                        break;
                    }

            }
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
                selectedResults.Sort((first, second) => second.Speed.CompareTo(first.Speed));
                return selectedResults;
            }
            selectedResults.Sort((first, second) => first.Date.CompareTo(second.Date));
            return selectedResults;
        }





    }

}
