﻿using CourseProjectKeyboardApplication.ApiClients;
using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Controllers;
using CourseProjectKeyboardApplication.Shared.Providers;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProjectKeyboardApplication.Shared.Services
{
    public static class EducationUsersProgressService
    {
        private static EducationUserProgressApiClient _apiClient;
        private static List<EducationUsersProgress> _educationUserProgressesCollection;
        private static List<EducationUsersProgress> _addedNewEducationUsersProgressCollection;
        private static List<EducationUsersProgress> _updatedEducationUsersProgressCollection;
        static EducationUsersProgressService()
        {
            _apiClient = DbApiClientProvider.EducationUserProgressApiClient;
            _addedNewEducationUsersProgressCollection = new List<EducationUsersProgress>(100);
            _updatedEducationUsersProgressCollection = new List<EducationUsersProgress>(100);

        }
        public static async Task InitEducationUserProgressCollection(int userId)
        {
            _educationUserProgressesCollection = (await _apiClient.GetEducationUsersProgressesByUserIdAsync(userId))?.ToList();
        }
        public static async Task<IEnumerable<EducationUsersProgress>?> GetEducationUsersProgressesByUserIdAsync(int userId)
        {
            _educationUserProgressesCollection ??= (await _apiClient.GetEducationUsersProgressesByUserIdAsync(userId))?.ToList();
            return _educationUserProgressesCollection;
        }
        public static async Task AddNewEducationUsersProgressAsync(EducationUsersProgress educationUsersProgress)
        {
            _apiClient.AddEducationUsersProgressAsync(educationUsersProgress);
        }
        public static async Task SaveAddedEducationUsersResultAsync()
        {
            try
            {
                if (_addedNewEducationUsersProgressCollection is not null && _addedNewEducationUsersProgressCollection.Count != 0)
                {

                    var addTask = _apiClient.AddEductionUsersProgressRangeAsync(_addedNewEducationUsersProgressCollection);
                    Task updateTask = Task.CompletedTask;
                    if (_updatedEducationUsersProgressCollection is not null && _updatedEducationUsersProgressCollection.Count != 0)
                    {
                        updateTask = _apiClient.UpdateRangeEducationUsersProgress(_updatedEducationUsersProgressCollection);
                    }

                    await Task.WhenAll(addTask, updateTask);
                }
                else if (_updatedEducationUsersProgressCollection is not null && _updatedEducationUsersProgressCollection.Count != 0)
                {
                    await _apiClient.UpdateRangeEducationUsersProgress(_updatedEducationUsersProgressCollection);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void AddNewEducationUsersProgressLocal(EducationUsersProgress newEducationUsersProgress)
        {
            _addedNewEducationUsersProgressCollection.Add(newEducationUsersProgress);
            _educationUserProgressesCollection.Add(newEducationUsersProgress);
        }
        public static EducationUsersProgress? GetNextEducationProgress(EducationUsersProgress currentProgress)
        {
            return _educationUserProgressesCollection.Where(oneProg => oneProg.Id > currentProgress.Id)?.OrderBy(oneProg => oneProg.Id).FirstOrDefault();
        }
        public static int GetIdOfLastEducationProgressElement()
        {
            return _educationUserProgressesCollection?.LastOrDefault()?.Id ?? 0;
        }
        public static EducationUsersProgress? GetEducationProgressByLessonId(int lessonId)
        {
            try
            {
                var result = _educationUserProgressesCollection.FirstOrDefault(oneEducProgress => oneEducProgress.EnglishLayoutLessonId == lessonId
                && oneEducProgress.UserId == UserController.CurrentUser.Id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static void UpdateEducationUserProgressLocal(EducationUsersProgress updatedEducationUsersProgress, bool isLessCompleted, bool isWithoutMistakeCompleted, bool isSpeedCompleted)
        {
            var currentEducationUsersProgress = _educationUserProgressesCollection.FirstOrDefault(oneEducProg => oneEducProg.Id == updatedEducationUsersProgress.Id);
            if (currentEducationUsersProgress is not null)
            {
                if (isLessCompleted)
                {
                    currentEducationUsersProgress.IsLessThanTwoErrorsCompleted = isLessCompleted;
                }
                if (isWithoutMistakeCompleted)
                {
                    currentEducationUsersProgress.IsWithoutErrorsCompleted = isWithoutMistakeCompleted;
                }
                if (isSpeedCompleted)
                {
                    currentEducationUsersProgress.IsSpeedCompleted = isSpeedCompleted;
                }
                if (!_addedNewEducationUsersProgressCollection.Contains(currentEducationUsersProgress))
                    _updatedEducationUsersProgressCollection.Add(currentEducationUsersProgress);
            }

        }
        public static int GetEducationUsersProgressesCount() => _educationUserProgressesCollection.Count;
        public static bool IsAllPerfectlyCompleted() => _educationUserProgressesCollection.All(oneEducProg => oneEducProg.IsSpeedCompleted && oneEducProg.IsWithoutErrorsCompleted && oneEducProg.IsLessThanTwoErrorsCompleted);
    }
}
