using CourseProjectKeyboardApplication.ApiClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Providers
{
    public static class DbApiClientProvider
    {
        private static HttpClient _httpClient;
        private static JsonSerializerOptions _jsonOptions;


        private static EnglishTypingTestTextApiClient _englishTypingTextApiClient;
        private static UserApiClient _userApiClient;
        private static EducationUserProgressApiClient _educationUserProgressApiClient;
        private static TypingTestResultApiClient _typingTestResultApiClient;
        private static EnglishLayoutLevelApiClient _englishLayoutLevelApiClient;
        private static EnglishLayoutLessonApiClient _englishLayoutLessonApiClient;
        static DbApiClientProvider()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7199/");
            _jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };
            _englishTypingTextApiClient = new EnglishTypingTestTextApiClient(_httpClient, _jsonOptions);
            _userApiClient = new UserApiClient(_httpClient, _jsonOptions);
            _educationUserProgressApiClient = new EducationUserProgressApiClient(_httpClient, _jsonOptions);
            _typingTestResultApiClient = new TypingTestResultApiClient(_httpClient, _jsonOptions);
            _englishLayoutLevelApiClient = new EnglishLayoutLevelApiClient(_httpClient, _jsonOptions);
            _englishLayoutLessonApiClient = new EnglishLayoutLessonApiClient(_httpClient, _jsonOptions);


        }
        public static UserApiClient UserApiClient => _userApiClient;
        public static EnglishTypingTestTextApiClient EnglishTypingTestTextApiClient => _englishTypingTextApiClient;
        public static EducationUserProgressApiClient EducationUserProgressApiClient => _educationUserProgressApiClient;
        public static EnglishLayoutLevelApiClient EnglishLayoutLevelApiClient => _englishLayoutLevelApiClient;
        public static EnglishLayoutLessonApiClient EnglishLayoutLessonApiClient => _englishLayoutLessonApiClient;
        public static TypingTestResultApiClient TypingTestResultApiClient => _typingTestResultApiClient;

        public static void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
