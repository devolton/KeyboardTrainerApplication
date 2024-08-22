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

        /// <summary>
        /// Add pages to local collecton of pages
        /// </summary>
        /// <param name="pages">Collection of pages</param>
        public static void InitPages(List<Page> pages)
        {
            _pagesList = pages;
        }

        /// <summary>
        /// Displaying TypingTutor page
        /// </summary>
        public static void DisplayTypingTutorPage()
        {
            var page = GetPage(typeof(TypingTutorPage)) as TypingTutorPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }
        /// <summary>
        /// Displaying TypingTest page
        /// </summary>
        public static void DisplayTypingTestPage()
        {
            var page = GetPage(typeof(TypingTestPage)) as TypingTestPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }

        /// <summary>
        /// Displaying Learn page
        /// </summary>
        public static void DisplayLearnPage()
        {
            var page = GetPage(typeof(LearnPage)) as LearnPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }

        /// <summary>
        /// Displaying TypingCertificates page
        /// </summary>
        public static void DisplayTypingCertificatesPage()
        {
            var page = GetPage(typeof(TypingCertificatesPage)) as TypingCertificatesPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }
        /// <summary>
        /// Displaying TypingCertificationResult page 
        /// </summary>
        public static void DisplayTypingCertificatesResultPage()
        {
            var page = GetPage(typeof(TypingCertificationResultsPage)) as TypingCertificationResultsPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }

        /// <summary>
        /// Displaying EducationResults page
        /// </summary>
        public static void DisplayEducationResultsPage()
        {
            var page = GetPage(typeof(EducationResultsPage)) as EducationResultsPage;
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }

        /// <summary>
        /// Displaying EditUserProfile page
        /// </summary>
        public static void DisplayEditUserProfilePage()
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
        /// <summary>
        /// Displaying TypingTestResult page
        /// </summary>
        public static void DisplayTypingTestResultPage()
        {
            var page = GetPage(typeof(TypingTestResultPage)) as TypingTestResultPage;
            if(page != null)
            {
                MainFrame.Content = page;
            }
        }


        /// <summary>
        /// Method choice page from collection based on Type and return it
        /// </summary>
        /// <param name="pageType">Type of page which we want display</param>
        /// <returns>Page based on type</returns>
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
