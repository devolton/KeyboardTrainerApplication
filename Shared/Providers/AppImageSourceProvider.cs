using CourseProjectKeyboardApplication.ApiClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CourseProjectKeyboardApplication.Shared.Providers
{
    public static class AppImageSourceProvider
    {
        private static StaticImageApiClient _staticImageApiClient = ContentApiClientProvider.StaticImageApiClient;
        public static ImageSource KeyboardAppIconImageSource { get; private set; }
        public static ImageSource LockIconImageSource { get; private set; }
        public static ImageSource StudyIconImageSource { get; private set; }
        public static ImageSource RepeatIconImageSource { get; private set; }
        public static ImageSource DevoltonLabsImageSource { get; private set; }
        public static ImageSource EditProfileIconImageSource { get; private set; }
        public static ImageSource CertificateIconImageSource { get; private set; }
        public static ImageSource KeyboardSchemeEngImageSource { get; private set; }
        public static ImageSource StartLeftEngPositionImageSource { get; private set; }
        public static ImageSource StartRightEngPositionImageSource { get; private set; }
        public static ImageSource LeftArrowCurrentLessonImageSource { get; private set; }
        public static ImageSource StatisticIconImageSource { get; private set; }
        public static ImageSource GoldStarImageSource { get; private set; }
        public static ImageSource LightGrayStarImageSource { get; private set; }
        public static ImageSource GoldTargetImageSource { get; private set; }
        public static ImageSource LightGrayTargetImageSource { get; private set; }
        public static ImageSource GoldFlashImageSource { get; private set; }
        public static ImageSource LightGrayFlashImageSource { get; private set; }
        public static ImageSource SilverFlashImageSource { get; private set; }
        public static ImageSource SilverTargetImageSource { get; private set; }
        public static ImageSource SilverCalendarImageSource { get; private set; }
        public static ImageSource GoldMedalImageSource { get; private set; }
        public static ImageSource PlatinumMedalImageSource { get; private set; }
        public static ImageSource SilverMedalImageSource { get; private set; }
        public static ImageSource BlueFlashImageSource { get; private set; }
        public static ImageSource BlueStarImageSource { get; private set; }
        public static ImageSource BlueTargetImageSource { get; private set; }
        public static ImageSource KeyboardIconImageSource { get; private set; }
        public static ImageSource DefaultUserAvatarImageSource { get; private set; }
        public static ImageSource InfoIconImageSource { get; set; }
        public static async Task Init()
        {
            KeyboardAppIconImageSource ??= await _staticImageApiClient.GetImageSourceAsync("KeyboardApplicationLogo.png");
            KeyboardIconImageSource ??= await _staticImageApiClient.GetImageSourceAsync("KeyboardIcon.png");
            BlueFlashImageSource ??= await _staticImageApiClient.GetImageSourceAsync("BlueFlash.png");
            BlueStarImageSource ??= await _staticImageApiClient.GetImageSourceAsync("BlueStar.png");
            BlueTargetImageSource ??= await _staticImageApiClient.GetImageSourceAsync("BlueTarget.png");
            LockIconImageSource ??= await _staticImageApiClient.GetImageSourceAsync("LessonLockIcon.png");
            StudyIconImageSource ??= await _staticImageApiClient.GetImageSourceAsync("StudyIcon.png");
            RepeatIconImageSource ??= await _staticImageApiClient.GetImageSourceAsync("RepeatIcon.png");
            DevoltonLabsImageSource ??= await _staticImageApiClient.GetImageSourceAsync("DevoltonLabsLogo.png");
            EditProfileIconImageSource ??= await _staticImageApiClient.GetImageSourceAsync("EditProfileIcon.png");
            CertificateIconImageSource ??= await _staticImageApiClient.GetImageSourceAsync("CertificateIcon.png");
            KeyboardSchemeEngImageSource ??= await _staticImageApiClient.GetImageSourceAsync("KeyboardSchemeEng.png");
            StartLeftEngPositionImageSource ??= await _staticImageApiClient.GetImageSourceAsync("StartLeftEng.png");
            StartRightEngPositionImageSource ??= await _staticImageApiClient.GetImageSourceAsync("StartRightEng.png");
            LeftArrowCurrentLessonImageSource ??= await _staticImageApiClient.GetImageSourceAsync("LeftArrow.png");
            StatisticIconImageSource ??= await _staticImageApiClient.GetImageSourceAsync("StatisticIcon.png");
            GoldStarImageSource ??= await _staticImageApiClient.GetImageSourceAsync("GoldStar.png");
            LightGrayStarImageSource ??= await _staticImageApiClient.GetImageSourceAsync("LightGrayStar.png");
            GoldTargetImageSource ??= await _staticImageApiClient.GetImageSourceAsync("GoldTarget.png");
            LightGrayTargetImageSource ??= await _staticImageApiClient.GetImageSourceAsync("LightGrayTarget.png");
            GoldFlashImageSource ??= await _staticImageApiClient.GetImageSourceAsync("GoldFlash.png");
            LightGrayFlashImageSource ??= await _staticImageApiClient.GetImageSourceAsync("LightGrayFlash.png");
            SilverCalendarImageSource ??= await _staticImageApiClient.GetImageSourceAsync("SilverCalendar.png");
            SilverFlashImageSource ??= await _staticImageApiClient.GetImageSourceAsync("SilverFlash.png");
            SilverTargetImageSource ??= await _staticImageApiClient.GetImageSourceAsync("SilverTarget.png");
            GoldMedalImageSource ??= await _staticImageApiClient.GetImageSourceAsync("GoldMedal.png");
            PlatinumMedalImageSource ??= await _staticImageApiClient.GetImageSourceAsync("PlatinumMedal.png");
            SilverMedalImageSource ??= await _staticImageApiClient.GetImageSourceAsync("SilverMedal.png");
            DefaultUserAvatarImageSource ??= await _staticImageApiClient.GetImageSourceAsync("DefaultUserAvatar.png");
            InfoIconImageSource ??= await _staticImageApiClient.GetImageSourceAsync("InfoIcon.png");
 
            



        }
    }
}
