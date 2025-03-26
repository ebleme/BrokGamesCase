using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Ebleme.SaveSystem {
    public class AutoSave {
        [JsonProperty] public string ScenarioId { get; set; }
        [JsonProperty] public GameObject Snapshot { get; set; }
        [JsonProperty] public DateTime Timestamp { get; set; }

        public const int MaxAutoSaves = 3;
        public const float AutoSaveSecondsInterval = 60f;

        public AutoSave(string scenarioId, GameObject snapshot, DateTime timestamp) {
            ScenarioId = scenarioId;
            Snapshot = snapshot;
            Timestamp = timestamp;
        }

        // public string GetAutoSavedDate() {
        //     return new LocalizedString(Constants.PopupsLocalizationTableName, "DateTimeText") {
        //         {
        //             "DateTime", new StringVariable() { Value = Timestamp.ToString("F", LocalizationSettings.SelectedLocale.Identifier.CultureInfo) }
        //         }
        //     }.GetLocalizedString();
        // }
    }
}