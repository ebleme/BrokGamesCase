using UnityEngine;

namespace Ebleme.Utility {
    public static class Constants
    {
        public const string PlayerPrefsVersionKey = "PlayerPrefsVersion";
        
        // public const string UsernameKey = "Username";
        // public const string UserFullNameKey = "UserFullName";

        public const string APIBaseURLControllerFormat = "{0}/{1}";
        public const string APIBaseURLControllerActionFormat = "{0}/{1}{{2}";

        
        public const string TimeTextHMSFormat = @"hh\:mm\:ss";
        public const string TimeTextMSFormat = @"mm\:ss";
        public const string TimeTextMinutesFormat = @"%m";
        public const string LessThanMinuteTimeText = "< 1 min";

        public const float OneTenthOfSecond = 0.1f;
        public const float QuarterOfSecond = 0.25f;
        public const float HalfSecond = 0.5f;
        public const float OneSecond = 1f;
        public const float TwoSeconds = 2f;
        public const float ThreeSeconds = 3f;
        public const float FiveSeconds = 5f;
        public const float TenSeconds = 10f;
        public const float TwentySeconds = 20f;
        public const float TwentyFiveSeconds = 25f;
        public const float ThirtySeconds = 30f;

        public const float MaxEulerAnglesRotation = 360f;

        public static readonly WaitForSeconds FiveHundredthsOfSecondInterval = new WaitForSeconds(0.05f);
        public static readonly WaitForSeconds OneTenthOfSecondInterval = new WaitForSeconds(0.1f);
        public static readonly WaitForSeconds QuarterOfSecondInterval = new WaitForSeconds(0.25f);
        public static readonly WaitForSeconds HalfSecondInterval = new WaitForSeconds(0.5f);
        public static readonly WaitForSeconds OneSecondInterval = new WaitForSeconds(1f);
        public static readonly WaitForSeconds TwoSecondsInterval = new WaitForSeconds(2f);
        public static readonly WaitForSeconds ThreeSecondsInterval = new WaitForSeconds(3f);
        public static readonly WaitForSeconds FiveSecondsInterval = new WaitForSeconds(5f);
        public static readonly WaitForSeconds ThirtySecondsInterval = new WaitForSeconds(30f);

        public const string UnknownName = "Unknown";

        public const string GpsAccuracyKey = "GPSAccuracy";
        public const string GpsUpdateDistanceKey = "GPSUpdateDistance";

        public const float ClickThreshold = 10f;
        public const float DefaultGpsAccuracy = 5f;
        public const float DefaultGpsUpdateDistance = 1f;

        public const string WalkingModeVisitedKey = "ExplorationWalkModeVisited";

        #region Colors

        public static readonly Color TransparentWhite = new Color(1f, 1f, 1f, 0f);
        public static readonly Color SemiTransparentWhite = new Color(1f, 1f, 1f, 0.75f);
        public static readonly Color GrayOut = new Color(0.5f, 0.5f, 0.5f, 0.5019608f);

        #endregion Colors
      
        #region Text Constants

        public const string PercentageSourceText = "{0}%";
        public const string ProgressSourceText = "{0}/{1}";
        public const string ProgressColoredSourceText = "<color={2}>{0}</color>/{1}";

        #endregion Text Constants

        public const string DefaultBaseUsername = "Player";
       
    }
}