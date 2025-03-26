using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ebleme.SaveSystem {
    public class SaveData {
        // [JsonProperty] public BranchDto Branch { get; set; }
        // [JsonProperty] public Enums.CarTypes LastUsedCarType { get; set; }
        [JsonProperty] public List<string> CompletedTutorials { get; set; }
        // [JsonProperty] public string SelectedLanguageCode { get; set; }
        [JsonProperty] public string SaveVersion { get; set; }

        public SaveData(string version) {
            CompletedTutorials = new List<string>();

            SaveVersion = version;
        }
        
        public void MarkTutorialAsCompleted(string tutorialId) {
            if (!CompletedTutorials.Contains(tutorialId)) {
                CompletedTutorials.Add(tutorialId);
            }
        }

        public bool IsTutorialCompleted(string tutorialId) {
            return CompletedTutorials.Contains(tutorialId);
        }
    }
}