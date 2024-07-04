﻿using CourseProjectKeyboardApplication.AppPages.Pages;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Mediators;
using CourseProjectKeyboardApplication.View.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace CourseProjectKeyboardApplication.Model
{
    public class TypingTutorPageModel
    {
        private Dictionary<Key, string> _defaultKeyValueDictionary;
        private Dictionary<Key, string> _shiftPressedKeyValueDictionary;
        private int _typingTutorSpeed;
        private string _currentLearnString = string.Empty;
        private int _missClickCounter;
        private int _wordsCount;
        private int _currentFocusWordIndex;
        private List<Run> _lettersRunsList;
        private Stopwatch _stopwatcher;
        private double _progressBarMaxValue;

        public static TypingTutorPageModel _instance;

        private TypingTutorPageModel()
        {
            
            _currentFocusWordIndex = 0;
            _lettersRunsList = new List<Run>(_currentLearnString.Length);
            InitKeyValueDictionaries();
            _stopwatcher = new Stopwatch();      
        }
        /// <summary>
        /// Start stopwatcher
        /// </summary>
        public void StartMeasureTime()
        {
            _stopwatcher.Start();
        }
        /// <summary>
        /// Stop stopwatcher
        /// </summary>
        public void StopMeasureTime()
        {
            _stopwatcher.Stop();
        }
        public static TypingTutorPageModel Instance()
        {
            _instance ??= new TypingTutorPageModel();
            return _instance;
        }
        /// <summary>
        /// Get progress bar max value
        /// </summary>
        /// <returns>Max value of progress bar </returns>
        public double GetProgressBarMaxValue()
        {
            return _progressBarMaxValue;
        }
        /// <summary>
        /// Notify that char is uppercase
        /// </summary>
        /// <returns>Is uppercase current letter</returns>
        public bool IsFocusCharUppercase()
        {
            return _shiftPressedKeyValueDictionary.Any(onePair => onePair.Value.Equals(_currentLearnString[_currentFocusWordIndex].ToString()));
        }
        /// <summary>
        /// Set error Runs style when user pressed incorrectly key
        /// </summary>
        public void SetRunErrorStyle()
        {
            _lettersRunsList[_currentFocusWordIndex].Foreground = System.Windows.Media.Brushes.Red;
        }
        /// <summary>
        /// Remove error Runs style when user releases incorrectly pressed key
        /// </summary>
        public void RemoveRunErrorStyle()
        {
            _lettersRunsList[_currentFocusWordIndex].Foreground = System.Windows.Media.Brushes.Violet;
        }

        /// <summary>
        /// Get tag of current char
        /// </summary>
        /// <returns>tag of current key</returns>
        public string GetCurrentKeyTag()
        {
            return _currentLearnString[_currentFocusWordIndex].ToString();
        }
        /// <summary>
        /// Get bool value: is corrert keyboard button click
        /// </summary>
        /// <param name="pushedKey">Key of pressed keyboard button</param>
        /// <param name="isShiftPushed">flag: is shift or no</param>
        /// <returns>Is valid keyboard button pressed</returns>
        public bool IsValidPushedButton(Key pushedKey, bool isShiftPushed)
        {
            if (isShiftPushed)
            {
                return _shiftPressedKeyValueDictionary.Any(onePair => onePair.Key == pushedKey && onePair.Value.Equals(_currentLearnString[_currentFocusWordIndex].ToString()));
            }
            return _defaultKeyValueDictionary.Any(onePair => onePair.Key == pushedKey && onePair.Value.Equals(_currentLearnString[_currentFocusWordIndex].ToString()));


        }

         /// <summary>
         /// Get list of runs elements for each leter of lesson string 
         /// </summary>
         /// <returns>List of run elements</returns>
        public List<Run> GetLearnStrRuns()
        {
            //maybe remove reset method and reset data when lesson is starting
            #region
            _currentLearnString = UserController.CurrentLesson?.Text ?? "Hello world my name is Antonio Montana"; //вынести в отдельную функцию 
            _progressBarMaxValue = _currentLearnString.Length;
            _wordsCount = GetWordsCount();
            #endregion
            _lettersRunsList.Clear();
            for (int i = 0; i < _currentLearnString.Length; i++)
            {
                _lettersRunsList.Add(new Run(_currentLearnString[i].ToString()));
                _lettersRunsList[i].FontWeight = FontWeights.Bold;
                if (i == _currentFocusWordIndex)
                {
                    _lettersRunsList[i].Foreground = System.Windows.Media.Brushes.Violet;
                }
            }
            return _lettersRunsList;
        }
        /// <summary>
        /// Change current and next runs styles on successfull keyboard button pressed 
        /// </summary>
        public void ChangeFocusToNextRun()
        {

            _lettersRunsList[_currentFocusWordIndex].Foreground = System.Windows.Media.Brushes.LightGray;
            if (_currentFocusWordIndex.Equals(_lettersRunsList.Count - 1))
            {
                _stopwatcher.Stop();
                _typingTutorSpeed = GetTypingTutorSpeed();
                TypingTutorResultController.TypingTutorSpeed = _typingTutorSpeed;
                TypingTutorResultController.MisclickCount = _missClickCounter;
                FrameMediator.DisplayTypingTutorResultPage();
                return;
            }
            _currentFocusWordIndex++;
            _lettersRunsList[_currentFocusWordIndex].Foreground = System.Windows.Media.Brushes.Violet;
        }
        /// <summary>
        /// Method of lesson restart
        /// </summary>
        public void LessonReset()
        {
            _currentFocusWordIndex = 0;
            _missClickCounter = 0;
            _stopwatcher.Stop();
            _stopwatcher.Reset();
            foreach (var oneRun in _lettersRunsList)
            {
                oneRun.Foreground = System.Windows.Media.Brushes.Gray;
            }
            _lettersRunsList[0].Foreground = System.Windows.Media.Brushes.Violet;
            
        }
        /// <summary>
        /// Get string tag value of pressed keyboard button
        /// </summary>
        /// <param name="key">Key of pressed keyboard button</param>
        /// <returns>Str tag of current pressed keyboard button</returns>
        public string GetKeyStrTag(Key key)
        {
            return _defaultKeyValueDictionary.FirstOrDefault(onePair => onePair.Key.Equals(key)).Value;
        }
        /// <summary>
        /// Increment of missclick count
        /// </summary>
        public void AddMissclickCount()
        {
            _missClickCounter++;
        }

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
    /// Get words count in current lesson str
    /// </summary>
    /// <returns>Count of words in lesson string</returns>
        private int GetWordsCount()
        {
            char delimiters = ' ';
            string[] words = _currentLearnString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }
        /// <summary>
        /// Culculate the avarage print speed
        /// </summary>
        /// <returns>Typing tutor speed</returns>
        private int GetTypingTutorSpeed()
        {
            return (int)(_wordsCount * 60 / _stopwatcher.Elapsed.TotalSeconds);
            
        }



    }
}
