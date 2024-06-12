using CourseProjectKeyboardApplication.Database.Context;
using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Models
{
    public class TypingTestResultModel
    {
        private TypingTutorDbContext _context;
        private DbSet<TypingTestResult> _typingTestResults;
        public TypingTestResultModel()
        {
            _context = TypingTutorDbContext.Instance();
            _typingTestResults = _context.TypingTestResults;
        }
        public IEnumerable<TypingTestResult> GetTypingTestResultsByUserId(int userId)
        {
            return _typingTestResults.Where(oneTestResult => oneTestResult.UserId == userId);
        }
       
    }
}
