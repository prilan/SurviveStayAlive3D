using System.IO;
using SaveState;
using UnityEngine;

namespace SaveSystem
{
    public class JsonSaveSystem : ISaveSystem
    {
        private static string GetFleName(int slot)
        {
            return "save_" + slot;
        }

        private static string GetSavePath(int slot)
        {
            return Path.Combine(Application.dataPath, "Resources", GetLocalSavePath(slot));
        }

        private static string GetLocalSavePath(int slot)
        {
            return Path.Combine(GetFleName(slot) + ".json");
        }

        public void SaveDataState(SaveDataState saveData, int slot)
        {
            File.WriteAllText(GetSavePath(slot), saveData.ToSaveGame());
        }

        public SaveDataState LoadDataState(int slot)
        {
            var textAsset = Resources.Load<TextAsset>(GetFleName(slot));
            var saveDataState = JsonUtility.FromJson<SaveDataState>(textAsset.text);

            return saveDataState;
        }

        public bool HasSaveDataState(int slot)
        {
            return File.Exists(GetSavePath(slot));
        }
    }
}
