using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Mediators;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProjectKeyboardApplication.Shared.Controllers
{
    public static class UserController
    {
        public static User CurrentUser { get; set; }
        private static EducationUserProgressModel _educModel;
        private static TypingTestResultModel _typingTestResultsModel;
        private static List<EducationUsersProgress> _currentUserEducationProgressCollection;
        private static List<TypingTestResult> _typingTestResultsCollection;
        static UserController()
        {
            _educModel = DatabaseModelMediator.EducationUserProgressModel;
            _currentUserEducationProgressCollection = _educModel.GetUsersEducationProgress(1).ToList();


        }
        public static EducationUsersProgress CurrentUserEducationProgress
        {
            get;
            set;
        }
        public static void UpdateCurrentEducationUserProgress(bool isLessCompleted, bool isWithoutMistakeCompleted, bool isSpeedCompleted)
        {
            if (isLessCompleted)
            {
                CurrentUserEducationProgress.IsLessThanTwoErrorsCompleted = isLessCompleted;
            }
            if (isWithoutMistakeCompleted)
            {
                CurrentUserEducationProgress.IsWithoutErrorsCompleted = isWithoutMistakeCompleted;
            }
            if (isSpeedCompleted)
            {
                CurrentUserEducationProgress.IsSpeedCompleted = isSpeedCompleted;
            }

            //сделать сохранение в бд по таймеру и при срабатывании события Close
            try
            {
                _educModel.SaveChanges(); // using timer or destructor
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void SetNextEducationUserProgeress()
        {
            var currentIndex = _currentUserEducationProgressCollection.IndexOf(CurrentUserEducationProgress);
            if (currentIndex != _currentUserEducationProgressCollection.Count - 1)
            {
                CurrentUserEducationProgress = _currentUserEducationProgressCollection[++currentIndex];
            }

        }

    }
}
