using System;

namespace Ebleme.SaveSystem {
    public class PlayerPrefsSaveLoadHandler : ISaveLoadHandler {
        public string SaveKey => "save";
        public DateTime NextSaveTime { get; private set; }
        public bool IsSaving { get; private set; }

        private readonly float _saveInterval;

        public PlayerPrefsSaveLoadHandler(float saveInterval = -1f) {
            _saveInterval = saveInterval;
            SetNextSaveDate();
        }

        public bool CanSave() {
            if (_saveInterval < 0) {
                return true;
            }
            return _saveInterval > 0 && DateTime.Now >= NextSaveTime;
        }

        public void Save(string saveDataJson) {
            if (IsSaving) {
                return;
            }

            IsSaving = true;

            if (string.IsNullOrEmpty(saveDataJson)) return;
#if UNITY_EDITOR
            PlayerPrefsSaveManager.Save(SaveKey, saveDataJson);

#else
            string saveDataEncrypted = DESCrypto.Encrypt(saveDataJson);
            PlayerPrefsSaveManager.Save(SaveKey, saveDataEncrypted);
#endif
            SetNextSaveDate();
            IsSaving = false;
        }


        public string Load() {
            if (!PlayerPrefsSaveManager.Exists(SaveKey)) {
                return null;
            }

            string data = PlayerPrefsSaveManager.LoadString(SaveKey);
#if UNITY_EDITOR

            return data;
#else
            return DESCrypto.Decrypt(data);
#endif
        }

        public bool SaveExists() {
            return PlayerPrefsSaveManager.Exists(SaveKey);
        }

        public void Delete() {
            PlayerPrefsSaveManager.Delete(SaveKey);
        }

        public void SetNextSaveDate() {
            if (_saveInterval > 0) {
                NextSaveTime = DateTime.Now.AddSeconds(_saveInterval);
            }
        }
    }
}