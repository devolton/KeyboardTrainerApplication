using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Enums
{
    /// <summary>
    /// Type of notification message
    /// </summary>
    public enum NotifyType
    {
        NotUniqueLogin,
        NotUniqueEmail,
        UserNotFoundByLoginOrEmail,
        IncorrectPassword,
        InvalidPassword,
        InvalidLogin,
        InvalidEmail,
        InvalidName,
        ServerRequestTimeout,
        ServerOk,
        InvalidLanguageSelected

    }
}
