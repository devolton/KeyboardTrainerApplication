﻿using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Shared.Services;
using CourseProjectKeyboardApplication.Tools;
using KeyboardApplicationToolsLibrary.AuthorizationTools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Model
{
    public class UserSingUpPageModel:RegistrationFormModel
    {
        public UserSingUpPageModel()
        {
          
        }

        public async Task<User> RegisterUser(string name,string login,string email, string password)
        {
            var user = new User
            {
                Name = name,
                Email = email,
                Login = login,
                Password = PasswordSHA256Encrypter.EncryptPassword(password),
                EnglishLayoutLessonId=1,
                EnglishLayoutLevelId=1,
                AvatarPath=""

            };
            user = await UserService.AddNewUserAsync(user);
            return user;
        }

     
    }
}
