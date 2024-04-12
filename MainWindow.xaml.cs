﻿using CourseProjectKeyboardApplication.AppPages.Pages;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProjectKeyboardApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LearnPage _learnPage;
        private TypingTestPage _typingTestPage;
        private TypingTutorPage _typingTutorPage;
        public MainWindow()
        {
            InitializeComponent();
            _learnPage = new LearnPage(MainFrame);
            _typingTutorPage = new TypingTutorPage();
            _typingTestPage = new TypingTestPage();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();

        }

        private void TypingTutorMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _typingTutorPage;
        }

        private void TypingTestMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _typingTestPage;
        }

        private void LearnMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _learnPage;

        }
    }
}