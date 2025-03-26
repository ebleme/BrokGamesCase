using System;
using System.IO;
using UnityEngine;

namespace Ebleme.SaveSystem
{
    public class FileSaveLoadHandler : ISaveLoadHandler
    {
        public string SaveKey { get; }
        public DateTime NextSaveTime { get; private set; }
        public bool IsSaving { get; private set; }

        private readonly float _saveInterval;
        private readonly string _saveFolderPath;

        private const string FileName = "data.json";
        private const string SaveFolderName = "Saves";

        public FileSaveLoadHandler(float saveInterval = -1f)
        {
            _saveInterval = saveInterval;
            _saveFolderPath = Path.Combine(Application.persistentDataPath, SaveFolderName);
            SaveKey = Path.Combine(_saveFolderPath, FileName);
            SetNextSaveDate();
        }

        public void CreateSaveDirectory()
        {
            if (!Directory.Exists(_saveFolderPath))
            {
                Directory.CreateDirectory(_saveFolderPath);
            }
        }

        public bool CanSave()
        {
            if (_saveInterval < 0f)
            {
                return true;
            }

            return _saveInterval > 0 && DateTime.Now >= NextSaveTime;
        }

        public void Delete()
        {
            if (File.Exists(SaveKey))
            {
                File.Delete(SaveKey);
                return;
            }

            Debug.LogWarningFormat("Could not delete save file because it does not exist: {0}", SaveKey);
        }

        public string Load()
        {
            if (!SaveExists()) return null;
            string textData = File.ReadAllText(SaveKey);
#if UNITY_EDITOR

            return textData;
#else
            return DESCrypto.Decrypt(textData);
#endif
        }

        public void Save(string saveDataJson)
        {
            if (IsSaving)
            {
                return;
            }

            IsSaving = true;
            try
            {
                if (string.IsNullOrEmpty(saveDataJson)) return;
#if UNITY_EDITOR
                File.WriteAllText(SaveKey, saveDataJson);

#else
                    string saveDataEncrypted = DESCrypto.Encrypt(saveDataJson);
                    File.WriteAllText(SaveKey, saveDataEncrypted);
#endif
                SetNextSaveDate();
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An exception occured while saving {0}. Message {1}", SaveKey, ex.Message);
            }

            IsSaving = false;
        }

        public void SetNextSaveDate()
        {
            if (_saveInterval > 0)
            {
                NextSaveTime = DateTime.Now.AddSeconds(_saveInterval);
            }
        }

        public bool SaveExists()
        {
            return File.Exists(SaveKey);
        }
    }
}