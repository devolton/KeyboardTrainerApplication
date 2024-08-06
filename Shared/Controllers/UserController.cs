using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProjectKeyboardApplication.Shared.Controllers
{
    public static class UserController
    {
        public static User CurrentUser { get; set; }
        public static EnglishLayoutLesson CurrentLesson { get; set; }
      

        static UserController()
        {
           
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

        }
        public static void SetNextEducationUserProgeress()
        {

            CurrentUserEducationProgress = EducationUsersProgressService.GetNextEducationProgress(CurrentUserEducationProgress);
            CurrentLesson = CurrentUserEducationProgress?.EnglishLayoutLesson;

        }
        public static EducationUsersProgress CreateNewEducationUsersProgresses()
        {
            EducationUsersProgress newEducProgress = new EducationUsersProgress
            {
                Id = EducationUsersProgressService.GetIdOfLastEducationProgressElement()+1,
                IsLessThanTwoErrorsCompleted = false,
                IsSpeedCompleted = false,
                IsWithoutErrorsCompleted = false,
                EnglishLayoutLesson = CurrentLesson,
                EnglishLayoutLevel = CurrentLesson.EnglishLayoutLevel,
                EnglishLayoutLessonId = CurrentLesson.Id,
                EnglishLayoutLevelId = CurrentLesson.EnglishLayoutLevelId,
                UserId = CurrentUser.Id,
                User = CurrentUser
            };
            EducationUsersProgressService.AddNewEducationUsersProgressLocal(newEducProgress);
            return newEducProgress;

        }
        public static async Task InitLessons()
        {
             await EnglishLayoutLessonsService.InitLessonsCollectionAsync();
        }
        public static void ChangeCurrentUserLesson()
        {

            var nextLesson = EnglishLayoutLessonsService.GetNextLesson(CurrentLesson);
            if (nextLesson != null)
            {
                CurrentUser.EnglishLayoutLesson = nextLesson;
                CurrentUser.EnglishLayoutLessonId = nextLesson.Id;
                CurrentUser.EnglishLayoutLevel = nextLesson.EnglishLayoutLevel;
                CurrentUser.EnglishLayoutLevelId = nextLesson.EnglishLayoutLevelId;
            }

        }

    }
}
