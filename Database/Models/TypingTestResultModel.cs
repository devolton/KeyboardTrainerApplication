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
    public class TypingTestResultModel:BaseTypingTutorModel
    {
        private DbSet<TypingTestResult> _typingTestResults;
        public TypingTestResultModel()
        {
            _typingTestResults = _context.TypingTestResults;
        }
        public IEnumerable<TypingTestResult> GetTypingTestResultsByUserId(int userId)
        {
            return _typingTestResults.Where(oneTestResult => oneTestResult.UserId == userId);
        }
        public int RemoveUsersTest(int userId)
        {
            
            var removeTestResultsCollection = _typingTestResults.Where(oneResult => oneResult.UserId.Equals(userId));
            int removeCount = removeTestResultsCollection.Count();
            _typingTestResults.RemoveRange(removeTestResultsCollection);
            return removeCount;
        }
        public void AddNewTypingTestResult(TypingTestResult typingTestResult)
        {
            _typingTestResults.Add(typingTestResult);
           
        }
        
        public TypingTestResult? GetBestUserTestResult(int userId)
        {
            return _typingTestResults.Where(oneResult => oneResult.Id.Equals(userId))?.OrderByDescending(oneResult=>oneResult.Speed).FirstOrDefault();
        }
        
    }
}
