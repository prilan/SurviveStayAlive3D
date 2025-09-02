using Utility;

namespace SaveSystem
{
    public class SaveStateManager : AbstractSingleton<SaveStateManager>
    {
        public ISaveSystem SaveSystem => SaveSystemManager.SaveSystem;

        private SaveSystemManager SaveSystemManager { get; set; } = new SaveSystemManager(new PlayerPrefsSaveSystem());

        public void ChangeSaveSystem(ISaveSystem saveSystem)
        {
            SaveSystemManager = new SaveSystemManager(saveSystem);
        }
    }
}
