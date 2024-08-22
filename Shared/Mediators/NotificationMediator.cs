using CourseProjectKeyboardApplication.Shared.Enums;
using CourseProjectKeyboardApplication.Tools.Converters;
using CourseProjectKeyboardApplication.View.Windows;
using CourseProjectKeyboardApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProjectKeyboardApplication.Shared.Mediators
{
    public static class NotificationMediator
    {
        private static bool _notificationWindowWorking = false;

        /// <summary>
        /// Method recieved NotifyType, show Notification window and closing it
        /// </summary>
        /// <param name="notifyType">Type of notification which we want show</param>
        /// <returns></returns>
        public static async Task ShowNotificationWindow(NotifyType notifyType)
        {
            if (_notificationWindowWorking)
                return;
            var notificationWindow = new NotificationWindow() { Notification = FromNotifyTypeToMessage.Convert(notifyType) };
            Application.Current.Dispatcher.Invoke(() =>
            {
                notificationWindow.Show();
                _notificationWindowWorking = true;
            });
            await Task.Delay(4000);
            Application.Current.Dispatcher.Invoke(() =>
            {
                notificationWindow.Close();
                _notificationWindowWorking = false;
            });
        }



    }
}
