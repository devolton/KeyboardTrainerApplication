using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Mediators
{
    public static class DatabaseModelMediator
    {
        private static UserModel _userModel;
        private static EducationUserProgressModel _educationProgressModel;
        private static TypingTestResultModel _typingTestResultModel;
        private static EnglishLayoutLessonModel _lessonsModel;
        private static EnglishLayoutLevelModel _levelModel; 
        static DatabaseModelMediator()
        {
            _userModel = new UserModel();
            _educationProgressModel=new EducationUserProgressModel();
            _typingTestResultModel=new TypingTestResultModel();
            _lessonsModel=new EnglishLayoutLessonModel();
            _levelModel=new EnglishLayoutLevelModel();
            
        }
        public static UserModel UserModel => _userModel;
        public static EducationUserProgressModel EducationUserProgressModel => _educationProgressModel;
        public static TypingTestResultModel TypingTestResultModel => _typingTestResultModel;
        public static EnglishLayoutLessonModel EnglishLayoutLessonModel => _lessonsModel;
        public static EnglishLayoutLevelModel EnglishLayoutLevelModel => _levelModel;

    }
}
