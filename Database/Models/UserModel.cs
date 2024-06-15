using CourseProjectKeyboardApplication.View.Pages;
using CourseProjectKeyboardApplication.Database.Context;
using CourseProjectKeyboardApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KeyboardApplicationToolsLibrary.AuthorizationTools;
using System.Collections;
using System.Xml;

namespace CourseProjectKeyboardApplication.Database.Models
{
    public class UserModel:BaseTypingTutorModel,IEnumerable<User>
    {
        private DbSet<User> _users;

        public UserModel()
        {
            _users = _context.Users;
        }
        public User? GetUserById(int Id)
        {
            return _users.FirstOrDefault(oneUser => oneUser.Id.Equals(Id));
        }
        public bool IsUniqueEmail(string email)
        {
            return !_users.Any(oneUser => oneUser.Email.Equals(email));
        }
        public bool IsUniqueLogin(string login)
        {
            return !_users.Any(oneUser => oneUser.Login.Equals(login));
        }
        public int UpdateUser(User user)
        {
            int successOperationCode = 0;
            User userInDbSet = GetUserById(user.Id);
            if (userInDbSet != null)
            {
                userInDbSet.Email = user.Email;
                userInDbSet.Name = user.Name;
                userInDbSet.Login = user.Login;
                userInDbSet.Password = PasswordSHA256Encrypter.EncryptPassword(user.Password);
                userInDbSet.EnglishLayoutLesson = user.EnglishLayoutLesson;
                userInDbSet.EnglishLayoutLevel = user.EnglishLayoutLevel;
                userInDbSet.EnglishLayoutLessonId = user.EnglishLayoutLessonId;
                userInDbSet.EnglishLayoutLevelId = user.EnglishLayoutLevelId;
                userInDbSet.AvatarPath = user.AvatarPath;
                successOperationCode++;
            }
            return successOperationCode;

        }
        public bool IsThereUserInDatabase(int Id)
        {
            return _users.Any(oneUser => oneUser.Id.Equals(Id));
        }
        public int RemoveUser(int id)
        {
            int successOperationCode = 0;
            User user = GetUserById(id);
            if (user != null)
            {
                _users.Remove(user);
                successOperationCode++;
            }
            return successOperationCode;

        }
        public void AddNewUser(User newUser) // maybe change into int 
        {
            _users.Add(newUser);
        }
        public bool IsUserExistByEmail(string email)
        {
            return _users.Any(oneUser => oneUser.Email == email);
        }
        public bool IsUserExistByLogin(string login)
        {
            return _users.Any(oneUser => oneUser.Login == login);
        }
        public User? GetUserByLoginOrEmailAndPassword(string loginOrEmail, string shaPassword)
        {
            return _users.FirstOrDefault(oneUser => (oneUser.Login == loginOrEmail || oneUser.Email == loginOrEmail)
            && oneUser.Password == shaPassword);
        }

        public IEnumerator<User> GetEnumerator()
        {
            return _users.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
