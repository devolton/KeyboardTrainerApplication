using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Providers
{
    public static class DatabaseModelProvider
    {
        private static UserModel _userModel;
        private static EducationUserProgressModel _educationProgressModel;
        private static TypingTestResultModel _typingTestResultModel;
        private static EnglishLayoutLessonModel _lessonsModel;
        static DatabaseModelProvider()
        {

            _userModel = new UserModel();
            _educationProgressModel = new EducationUserProgressModel();
            _typingTestResultModel = new TypingTestResultModel();
            _lessonsModel = new EnglishLayoutLessonModel();

        }
        public static UserModel UserModel => _userModel;
        public static EducationUserProgressModel EducationUserProgressModel => _educationProgressModel;
        public static TypingTestResultModel TypingTestResultModel => _typingTestResultModel;
       
    }
}
