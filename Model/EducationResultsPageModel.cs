using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Providers;
using CourseProjectKeyboardApplication.Shared.Services;

namespace CourseProjectKeyboardApplication.Model
{
    public class EducationResultsPageModel
    {

        private int _commonLevelCount;
        private string _languageLayoutType = "English layout";
        private int _commonLessonsCount = 0;
        private IEnumerable<EnglishLayoutLevel> _englishLayoutLevelsCollection;


        public EducationResultsPageModel()
        {
            InitAllLevelCountAsync();

        }
        /// <summary>
        /// Get actual user education progress str(current level and common levels count) to educationResultsPage header
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLevelHeaderStr()
        {

            int currentLevelCounter = UserController.CurrentUser.EnglishLayoutLevel.Ordinal;
            return $"Level {currentLevelCounter} from {_commonLevelCount}";
        }

        /// <summary>
        /// Get actual user education progress percent of completed lessons count
        /// </summary>
        /// <returns></returns>
        public async Task<double> GetPercentOfCompletedLessonsAsync()
        {
            var completedLessonsCount = (await EducationUsersProgressService.GetEducationUsersProgressesByUserIdAsync(UserController.CurrentUser.Id))?.Count() ?? 0;
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
        /// <returns>Percent of completed lessons</returns>
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
        private async void InitAllLevelCountAsync()
        {
            await GetLevelsAsync();
            _commonLevelCount = _englishLayoutLevelsCollection.Count();
        }





    }

}
