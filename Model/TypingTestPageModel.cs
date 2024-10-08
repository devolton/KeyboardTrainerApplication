﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Timers;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Providers;
using System.Windows.Media;
using System.Text.Json;
using System.Reflection;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Managers;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingTestPageModel : ITypingTestPageModel
    {
        private List<Run> _runsList;
        private int _wordsTypingCount;
        private System.Timers.Timer _timer;
        private int _currentSymbolIndex;
        private int _timerInterval;
        private int _misclickCount;
        private int _pushedSymbolsCount;

        private string _currentText = string.Empty;
        private Dictionary<Key, string> _defaultKeyValueDictionary;
        private Dictionary<Key, string> _shiftPressedKeyValueDictionary;
        private List<EnglishTypingTestText> _testTextCollection;
        private Random _random;
        private string _infoBlockRightHeaderText = string.Empty;
        private string _infoBlockLeftHeaderText = string.Empty;
        private string _infoBlockRightBodyText = string.Empty;
        private string _infoBlockLeftBodyText = string.Empty;
        private string _firstPartNearAchievementTableText = string.Empty;
        private string _secondPartNearAchievementTableText = string.Empty;
        private ImageSource _starImageSource;
        private ImageSource _flashImageSource;
        private ImageSource _targetImageSource;
        private ImageSource _keyboardIconImageSource;



        public TypingTestPageModel()
        {
            _runsList = new List<Run>(_currentText.Length);
            _timerInterval = 30_000;
            _pushedSymbolsCount = 0;
            _timer = new System.Timers.Timer(_timerInterval);
            _timer.AutoReset = false;
            _timer.Elapsed += Timer_Elapsed;
            InitKeyValueDictionaries();
            InitStaticText();
            InitTextCollectionAsync();
            _random = new Random();


        }
        public ImageSource GetTargetImageSource()
        {
            _targetImageSource ??= AppImageSourceProvider.BlueTargetImageSource;
            return _targetImageSource;
        }
        public ImageSource GetFlashImageSource()
        {
            _flashImageSource ??= AppImageSourceProvider.BlueFlashImageSource;
            return _flashImageSource;
        }
        public ImageSource GetStarImageSource()
        {
            _starImageSource ??= AppImageSourceProvider.BlueStarImageSource;
            return _starImageSource;
        }
        public ImageSource GetKeyboardIconImageSource()
        {
            _keyboardIconImageSource ??= AppImageSourceProvider.KeyboardIconImageSource;
            return _keyboardIconImageSource;
        }

        /// <summary>
        /// Initializing typing test results values and open Typing test result page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {

            TypingTestResultManager.MiscliskCount = _misclickCount;
            TypingTestResultManager.PushedSymbolsCount = _pushedSymbolsCount;
            TypingTestResultManager.TypingSpeed = GetTypingTutorSpeed();
            Application.Current.Dispatcher.Invoke(() =>
            {
                FrameMediator.DisplayTypingTestResultPage();

            });


        }

        /// <summary>
        /// Set interval for timer
        /// </summary>
        /// <param name="milliseconds">Millisecond of interval value</param>
        public void SetTimerInterval(int milliseconds)
        {
            _timerInterval = milliseconds;
        }
        /// <summary>
        /// Get Run element after current Run element
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Check is user select Engilish layout in PC
        /// </summary>
        /// <returns></returns>
        public bool IsEnglishLanguageSelected()
        {
             return InputLanguageManager.Current.CurrentInputLanguage.EnglishName.Contains("English");
            
        }
        /// <summary>
        /// Initializing text collectoin
        /// </summary>
        /// <returns></returns>
        private async Task InitTextCollectionAsync()
        {
            var textCollection = await DbApiClientProvider.EnglishTypingTestTextApiClient.GetAllTextsAsync();
            _testTextCollection = textCollection.ToList();
        }
        /// <summary>
        /// Setup test options
        /// </summary>
        public void SetupTest()
        {

            int randomIndex = _random.Next(_testTextCollection.Count);
            _currentText = _testTextCollection[randomIndex].Text;
            _currentSymbolIndex = 0;
            _wordsTypingCount = 1;
            _misclickCount = 0;
        }
        /// <summary>
        /// Reset test options
        /// </summary>
        public void ResetTestSettings()
        {
            _runsList.Clear();
            _wordsTypingCount = 1;
            _misclickCount = 0;
            _pushedSymbolsCount = 0;
            _currentSymbolIndex = 0;

        }
        /// <summary>
        /// Start timer
        /// </summary>
        public void StartTimer()
        {
            _timer.Interval = _timerInterval;
            _timer.Start();
        }
        /// <summary>
        /// Stop timer
        /// </summary>
        public void TimerReset()
        {
            _timer.Stop();

        }
        /// <summary>
        /// check if the current element is uppercase
        /// </summary>
        /// <returns></returns>
        public bool IsFocusCharUppercase()
        {
            return _shiftPressedKeyValueDictionary.Any(onePair => onePair.Value.Equals(_currentText[_currentSymbolIndex].ToString()));
        }
        /// <summary>
        /// Check if user pushed valid keyboard button
        /// </summary>
        /// <param name="pushedKey">Key of pushed button</param>
        /// <param name="isShiftPushed">Is pushed button is SHIFT</param>
        /// <returns></returns>
        public bool IsValidPushedButton(Key pushedKey, bool isShiftPushed)
        {
            if (isShiftPushed)
            {
                return _shiftPressedKeyValueDictionary.Any(onePair => onePair.Key == pushedKey && onePair.Value.Equals(_currentText[_currentSymbolIndex].ToString()));
            }
            return _defaultKeyValueDictionary.Any(onePair => onePair.Key == pushedKey && onePair.Value.Equals(_currentText[_currentSymbolIndex].ToString()));


        }
        /// <summary>
        /// Set style for Run element 
        /// </summary>
        /// <param name="isValidPushed">Is pushed button is valid</param>
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

        public string GetFirstPartNearAchivementTebleText() => _firstPartNearAchievementTableText;
        public string GetSecondPartNearAchivementTableText() => _secondPartNearAchievementTableText;
        public string GetLeftInfoBodyText() => _infoBlockLeftBodyText;
        public string GetRightInfoBodyText() => _infoBlockRightBodyText;
        /// <summary>
        /// Increment missclick count
        /// </summary>
        /// <returns></returns>
        public void IncrementMissclickCount() => ++_misclickCount;
        /// <summary>
        /// Increment symbols count which user pushed 
        /// </summary>
        public void IncrementPushedSymbolsCount() => ++_pushedSymbolsCount;

        /// <summary>
        /// Increment words count
        /// </summary>
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
        /// <summary>
        /// Culculate of typing tutor speed 
        /// </summary>
        /// <returns></returns>
        private int GetTypingTutorSpeed()
        {

            return (int)(((double)_wordsTypingCount / (_timerInterval / 1000) * 60));

        }
        /// <summary>
        /// Init text proporties with using reflaction
        /// </summary>
        private async void InitStaticText()
        {
            var jsonText = await ContentApiClientProvider.JsonTextApiClient.GetPageJsonAsync(Shared.Enums.PageType.TypingTestPage);
            if (jsonText is not null)
            {
                Dictionary<string, string>? propValueDict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText);
                Type targetType = this.GetType();
                if (propValueDict is not null)
                {
                    foreach (var onePair in propValueDict)
                    {
                        FieldInfo fieldInfo = targetType.GetField($"_{onePair.Key}", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (fieldInfo is null)
                            continue;
                        if (fieldInfo.FieldType == typeof(string))
                        {
                            fieldInfo.SetValue(this, onePair.Value);
                        }
                    }


                }

            }
        }



    }
}
