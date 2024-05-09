using CourseProjectKeyboardApplication.AppPages.Pages;
using CourseProjectKeyboardApplication.View.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseProjectKeyboardApplication.Tools
{
    public static class FrameMediator
    {
        private static Frame _mainFrame;
        private static List<Page> _pagesList;

        public static Frame MainFrame
        {
            get { return _mainFrame; }
            set { _mainFrame = value; }
        }
        public static void InitPages(List<Page> pages)
        {
            _pagesList = pages;
        }
        public static void DisplayTypingTutorPage()
        {
            foreach(var onePage in _pagesList)
            {
                var typingTutorPage = onePage as TypingTutorPage;
                if(typingTutorPage is not null)
                {
                    MainFrame.Content = typingTutorPage;
                    return;
                }
                
            }
        }
        public static void DisplayTypingTestPage()
        {
            foreach (var onePage in _pagesList)
            {
                var typingTestPage = onePage as TypingTestPage;
                if (typingTestPage is not null)
                {
                    MainFrame.Content = typingTestPage;
                    return;
                }

            }
        }
        public static void DisplayLearnPage()
        {
            foreach (var onePage in _pagesList)
            {
                var learnPage = onePage as LearnPage;
                if (learnPage is not null)
                {
                    MainFrame.Content = learnPage;
                    return;
                }

            }
        }
   
    }
}
