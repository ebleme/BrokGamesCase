using System;
using System.Linq;

namespace Ebleme.Utility {
    public static class Enums {
        
        public enum PoolType {
            Stack,
            LinkedList
        }

        // Enum extensions

        #region Extension Methods

        public static string GetPrettyEnumName<T>(T element) {
            var name = Enum.GetName(typeof(T), element)!;
            return string.Concat(name.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }

        public static string GetEnumName<T>(T element) {
            return Enum.GetName(typeof(T), element);
        }

        public static T[] GetEnumValues<T>() {
            return (T[])Enum.GetValues(typeof(T));
        }
        public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = false) where TEnum : struct, Enum
        {
            if (Enum.TryParse<TEnum>(value, ignoreCase, out var result))
                return result;
            throw new ArgumentException($"'{value}' değeri {typeof(TEnum).Name} enum türü için geçerli bir değer değildir.");
        }
        

        public static T Next<T>(this T enumValue) where T : struct, IConvertible {
            if (!typeof(T).IsEnum) throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");
            T[] enumArray = (T[])Enum.GetValues(enumValue.GetType());
            var enumValueIndex = Array.IndexOf(enumArray, enumValue) + 1;
            return (enumArray.Length == enumValueIndex) ? enumArray[0] : enumArray[enumValueIndex];
        }

        public static T Previous<T>(this T enumValue) where T : struct {
            if (!typeof(T).IsEnum) throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");
            T[] enumArray = (T[])Enum.GetValues(enumValue.GetType());
            var enumValueIndex = Array.IndexOf(enumArray, enumValue) - 1;
            return (enumValueIndex == -1) ? enumArray[^1] : enumArray[enumValueIndex];
        }

        #endregion
    }
}