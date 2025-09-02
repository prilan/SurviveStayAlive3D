namespace SaveSystem
{
    public class SaveSystemManager
    {
        private readonly ISaveSystem saveSystem;
        public ISaveSystem SaveSystem => saveSystem;
        
        public SaveSystemManager(ISaveSystem saveSystem)
        {
            this.saveSystem = saveSystem;
        }
    }
}
