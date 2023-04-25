using SaveState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveSystem
{
    public interface ISaveSystem
    {
        void SaveDataState(SaveDataState saveData, int slot);
        SaveDataState LoadDataState(int slot);
        bool HasSaveDataState(int slot);
    }
}
