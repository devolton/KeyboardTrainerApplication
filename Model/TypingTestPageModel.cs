using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using System.Timers;
using CourseProjectKeyboardApplication.View.Pages;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingTestPageModel
    {
        private List<Run> _runsList;
        private int _wordsTypingCount;
        private System.Timers.Timer _timer;
        private int _currentSymbolIndex;
        private int _timerInterval;
        private int _misclickCount;
        private static TypingTestPageModel _instance;
        private string _currentText = string.Empty;
        private Dictionary<Key, string> _defaultKeyValueDictionary;
        private Dictionary<Key, string> _shiftPressedKeyValueDictionary;
        private List<EnglishTypingTestText> _testTextCollection;
        private Random _random;
        private string _infoBlockRightHeaderText = "How is typing speedmeasured on DevoltonLabs?";
        private string _infoBlockLeftHeaderText = "Why do I need to take a typing test?";
        private string _infoBlockRightBodyText = "The most common way to measure typing speed is words per minute, or WPM. The 'word' is an average of 5 characters. To calculate WPM, simply take the number of words typed in a minute with no typos and divide by five. For example, if you type 100 characters in a minute including spaces, your typing speed would be 20 WPM. We all know how frustrating it is to make a typo in an important document.But did you know that typos can also have a major impact on your typing speed? That's why we don't allow you to continue typing if you have a typo in your test.You have to fix it to proceed with the WPM test.";
        private string _infoBlockLeftBodyText = "There are many reasons why you might want to take a typing speed test. Perhaps you’re curious to find out how fast you can type, or maybe you want to see if you need to improve your accuracy. Either way, a typing speed test is a great way to estimate your progress. The average typing speed is 40 words per minute, so if you can beat that, you’re doing great!\r\n\r\nYou can take the test as many times as you like, and each time you’ll likely see your speed and accuracy improve. So why not give it a try today? You might be surprised at how fast you can type.";
        private string _firstPartNearAchivementTableText = "Do you know that you can get certified in keyboarding on any layout? That’s right — whether you’re a QWERTY fan or prefer DVORAK, there’s a certification test for you.";
        private string _secondPartNearAchivementTebleText = "You can take the test as many times as you want! Only the best score will count towards your certification, there’s no need to worry about making a mistake.";

        private TypingTestPageModel()
        {
            _runsList = new List<Run>(_currentText.Length);
            _timerInterval = 30_000;
            _timer = new System.Timers.Timer(_timerInterval);
            _timer.AutoReset = false;
            _timer.Elapsed += Timer_Elapsed;
            InitKeyValueDictionaries();
            _testTextCollection = DatabaseModelMediator.EnglishTypingTestTextModel.GetAllTexts().ToList();
            _random = new Random();
            

        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            TypingTestResultController.MiscliskCount = _misclickCount;
            TypingTestResultController.AllSymbolCount = _currentText.Length;
            TypingTestResultController.TypingSpeed = GetTypingTutorSpeed();
            Application.Current.Dispatcher.Invoke(() =>
            {
                FrameMediator.DisplayTypingTestResultPage();

            });
            

        }


        public static TypingTestPageModel Instance()
        {
            _instance ??= new TypingTestPageModel();
            return _instance;
        }
        public List<Run> GetTextRuns()
        {
            _runsList.Clear();
            for (int i = 0; i < _currentText.Length; i++)
            {
                _runsList.Add(new Run(_currentText[i].ToString()));
                _runsList[i].FontWeight = FontWeights.Bold;
                if (i == _currentSymbolIndex)
                {
                    _runsList[i].Foreground = System.Windows.Media.Brushes.SteelBlue;
                }
            }

            return _runsList;
        }
        public bool IsEnglishLanguageSelected()
        {
            return InputLanguageManager.Current.CurrentInputLanguage.EnglishName.Contains("English");
        }
        public void SetupTest()
        {
            int randomIndex = _random.Next(_testTextCollection.Count);
            _currentText = _testTextCollection[randomIndex].Text;
            _currentSymbolIndex = 0;
            _wordsTypingCount = 1;
            _misclickCount = 0;
        }
        public void ResetTestSettings()
        {
            _runsList.Clear();
            _wordsTypingCount = 1;
            _misclickCount = 0;
            _currentSymbolIndex = 0;

        }
        public void StartTimer()
        {
            _timer.Start();
        } 
        public void TimerReset()
        {
            _timer.Stop();
            _timer.Interval = _timerInterval;
        }
        public bool IsFocusCharUppercase()
        {
            return _shiftPressedKeyValueDictionary.Any(onePair => onePair.Value.Equals(_currentText[_currentSymbolIndex].ToString()));
        }
        public bool IsValidPushedButton(Key pushedKey, bool isShiftPushed)
        {
            if (isShiftPushed)
            {
                return _shiftPressedKeyValueDictionary.Any(onePair => onePair.Key == pushedKey && onePair.Value.Equals(_currentText[_currentSymbolIndex].ToString()));
            }
            return _defaultKeyValueDictionary.Any(onePair => onePair.Key == pushedKey && onePair.Value.Equals(_currentText[_currentSymbolIndex].ToString()));


        }
        
        public void SetSymbolRunStyle(bool isValidPushed)
        {
            Task.Run(async () =>
            {
                

                _runsList[_currentSymbolIndex].Dispatcher.Invoke(() =>
                {
                    if (isValidPushed)
                    {
                        _runsList[_currentSymbolIndex++].Foreground = System.Windows.Media.Brushes.LightGray;
                        _runsList[_currentSymbolIndex].Foreground = System.Windows.Media.Brushes.SteelBlue;
                    }
                    else
                    {
                        _runsList[_currentSymbolIndex].Foreground = System.Windows.Media.Brushes.Red;
                    }
                });
                if (!isValidPushed)
                {
                    await Task.Delay(200);
                    _runsList[_currentSymbolIndex].Dispatcher.Invoke(() =>
                    {
                        _runsList[_currentSymbolIndex].Foreground = System.Windows.Media.Brushes.SteelBlue;
                    });
                }
          
            });

        }
        public string GetLeftInfoHeaderText() => _infoBlockLeftHeaderText;
        public string GetRightInfoHeaderText() => _infoBlockRightHeaderText;
        public string GetLeftInfoBodyText() => _infoBlockLeftBodyText;
        public string GetRightInfoBodyText() => _infoBlockRightBodyText;
        public void IncrementMissclickCount() => ++_misclickCount;
        public void IncrementWordsTypingCount() => _wordsTypingCount++;
        private void InitKeyValueDictionaries()
        {
            _defaultKeyValueDictionary = new Dictionary<Key, string>()
            {
                {Key.Oem3,"`"},{Key.D1,"1"},{Key.D2,"2"},{Key.D3,"3"},{Key.D4,"4"},{Key.D5,"5"},{Key.D6,"6"},{Key.D7,"7"},{Key.D8,"8"},{Key.D9,"9"},{Key.D0,"0" },{Key.OemMinus,"-" },{Key.OemPlus,"="},
                { Key.Q, "q" }, { Key.W, "w" },{ Key.E, "e" },{ Key.R, "r" },{ Key.T, "t" },{ Key.Y, "y" }, { Key.U, "u" },{ Key.I, "i" },{ Key.O, "o" },{ Key.P, "p" },{Key.OemOpenBrackets,"["},{Key.Oem6,"]"},
                { Key.A, "a" },{ Key.S, "s" },{ Key.D, "d" },{ Key.F, "f" },{ Key.G, "g" },{ Key.H, "h" },{ Key.J, "j" },{ Key.K, "k" },{ Key.L, "l" },{Key.Oem1,";" },{Key.OemQuotes,"'"},{Key.Oem5,"\\"},
                { Key.Z, "z" },{ Key.X, "x" },{ Key.C, "c" },{ Key.V, "v" },{ Key.B, "b" },{ Key.N, "n" },{ Key.M, "m" },{Key.OemComma,","},{Key.OemPeriod,"."},{Key.OemQuestion,"/"},
                {Key.Space," " },{Key.Tab,"Tab"},{Key.LeftShift,"LeftShift"},{Key.RightShift,"RightShift"},{Key.Back,"Back"},{Key.CapsLock,"Caps"}


            };
            _shiftPressedKeyValueDictionary = new Dictionary<Key, string>()
            {
                {Key.Oem3,"~"},{Key.D1,"!"},{Key.D2,"@"},{Key.D3,"#"},{Key.D4,"$"},{Key.D5,"%"},{Key.D6,"^"},{Key.D7,"&"},{Key.D8,"*"},{Key.D9,"("},{Key.D0,")" },{Key.OemMinus,"_" },{Key.OemPlus,"+"},
                { Key.Q, "Q" }, { Key.W, "W" },{ Key.E, "E" },{ Key.R, "R" },{ Key.T, "T" },{ Key.Y, "Y" }, { Key.U, "U" },{ Key.I, "I" },{ Key.O, "O" },{ Key.P, "P" },{Key.OemOpenBrackets,"{"},{Key.Oem6,"}"},
                { Key.A, "A" },{ Key.S, "S" },{ Key.D, "D" },{ Key.F, "F" },{ Key.G, "G" },{ Key.H, "H" },{ Key.J, "J" },{ Key.K, "K" },{ Key.L, "L" },{Key.Oem1,":" },{Key.OemQuotes,"\""},{Key.Oem5,"|"},
                { Key.Z, "Z" },{ Key.X, "X" },{ Key.C, "C" },{ Key.V, "V" },{ Key.B, "B" },{ Key.N, "N" },{ Key.M, "M" },{Key.OemComma,"<"},{Key.OemPeriod,">"},{Key.OemQuestion,"?"}

            };
        }
        private int GetTypingTutorSpeed()
        {

            return (int)(((double)_wordsTypingCount / (_timerInterval/1000)*60));

        }
      
       
    
    }
}
