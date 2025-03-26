using System;
using Ebleme.Utility;
using UnityEngine;

namespace UnityUtils {
    public static class HelperExtensions {
        private const string GuidDefaultFormat = "D";
        private const string GuidOnlyDigitsFormat = "N";

        public static string GenerateDefaultGuid() {
            return Guid.NewGuid().ToString(GuidDefaultFormat);
        }

        public static string GenerateOnlyDigitsGuid() {
            return Guid.NewGuid().ToString(GuidOnlyDigitsFormat);
        }
        
        public static string GenerateUsername(string guid, string baseName = Constants.DefaultBaseUsername) {
            return $"{baseName}{guid[..8]}";
        }

        public static string ConvertToHexColorString(this Color color) => $"#{ColorUtility.ToHtmlStringRGBA(color)}";
    }
}