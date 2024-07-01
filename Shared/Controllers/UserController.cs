using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using CourseProjectKeyboardApplication.Shared.Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Controllers
{
    public static class UserController
    {
        public static User CurrentUser { get; set; }
        private static EducationUserProgressModel _educModel;
        private static TypingTestResultModel _typingTestResultsModel;
        private static List<EducationUsersProgress> _currentUserEducationProgressCollection;
        private static List<TypingTestResult> _typingTestResultsCollection;
        public  static EducationUsersProgress CurrentUserEducationProgress
        {
            get;
            set;
        }

    }
}
