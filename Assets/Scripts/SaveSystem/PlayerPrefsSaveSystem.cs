using SaveState;
using SaveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SaveSystem
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        private const string SAVE_DATA_KEY = "SAVE_DATA_KEY_";

        public void SaveDataState(SaveDataState saveData, int slot)
        {
            PlayerPrefs.SetString(SAVE_DATA_KEY + slot, saveData.ToSaveGame());
        }

        public SaveDataState LoadDataState(int slot)
        {
            string dataString = PlayerPrefs.GetString(SAVE_DATA_KEY + slot);
            SaveDataState saveDataState = new SaveDataState();
            saveDataState = saveDataState.FromSaveGame(dataString);

            return saveDataState;
        }

        public bool HasSaveDataState(int slot)
        {
            return PlayerPrefs.HasKey(SAVE_DATA_KEY + slot);
        }
    }
}
