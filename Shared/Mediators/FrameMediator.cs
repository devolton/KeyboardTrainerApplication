using CourseProjectKeyboardApplication.AppPages.Pages;
using CourseProjectKeyboardApplication.View.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseProjectKeyboardApplication.Shared.Mediators
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
            var page = GetPage(typeof(TypingTutorPage)) as TypingTutorPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }
        public static void DisplayTypingTestPage()
        {
            var page = GetPage(typeof(TypingTestPage)) as TypingTestPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }
        public static void DisplayLearnPage()
        {
            var page = GetPage(typeof(LearnPage)) as LearnPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }
        public static void DisplayTypingCertificatesPage()
        {
            var page = GetPage(typeof(TypingCertificatesPage)) as TypingCertificatesPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }
        public static void DisplayTypingCertificatesResultPage()
        {
            var page = GetPage(typeof(TypingCertificationResultsPage)) as TypingCertificationResultsPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }
        public static void DisplayEducationResultsPage()
        {
            var page = GetPage(typeof(EducationResultsPage)) as EducationResultsPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }
        public static void DisplayEditUserProfilPage()
        {
            var page = GetPage(typeof(EditUserProfilPage)) as EditUserProfilPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }
        public static void DisplayTypingTutorResultPage()
        {
            var page = GetPage(typeof(TypingTutorResultPage)) as TypingTutorResultPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }

        }

        private static Page? GetPage(Type pageType)
        {
            foreach (var onePage in _pagesList)
            {

                if (onePage.GetType().Equals(pageType))
                {
                    return onePage;

                }
            }
            return null;
        }

    }
}
