using SaveState;
using UnityEngine;
using Utility;

namespace SaveSystem
{
    public class SaveStateManager : AbstractSingleton<SaveStateManager>
    {
        private const string SAVE_DATA_KEY = "SAVE_DATA_KEY";
        
        public void SaveDataState(SaveDataState saveData)
        {
            PlayerPrefs.SetString(SAVE_DATA_KEY, saveData.ToSaveGame());
        }

        public SaveDataState LoadDataState()
        {
            string dataString = PlayerPrefs.GetString(SAVE_DATA_KEY);
            SaveDataState saveDataState = new SaveDataState();
            saveDataState = saveDataState.FromSaveGame(dataString);

            return saveDataState;
        }

        public bool HasSaveDataState()
        {
            return PlayerPrefs.HasKey(SAVE_DATA_KEY);
        }
        
        /*-------------------------------------------------------*/
        
    }
}
