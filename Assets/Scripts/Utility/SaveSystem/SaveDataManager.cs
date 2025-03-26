using Ebleme.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ebleme.SaveSystem {
    public class SaveDataManager : SerializedSingleton<SaveDataManager> {
        [SerializeField] private bool usePlayerPrefs;
        private static SaveData saveData;

        private static readonly JsonSerializerSettings SerializationSettings = new() {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new DefaultContractResolver {
                NamingStrategy = new SnakeCaseNamingStrategy {
                    OverrideSpecifiedNames = true
                }
            }
        };

        private static ISaveLoadHandler saveLoadHandler;

        [ShowInInspector, LabelText("Save Version")]
        public const string Version = "v1.0";

        public static SaveData SaveData {
            get {
                if (saveData == null) {
                    LoadSave();
                }

                return saveData;
            }
        }

        #region Initialization

        protected override void Awake() {
            DontDestroyOnLoad(this);
            Init();
            base.Awake();
        }

        private void Init() {
            if (PlayerPrefs.GetString(Constants.PlayerPrefsVersionKey) != Version)
            {
                Debug.Log("Removing all PlayerPrefs.");
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetString(Constants.PlayerPrefsVersionKey, Version);
            }
            
            if (!usePlayerPrefs) {
                saveLoadHandler = new FileSaveLoadHandler();
                (saveLoadHandler as FileSaveLoadHandler)?.CreateSaveDirectory();
            }
            else {
                saveLoadHandler = new PlayerPrefsSaveLoadHandler();
            }

            Save();
        }

        private void Start() {
            LoadSave();
        }

        public static void Save() {
            if (saveData == null) {
                return;
            }

            var saveDataJson = JsonConvert.SerializeObject(saveData, SerializationSettings);
            saveLoadHandler.Save(saveDataJson);
        }

        private static void LoadSave() {
            var json = saveLoadHandler.Load();
            if (string.IsNullOrEmpty(json)) {
                saveData = new SaveData(Version);
                return;
            }

            saveData = JsonConvert.DeserializeObject<SaveData>(json, SerializationSettings);
        }

        private void OnApplicationPause(bool pause) {
            if (pause) {
                Save();
            }
        }

        protected override void OnDestroy() {
            Save();
            base.OnDestroy();
        }

        #endregion
        
        public static bool IsTutorialCompleted(string tutorialId) {
            return SaveData.IsTutorialCompleted(tutorialId);
        }

        public static void MarkTutorialAsCompleted(string tutorialId) {
            SaveData.MarkTutorialAsCompleted(tutorialId);
            Save();
        }
    }
}