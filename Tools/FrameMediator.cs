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
            DisplayPage(typeof(TypingTutorPage));
        }
        public static void DisplayTypingTestPage()
        {
            DisplayPage(typeof(TypingTestPage));
        }
        public static void DisplayLearnPage()
        {
            DisplayPage(typeof(LearnPage));
        }
        public static void DisplayTypingCertificatesPage()
        {
            DisplayPage(typeof(TypingCertificatesPage));
        }
        public static void DisplayTypingCertificatesResultPage()
        {
            DisplayPage(typeof(TypingCertificationResultsPage));
        }
        public static void DisplayEducationResultsPage()
        {
            DisplayPage(typeof(EducationResultsPage));
        }
        public static void DisplayEditUserProfilPage()
        {
            DisplayPage(typeof(EditUserProfilPage));
        }
        private static void DisplayPage(Type pageType)
        {
            foreach (var onePage in _pagesList)
            {
              
                if (onePage.GetType().Equals(pageType))
                {
                    MainFrame.Content = onePage;
                    return;
                }
            }
        }
   
    }
}
