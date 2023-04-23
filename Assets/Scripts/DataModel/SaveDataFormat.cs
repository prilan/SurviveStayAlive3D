using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    [Serializable]
    public class SaveDataFormat
    {
        public LevelFormat currentLevel;
        public List<PlayerFormat> players;
    }
}
