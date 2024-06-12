using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.View.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Context
{
    public class TypingTutorDbContext:DbContext
    {
        private const string  _DB_NAME= "typingTutorDb";
        private static TypingTutorDbContext _instance;
        public DbSet<User> Users { get; set; }
        public DbSet<TypingTestResult> TypingTestResults { get; set; }
        public DbSet<EnglishLayoutLesson> EnglishLayoutLessons { get; set; }
        public DbSet<EnglishLayoutLevel> EnglishLayoutLevels { get; set; }
        public DbSet<EducationUsersProgress> EducationUsersProgresses { get; set; }
        
        private TypingTutorDbContext():base($"name={_DB_NAME}")
        {
            System.Data.Entity.Database.SetInitializer <TypingTutorDbContext>(new TypingTutorDbContextInitializer());
            this.Database.Initialize(true);

        }
        public static TypingTutorDbContext Instance()
        {
            _instance ??= new TypingTutorDbContext();
            return _instance;
        }
    }
}
