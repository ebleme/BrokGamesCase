using UnityEngine;

namespace UnityUtils {

    public static class ConsoleExtensions {

        public static string Bold(this string str) {
#if UNITY_EDITOR
            return "<b>" + str + "</b>";
#else
            return str;
#endif
        }

        public static string Color(this string str, string clr) {
#if UNITY_EDITOR
            return string.Format("<color={0}>{1}</color>", clr, str);
#else
            return str;
#endif
        }

        public static string Italic(this string str) {
#if UNITY_EDITOR
            return "<i>" + str + "</i>";
#else
            return str;
#endif
        }

        public static string Size(this string str, int size) {
#if UNITY_EDITOR
            return $"<size={size}>{str}</size>";
#else
            return str;
#endif
        }

        /// <summary>
        /// Format text with style and color - editor only
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="logFont"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string FormatText(this string txt, FontStyle logFont = FontStyle.Normal, Color? color = null) {
#if !UNITY_EDITOR
             return txt;
#else
            var hexColor = color.HasValue ? color.Value.ConvertToHexColorString() : string.Empty;
            switch (logFont) {
                case FontStyle.Normal:
                    if (!color.HasValue) {
                        return txt;
                    }
                    return txt.Color(hexColor);

                case FontStyle.Bold:
                    if (!color.HasValue) {
                        return txt.Bold();
                    }
                    return txt.Bold().Color(hexColor);

                case FontStyle.Italic:
                    if (!color.HasValue) {
                        return txt.Italic();
                    }
                    return txt.Italic().Color(hexColor);

                case FontStyle.BoldAndItalic:
                    if (!color.HasValue) {
                        return txt.Bold().Italic();
                    }
                    return txt.Bold().Italic().Color(hexColor);

                default:
                    return txt;
            }
#endif
        }
    }
}
