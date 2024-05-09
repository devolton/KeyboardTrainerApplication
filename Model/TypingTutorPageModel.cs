using CourseProjectKeyboardApplication.AppPages.Pages;
using CourseProjectKeyboardApplication.Tools;
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
        private string _currentLearnString;
        private int _missClickCounter;
        private int _wordsCount;
        private int _currentFocusWordIndex;
        private List<Run> _lettersRunsList;
        private Stopwatch _stopwatcher;
        private double _progressBarMaxValue;

        public static TypingTutorPageModel _instance;

        private TypingTutorPageModel()
        {
            _currentLearnString = "pell qosw hzdf jskf sfsl lsjf jskj jkss lkds jsks jskf jskd";
            _currentFocusWordIndex = 0;
            _wordsCount=GetWordsCount();
            _lettersRunsList = new List<Run>(_currentLearnString.Length);
            InitKeyValueDictionaries();
            _stopwatcher = new Stopwatch();
            _progressBarMaxValue = _currentLearnString.Length;
            
        }
        public void StartMeasureTime()
        {
            _stopwatcher.Start();
        }
        public void StopMeasureTime()
        {
            _stopwatcher.Stop();
        }
        public static TypingTutorPageModel Instance()
        {
            _instance ??= new TypingTutorPageModel();
            return _instance;
        }
        public double GetProgressBarMaxValue()
        {
            return _progressBarMaxValue;
        }
        //уведомляет что символ должен быть нажат с Shift
        public bool IsFocusCharUppercase()
        {
            return _shiftPressedKeyValueDictionary.Any(onePair => onePair.Value.Equals(_currentLearnString[_currentFocusWordIndex].ToString()));
        }

        public void SetRunErrorStyle()
        {
            _lettersRunsList[_currentFocusWordIndex].Foreground = System.Windows.Media.Brushes.Red;
        }
        public void RemoveRunErrorStyle()
        {
            _lettersRunsList[_currentFocusWordIndex].Foreground = System.Windows.Media.Brushes.Violet;
        }

        //возвращает тег текущей буквы 
        public string GetCurrentKeyTag()
        {
            return _currentLearnString[_currentFocusWordIndex].ToString();
        }
        public bool IsValidPushedButton(Key pushedKey, bool isShiftPushed)
        {
            if (isShiftPushed)
            {
                return _shiftPressedKeyValueDictionary.Any(onePair => onePair.Key == pushedKey && onePair.Value.Equals(_currentLearnString[_currentFocusWordIndex].ToString()));
            }
            return _defaultKeyValueDictionary.Any(onePair => onePair.Key == pushedKey && onePair.Value.Equals(_currentLearnString[_currentFocusWordIndex].ToString()));


        }



        //генерирую елемент для каждой буквы 
        public List<Run> GetLearnStrRuns()
        {
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
        //изминения стиля одного елемента Run при нажатии на верную клавишу клавиатуры
        public void ChangeFocusToNextRun()
        {

            _lettersRunsList[_currentFocusWordIndex].Foreground = System.Windows.Media.Brushes.LightGray;
            if (_currentFocusWordIndex.Equals(_lettersRunsList.Count - 1))
            {
                //сбросить все стили 
                _stopwatcher.Stop();
                _typingTutorSpeed = GetTypingTutorSpeed();
                //передавать во фрейм TypingTutorResultPage и передавать результат
                MessageBox.Show($"Errors: {_missClickCounter} Speed: {_typingTutorSpeed}");
                FrameMediator.MainFrame.Content = new TypingTutorResultPage(_typingTutorSpeed,_missClickCounter);


                return;
            }
            _currentFocusWordIndex++;
            _lettersRunsList[_currentFocusWordIndex].Foreground = System.Windows.Media.Brushes.Violet;
        }
        //метод по перезапуску урока 
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
        public string GetKeyStrTag(Key key)
        {
            return _defaultKeyValueDictionary.FirstOrDefault(onePair => onePair.Key.Equals(key)).Value;
        }
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
        private int GetWordsCount()
        {
            char delimiters = ' ';
            string[] words = _currentLearnString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }
        private int GetTypingTutorSpeed()
        {
            return (int)(_wordsCount * 60 / _stopwatcher.Elapsed.TotalSeconds);
            
        }



    }
}
