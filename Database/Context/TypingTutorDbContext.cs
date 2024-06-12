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
