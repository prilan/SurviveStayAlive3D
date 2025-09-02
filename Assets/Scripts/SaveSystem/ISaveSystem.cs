using SaveState;

namespace SaveSystem
{
    public interface ISaveSystem
    {
        void SaveDataState(SaveDataState saveData, int slot);
        SaveDataState LoadDataState(int slot);
        bool HasSaveDataState(int slot);
    }
}
