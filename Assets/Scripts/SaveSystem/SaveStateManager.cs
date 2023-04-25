using SaveState;
using UnityEngine;
using Utility;

namespace SaveSystem
{
    public class SaveStateManager : AbstractSingleton<SaveStateManager>
    {
        public ISaveSystem SaveSystem => SaveSystemManager.SaveSystem;

        public SaveSystemManager SaveSystemManager { get; private set; } = new SaveSystemManager(new PlayerPrefsSaveSystem());

        public void ChangeSaveSystem(ISaveSystem saveSystem)
        {
            SaveSystemManager = new SaveSystemManager(saveSystem);
        }
    }
}
