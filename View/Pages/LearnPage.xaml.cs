﻿using CourseProjectKeyboardApplication.Shared.Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProjectKeyboardApplication.AppPages.Pages
{
    /// <summary>
    /// Логика взаимодействия для LearnPage.xaml
    /// </summary>
    public partial class LearnPage : Page
    {
        private LearnPageEnInfoModel _enLocalisation;
        public LearnPage( )
        {
            InitializeComponent();
            _enLocalisation = new LearnPageEnInfoModel();
            DataContext = _enLocalisation;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
    

        }

        private void TestSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            FrameMediator.DisplayTypingTestPage();


        }

        private void StartLearnButton_Click(object sender, RoutedEventArgs e)
        {
            FrameMediator.DisplayTypingTutorPage();

        }
    }
}
