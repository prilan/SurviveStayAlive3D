using SaveState;
using SaveSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SaveSystem
{
    public class JsonSaveSystem : ISaveSystem
    {
        private string GetFleName(int slot)
        {
            return "save_" + slot;
        }

        private string GetSavePath(int slot)
        {
            return Path.Combine(Application.dataPath, "Resources", GetLocalSavePath(slot));
        }

        private string GetLocalSavePath(int slot)
        {
            return Path.Combine(GetFleName(slot) + ".json");
        }

        public void SaveDataState(SaveDataState saveData, int slot)
        {
            File.WriteAllText(GetSavePath(slot), saveData.ToSaveGame());
        }

        public SaveDataState LoadDataState(int slot)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(GetFleName(slot));
            SaveDataState saveDataState = JsonUtility.FromJson<SaveDataState>(textAsset.text);

            return saveDataState;
        }

        public bool HasSaveDataState(int slot)
        {
            return File.Exists(GetSavePath(slot));
        }
    }
}
