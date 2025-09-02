using System;
using DataModel;
using SaveSystem;

namespace SaveState
{
    [Serializable]
    public class SaveDataState : ICanSave<SaveDataState>
    {
        public SaveDataFormat SaveData = new();
    }
}
