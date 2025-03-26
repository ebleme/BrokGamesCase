using System.Collections.Generic;
using Ebleme.Models;
using Newtonsoft.Json;

namespace Ebleme.SaveSystem {
    public class SaveData {
        [JsonProperty] public List<PlayerUpgradeData> PlayerUpgrades  { get; set; }
        // [JsonProperty] public string SelectedLanguageCode { get; set; }
        [JsonProperty] public string SaveVersion { get; set; }

        public SaveData(string version)
        {
            PlayerUpgrades = new List<PlayerUpgradeData>();

            SaveVersion = version;
        }
    }
}