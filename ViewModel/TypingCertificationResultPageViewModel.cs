using CourseProjectKeyboardApplication.Model;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;
using CourseProjectKeyboardApplication.Tools;
using System.Windows.Controls;
using CourseProjectKeyboardApplication.View.CustomControls;
using System.Globalization;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingCertificationResultPageViewModel:ViewModelBase
    {
        private StackPanel _statStackPanel;
        private ICommand _drawStatisticsCommand;
        private SeriesCollection _seriesCollection;
        private IEnumerable<string> _typingDateCollection;
        private static TypingCertificationResultPageViewModel _instance;
        private TypingCertificationResultPageModel _typingCertificationResultPageModel;
        private TypingCertificationResultPageViewModel()
        {
            _typingCertificationResultPageModel = new();
            _drawStatisticsCommand = new RelayCommand(OnDrawStatisticsCommand);
            _statStackPanel = new StackPanel();

        }
        public static TypingCertificationResultPageViewModel Instance()
        {
            _instance ??= new TypingCertificationResultPageViewModel();
            return _instance;
        }
        //properties
        #region
        public ICommand DrawStatisticsCommand => _drawStatisticsCommand;
        public SeriesCollection SeriesCollection {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection= value; 
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }
        //разобратся с инитом 
        public StackPanel StatStackPanel
        {
            get { return _statStackPanel; }
             set
            {
                _statStackPanel= value;
                InitStatBlock(0);
                OnPropertyChanged(nameof(StatStackPanel));
            }
        }

        private void InitStatBlock(int selectedIndex)
        {
            var statCollection = _typingCertificationResultPageModel.GetSortTypingResultList((TypingStatisticsPeriodTime)selectedIndex, true);
                StatStackPanel.Children.Clear();
                foreach (var item in statCollection)
                {
                    StatStackPanel.Children.Add(new TypingStatLine()
                    {
                        SpeedUnitValue = "wpm",
                        SpeedValue = item.TypingSpeed.ToString(),
                        AccuracyUnitValue = "%",
                        AccuracyValue = item.TypingAccuracy.ToString("#.#"),
                        DateValue = item.TypingDate.ToString("dd MMM yyyy", new CultureInfo("en-US"))
                    }); ;
                }
          
        }

        public IEnumerable<string> TypingDateCollection {
            get { return _typingDateCollection; }
            set
            {
                _typingDateCollection = value;
                OnPropertyChanged(nameof(TypingDateCollection));
            }
        }
        #endregion
        //command
        #region
        private void OnDrawStatisticsCommand(object param)
        {
            int selectedIndex = (int)param;
            var statTuple = _typingCertificationResultPageModel.GetStatistics((TypingStatisticsPeriodTime)selectedIndex);
            SeriesCollection = statTuple.Item1;
            TypingDateCollection = statTuple.Item2;
            InitStatBlock(selectedIndex);

        }
        #endregion
 
       
    }
}
