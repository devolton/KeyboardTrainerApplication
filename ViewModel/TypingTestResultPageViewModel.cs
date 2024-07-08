using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.View.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingTestResultPageViewModel : ViewModelBase
    {
        private string _resultSpeed = string.Empty;
        private string _accuracyPercent = string.Empty;
        private ICommand _displayResultCommand;
        private ICommand _improveScoreCommand;
        private TypingTestResultPageModel _model;
        public TypingTestResultPageViewModel()
        {
            _model = new TypingTestResultPageModel();
            _displayResultCommand = new RelayCommand(OnDisplayResultCommand);
            _improveScoreCommand = new RelayCommand(OnImproveScoreCommand);
            
        }
        //properties
        #region 
        public ICommand DisplayResultCommand => _displayResultCommand;
        public string SpeedValue
        {
            get { return _resultSpeed; }
            set
            {
                _resultSpeed = value;
                OnPropertyChanged(nameof(SpeedValue));
            }
        }
        public ICommand ImproveScoreCommand => _improveScoreCommand;
        public string AccuracyPercent
        {
            get { return _accuracyPercent; }
            set
            {
                _accuracyPercent = value;
                OnPropertyChanged(nameof(AccuracyPercent));
            }
        }
        #endregion
        //commands
        #region
        private void OnDisplayResultCommand(object param)
        {
            _model.InitStat();
            try
            {
                var elementPair = (KeyValuePair<LanguageLayotStatisticBlock, LanguageLayotStatisticBlock>)param;
                AccuracyPercent = _model.GetAccuracyPercentStr();
                SpeedValue = _model.GetSpeedStr();
                var speedBlock = elementPair.Key;
                var accuracyBlock = elementPair.Value;
                speedBlock.StatValue= SpeedValue;
                accuracyBlock.StatValue=AccuracyPercent;
            }
            catch(Exception ex)
            {
                return;
            }

            }
        private void  OnImproveScoreCommand(object param)
        {

            FrameMediator.DisplayTypingTestPage();
        }
        #endregion

    }
}
