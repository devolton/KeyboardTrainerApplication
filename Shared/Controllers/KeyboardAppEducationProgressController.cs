using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Database.Entities;
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
    public static class KeyboardAppEducationProgressController
    {
        public static User CurrentUser { get; set; }
        public static EnglishLayoutLesson CurrentLesson { get; set; }
      

        static KeyboardAppEducationProgressController()
        {
           
        }
        public static EducationUsersProgress CurrentUserEducationProgress
        {
            get;
            set;
        }

        /// <summary>
        /// Set next EducationUsersProgress in CurrentEducationUsersProgress and update CurrentLesson 
        /// </summary>
        public static void SetNextEducationUserProgeress()
        {

            CurrentUserEducationProgress = EducationUsersProgressService.GetNextEducationProgress(CurrentUserEducationProgress);
            CurrentLesson = CurrentUserEducationProgress?.EnglishLayoutLesson;

        }
        /// <summary>
        /// Creating new EducationUsersProgrees and delegating adding to EducationUsersProgressService
        /// </summary>
        /// <returns>New created EducationUsersProgress entity</returns>
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
        /// <summary>
        /// Delegating initializing EnglishLayoutLesson collection to EnglishLayoutLessonsService 
        /// </summary>
        /// <returns></returns>
        public static async Task InitLessons()
        {
             await EnglishLayoutLessonsService.InitLessonsCollectionAsync();
        }
        /// <summary>
        /// Update current lesson and level of user
        /// </summary>
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
