using CourseProjectKeyboardApplication.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Models
{
    public abstract class BaseTypingTutorModel
    {
        protected TypingTutorDbContext _context;
        public BaseTypingTutorModel()
        {
            _context = TypingTutorDbContext.Instance();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync(); // maybe add await
        }
    }
}
