using System;
using System.Globalization;
using UnityEngine;

namespace UnityUtils {

    public static class NumericExtensions {

        public static Quaternion ToQuaternionRotation(this Vector3 eulerRotation) {
            return (eulerRotation.sqrMagnitude < Mathf.Epsilon) ? Quaternion.identity : Quaternion.LookRotation(eulerRotation);
        }

        public static Quaternion FaceTarget(this Transform currentTransform, Transform target) {
            var lookPos = target.position - currentTransform.position;
            lookPos.y = 0;
            return lookPos.ToQuaternionRotation();
        }

        public static string ConvertToTimeStringHMFormat(this uint seconds) {
            int hours = Mathf.FloorToInt(seconds / 3600F);
            int min = Mathf.FloorToInt((seconds - hours * 3600) / 60F);
            return string.Format("{0:00}:{1:00}", hours, min);
        }

        public static string ConvertToTimeStringMSFormat(this uint seconds) {
            var min = Mathf.FloorToInt(seconds / 60F);
            var sec = Mathf.FloorToInt(seconds - min * 60);
            return string.Format("{0:00}:{1:00}", min, sec);
        }

        public static string ConvertToTimeStringMSMSFormat(this float seconds) {
            var min = Mathf.FloorToInt(seconds / 60F);
            var sec = Mathf.FloorToInt(seconds - min * 60);
            var milliSec = Mathf.FloorToInt(seconds * 1000) % 1000;
            return string.Format("{0:00}:{1:00}:{2:000}", min, sec, milliSec);
        }

        public static string ConvertToTimeStringHMSFormat(this uint seconds) {
            int hours = Mathf.FloorToInt(seconds / 3600F);
            int min = Mathf.FloorToInt((seconds - hours * 3600) / 60F);
            int sec = Mathf.FloorToInt(seconds - hours * 3600 - min * 60);
            return string.Format("{0:00}:{1:00}:{2:00}", hours, min, sec);
        }

        public static string ConvertToTimeStringMSFormat(this float seconds) {
            var min = Mathf.FloorToInt(seconds / 60F);
            var sec = Mathf.FloorToInt(seconds - min * 60);
            return string.Format("{0:00}:{1:00}", min, sec);
        }

        public static string ConvertToThousandsFormat<T>(this T number) where T : struct, IFormattable {
            var numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            numberFormatInfo.NumberGroupSeparator = " ";
            return number.ToString("#,0", numberFormatInfo);
        }

        public static string ConvertToKilometers(this double meters) {
            if (meters >= 0) {
                return Math.Ceiling(meters / 1000d).ToString("N0");
            }
            return "N/A";
        }

        public static int ToPercent(this float value) {
            return (int)(value * 100);
        }
    }
}
