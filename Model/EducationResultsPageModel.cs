using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Interfaces.ModelInterfaces;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Model
{
    public class EducationResultsPageModel: IEducationResultsPageModel
    {

        private int _commonLevelCount;
        private string _languageLayoutType = "English layout";
        private int _commonLessonsCount = 0;
        private IEnumerable<EnglishLayoutLevel> _englishLayoutLevelsCollection;
        private ImageSource _studyIconImageSource;


        public EducationResultsPageModel()
        {
            InitAllLevelCountAsync();

        }
        /// <summary>
        /// Get study icon image source 
        /// </summary>
        /// <returns>Study icon ImageSouce</returns>
        public  ImageSource GetStudyIconImageSourceAsync()
        {
            _studyIconImageSource ??= AppImageSourceProvider.StudyIconImageSource;
            return _studyIconImageSource;
        }
        /// <summary>
        /// Get actual user education progress str(current level and common levels count) to educationResultsPage header
        /// </summary>
        /// <returns>String of current level header </returns>
        public string GetCurrentLevelHeaderStr()
        {

            int currentLevelCounter = KeyboardAppEducationProgressController.CurrentUser.EnglishLayoutLevel.Ordinal;
            return $"Level {currentLevelCounter} from {_commonLevelCount}";
        }

        /// <summary>
        /// Get actual user education progress percent of completed lessons count
        /// </summary>
        /// <returns>Get percent of completed lessons</returns>
        public async Task<double> GetPercentOfCompletedLessonsAsync()
        {
            var completedLessonsCount = (await EducationUsersProgressService.GetEducationUsersProgressesByUserIdAsync(KeyboardAppEducationProgressController.CurrentUser.Id))?.Count() ?? 0;
            if (_commonLessonsCount == 0)
            {
                foreach (var level in _englishLayoutLevelsCollection)
                {
                    _commonLessonsCount += level.Lessons.Count;
                }
            }
            return ((double)completedLessonsCount / (double)_commonLessonsCount) * 100;

        }
        /// <summary>
        /// Get current language layout str 
        /// </summary>
        /// <returns></returns>
        public string GetLanguageLayoutTypeHeaderStr()
        {
            return _languageLayoutType;
        }
        /// <summary>
        /// Get current english layout levels colletion 
        /// </summary>
        /// <returns>User english layout levels collection</returns>
        public async Task<IEnumerable<EnglishLayoutLevel>> GetLevelsAsync()
        {
            _englishLayoutLevelsCollection ??= await DbApiClientProvider.EnglishLayoutLevelApiClient.GetAllLevelsAsync();
            return _englishLayoutLevelsCollection;
        }

        /// <summary>
        /// Initial all level count
        /// </summary>
        private async Task InitAllLevelCountAsync()
        {
            await GetLevelsAsync();
            _commonLevelCount = _englishLayoutLevelsCollection.Count();
        }





    }

}
