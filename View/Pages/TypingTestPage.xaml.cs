﻿using CourseProjectKeyboardApplication.ViewModel;
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
    /// Логика взаимодействия для TypingTestPage.xaml
    /// </summary>
    public partial class TypingTestPage : Page
    {
        private TypingTestPageViewModel _viewModel;

        public TypingTestPage()
        {
            _viewModel = TypingTestPageViewModel.Instance();
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void TakeTestButton_Click(object sender, RoutedEventArgs e)
        {
            TypingTestScrollViewer.ScrollToVerticalOffset(20);

        }


        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            _viewModel.KeyDownCommand.Execute(e.Key);
        }



        private void Page_LostFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.EndTestCommand.Execute(e);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TypingPage.Focus();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            _viewModel.ChangeTimeValueCommand.Execute(null);
        }
    }
}
