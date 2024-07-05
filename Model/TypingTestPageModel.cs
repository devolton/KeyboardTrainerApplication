﻿using System;
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
        private string _currentText = "the sun rises, painting the sky with hues of orange and pink. Birds chirp their morning song, welcoming the dawn. Dew glistens on blades of grass, nature's own jewels. A new day begins, full of possibilities and adventures waiting to be discovered. Embrace the day, for it is yours to conquer.";
        private Dictionary<Key, string> _defaultKeyValueDictionary;
        private Dictionary<Key, string> _shiftPressedKeyValueDictionary;

        private TypingTestPageModel()
        {
            _runsList = new List<Run>(_currentText.Length);
            _currentSymbolIndex = 0;
            _wordsTypingCount = 1;
            _misclickCount = 0;
            _timerInterval = 30_000;
            _timer = new System.Timers.Timer(_timerInterval);
            _timer.AutoReset = false;
            _timer.Elapsed += Timer_Elapsed;
            InitKeyValueDictionaries();

        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FrameMediator.MainFrame.Content = new TypingTestResultPage(GetTypingTutorSpeed(), _currentText.Length, _misclickCount);

            });
            

        }


        public static TypingTestPageModel Instance()
        {
            _instance ??= new TypingTestPageModel();
            return _instance;
        }
        public List<Run> GetTextRuns()
        {


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
