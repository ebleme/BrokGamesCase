// maebleme2

using System;
using UnityEngine;

namespace Ebleme.SaveSystem
{
     public static class PlayerPrefsSaveManager {

        public static void Delete(string key) {
            if (Exists(key)) {
                PlayerPrefs.DeleteKey(key);
            }
        }

        public static bool Exists(string key) {
            return !string.IsNullOrEmpty(key) && PlayerPrefs.HasKey(key);
        }

        public static TOutput Load<TOutput>(string key, TOutput defaultValue) {
            if (string.IsNullOrEmpty(key)) { return default; }

            return defaultValue switch {
                int intValue => (TOutput)Convert.ChangeType(PlayerPrefs.GetInt(key, intValue), typeof(TOutput)),
                long longValue => (TOutput)Convert.ChangeType(PlayerPrefsX.GetLong(key, longValue), typeof(TOutput)),
                bool boolValue => (TOutput)Convert.ChangeType(PlayerPrefsX.GetBool(key, boolValue), typeof(TOutput)),
                string stringValue => (TOutput)Convert.ChangeType(PlayerPrefs.GetString(key, stringValue), typeof(TOutput)),
                _ => throw new System.NotSupportedException(),
            };
        }
        
        public static bool LoadBool(string key, bool defaultValue) {
            return string.IsNullOrEmpty(key) ? defaultValue : PlayerPrefsX.GetBool(key, defaultValue);
        }
        
        public static long LoadLong(string key, long defaultValue) {
            return string.IsNullOrEmpty(key) ? defaultValue : PlayerPrefsX.GetLong(key, defaultValue);
        }
        
        public static int LoadInt(string key, int defaultValue) {
            return string.IsNullOrEmpty(key) ? defaultValue : PlayerPrefs.GetInt(key, defaultValue);
        }

        public static string LoadString(string key) {
            return string.IsNullOrEmpty(key) ? null : PlayerPrefs.GetString(key);
        }
        
        public static void Save<TInput>(string key, TInput value) {
            if (string.IsNullOrEmpty(key)) { return; }

            switch (value) {
                case int intValue:
                    PlayerPrefs.SetInt(key, intValue);
                    break;

                case long longValue:
                    PlayerPrefsX.SetLong(key, longValue);
                    break;

                case bool boolValue:
                    PlayerPrefsX.SetBool(key, boolValue);
                    break;

                case string stringValue:
                    PlayerPrefs.SetString(key, stringValue);
                    break;

                default:
                    throw new System.NotSupportedException();
            }
            PlayerPrefs.Save();
        }
    }
}