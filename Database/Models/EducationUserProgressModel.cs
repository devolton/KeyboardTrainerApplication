using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProjectKeyboardApplication.Database.Models
{
    public class EducationUserProgressModel : BaseTypingTutorModel
    {
        private DbSet<EducationUsersProgress> _educationUsersProgresses;
        public EducationUserProgressModel()
        {
            _educationUsersProgresses = _context.EducationUsersProgresses;
        }
        public EducationUsersProgress AddNewEducationUserProgress(EducationUsersProgress educationUser)
        {
            educationUser = _educationUsersProgresses.Add(educationUser);
            return educationUser;
        }
        public IEnumerable<EducationUsersProgress> GetUsersEducationProgress(int userId)
        {
            return _educationUsersProgresses.Where(oneProgress => oneProgress.UserId == userId);
        }
        public int RemoveUsersEducationProgress(int userId)
        {
            var removeEducationProgressesCollection = _educationUsersProgresses.Where(oneProgress => oneProgress.UserId == userId);
            int removeCount = removeEducationProgressesCollection.Count();
            _educationUsersProgresses.RemoveRange(removeEducationProgressesCollection);
            return removeCount;
        }
        public EducationUsersProgress? GetNextEducationProgress(EducationUsersProgress currentProgress)
        {
            return _educationUsersProgresses.Where(oneProg => oneProg.Id > currentProgress.Id)?.OrderBy(oneProg => oneProg.Id).FirstOrDefault();
        }
    }
}
