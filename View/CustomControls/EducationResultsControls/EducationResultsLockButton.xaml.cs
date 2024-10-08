﻿
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Interfaces;
using CourseProjectKeyboardApplication.Shared.Providers;
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

namespace CourseProjectKeyboardApplication.View.CustomControls.EducationResults
{
    /// <summary>
    /// Логика взаимодействия для EducationResultsLockButton.xaml
    /// </summary>
    public partial class EducationResultsLockButton : UserControl,IEducationResultLessonButton
    {
        public string LessonNumber { 
            get { return(string)GetValue(LessonNumberProperty);}
            set { SetValue(LessonNumberProperty, value); }
        }
        public ImageSource LockIconImageSource { 
            get { return(ImageSource)GetValue(LockIconImageSourceProperty);}
            set { SetValue(LockIconImageSourceProperty,value);}
        }

        public EnglishLayoutLesson EducationLesson { get; set; }
        public EducationUsersProgress EducationUserProgress { get; set; }

        public static readonly DependencyProperty LessonNumberProperty =
            DependencyProperty.Register("LessonNumber",typeof(string),typeof(EducationResultsLockButton),new PropertyMetadata(" "));
        public static readonly DependencyProperty LockIconImageSourceProperty =
            DependencyProperty.Register(nameof(LockIconImageSource),typeof(ImageSource),typeof(EducationResultsLockButton), new PropertyMetadata(null));
        public EducationResultsLockButton(EnglishLayoutLesson educationLesson, EducationUsersProgress educationUserProgress=null)
        {
            InitializeComponent();
            EducationLesson = educationLesson;
            EducationUserProgress = educationUserProgress;
            LessonNumber = educationLesson.Ordinal.ToString();
            DataContext = this;
            
        }

        private  void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LockIconImageSource = AppImageSourceProvider.LockIconImageSource;
        }
    }
}
