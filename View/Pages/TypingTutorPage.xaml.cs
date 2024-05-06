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
    /// Логика взаимодействия для TypingTutor.xaml
    /// </summary>
    public partial class TypingTutorPage : Page
    {
        private TypinTurorPageViewModel _typingTutorPageViewModel;
        public TypingTutorPage()
        {
            InitializeComponent();
            _typingTutorPageViewModel = new TypinTurorPageViewModel();

           

        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            _typingTutorPageViewModel.KeyDownCommand.Execute(e.Key);

        }
    }
}
