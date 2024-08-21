using CourseProjectKeyboardApplication.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Tools.Converters
{
    public static class FromNotifyTypeToMessage
    {
        private static readonly string _serverRequestTimeoutMessage = string.Empty;
        private static readonly string _notUniqueLoginMessage = string.Empty;
        private static readonly string _notUniqueEmailMessage = string.Empty;
        private static readonly string _userNotFoundMessage = string.Empty;
        private static readonly string _invalidLoginMessage = string.Empty;
        private static readonly string _invalidEmailMessage = string.Empty;
        private static readonly string _invalidPasswordMessage = string.Empty;
        private static readonly string _invalidNameMessage = string.Empty;
        private static readonly string _defaultMessage  = string.Empty;
        private static readonly string _incorrectPasswordMessage = string.Empty;
        private static readonly string _invalidLanguageSelected = string.Empty;
        static FromNotifyTypeToMessage()
        {
            _defaultMessage = "Invalid operation!";
            _serverRequestTimeoutMessage = "The request to the server timed out. Please check your internet connection and try again later";
            _notUniqueEmailMessage = "The email address is already registered. Please use a different email address.";
            _notUniqueLoginMessage = "The login is already in use. Please choose a different login";
            _userNotFoundMessage = "No user found with the provided login or email. Please check your input.";
            _invalidLoginMessage = "Please enter a username that is 3 to 16 characters long and contains only letters, numbers, and underscores.";
            _invalidPasswordMessage = "Please enter a password that is 8 to 32 characters long, includes at least one uppercase letter and one number, and contains only letters, numbers, and underscores.";
            _invalidEmailMessage = "Please enter a valid email address, e.g., 'example@domain.com', with up to 24 characters before the '@' symbol and up to 10 characters for the domain.";
            _invalidNameMessage = "Please enter a name with 2 to 32 characters, starting with a capital letter.";
            _incorrectPasswordMessage = "The password you entered is incorrect. Please try again.";
            _invalidLanguageSelected = "Please switch to the English keyboard layout";





        }
        public static string Convert(NotifyType notifyType)
        {
            switch (notifyType)
            {
                case NotifyType.InvalidName:
                    {
                        return _invalidNameMessage;
                    }
                case NotifyType.InvalidEmail:
                    {
                        return _invalidEmailMessage;
                    }
                case NotifyType.InvalidLogin:
                    {
                        return _invalidLoginMessage;
                    }
                    case NotifyType.InvalidPassword:
                    {
                        return _invalidPasswordMessage;
                    }
                    case NotifyType.UserNotFoundByLoginOrEmail:
                    {
                        return _userNotFoundMessage;
                    }
                case NotifyType.NotUniqueLogin:
                    {
                        return _notUniqueLoginMessage;
                    }
                case NotifyType.NotUniqueEmail:
                    {
                        return _notUniqueEmailMessage;
                    }
                case NotifyType.ServerRequestTimeout:
                    {
                        return _serverRequestTimeoutMessage;
                    }
                case NotifyType.IncorrectPassword:
                    {
                        return _incorrectPasswordMessage;
                    }
                case NotifyType.InvalidLanguageSelected:
                    {
                        return _invalidLanguageSelected;
                    }
                default:
                    {
                        return _defaultMessage;
                    }
            }
        }

    }
}
