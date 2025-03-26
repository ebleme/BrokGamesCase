using System;

namespace Ebleme.SaveSystem {
    public interface ISaveLoadHandler {
        string SaveKey { get; }
        DateTime NextSaveTime { get; }
        bool IsSaving { get; }
        bool CanSave();
        void Save(string jsonData);
        string Load();
        bool SaveExists();
        void Delete();
        void SetNextSaveDate();
    }
}