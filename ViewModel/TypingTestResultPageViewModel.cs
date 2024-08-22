using CourseProjectKeyboardApplication.Model;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
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
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypingTestResultPageViewModel : ViewModelBase
    {
        private string _resultSpeed = string.Empty;
        private string _accuracyPercent = string.Empty;
        private ICommand _displayResultCommand;
        private ICommand _improveScoreCommand;
        private ITypingTestResultPageModel _model;

        private ImageSource _keyboardIconImageSource;

        public TypingTestResultPageViewModel()
        {
            _model = new TypingTestResultPageModel();
            _displayResultCommand = new RelayCommand(OnDisplayResultCommand);
            _improveScoreCommand = new RelayCommand(OnImproveScoreCommand);
            
        }
        //properties
        #region 
        public ICommand DisplayResultCommand => _displayResultCommand;
        public ICommand ImproveScoreCommand => _improveScoreCommand;
        public string SpeedValue
        {
            get { return _resultSpeed; }
            set
            {
                _resultSpeed = value;
                OnPropertyChanged(nameof(SpeedValue));
            }
        }

        public string AccuracyPercent
        {
            get { return _accuracyPercent; }
            set
            {
                _accuracyPercent = value;
                OnPropertyChanged(nameof(AccuracyPercent));
            }
        }
        public ImageSource KeyboardIconImageSource
        {
            get => _keyboardIconImageSource;
            set
            {
                _keyboardIconImageSource = value;
                OnPropertyChanged(nameof(KeyboardIconImageSource));
            }
        }

        #endregion
        //commands
        #region
        /// <summary>
        /// Command of init and displaying test results
        /// </summary>
        /// <param name="param"></param>
        private void OnDisplayResultCommand(object param)
        {
            _model.InitStat();
           KeyboardIconImageSource ??= _model.GetKeyboardIconImageSource();
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
        /// <summary>
        /// Command of displaying TypingTest page
        /// </summary>
        /// <param name="param"></param>
        private void  OnImproveScoreCommand(object param)
        {

            FrameMediator.DisplayTypingTestPage();
        }
        #endregion

    }
}
