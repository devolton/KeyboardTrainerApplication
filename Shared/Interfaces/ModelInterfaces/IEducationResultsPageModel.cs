using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces
{
    public interface IEducationResultsPageModel
    {
        ImageSource GetStudyIconImageSourceAsync();
        string GetCurrentLevelHeaderStr();
        Task<double> GetPercentOfCompletedLessonsAsync();
        string GetLanguageLayoutTypeHeaderStr();
        Task<IEnumerable<EnglishLayoutLevel>> GetLevelsAsync();
    }
}
