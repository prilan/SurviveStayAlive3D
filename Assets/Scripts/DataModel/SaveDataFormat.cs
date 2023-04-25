using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    [Serializable]
    public class SaveDataFormat
    {
        public int currentLevelIndex;
        public LevelFormat currentLevel;
        public List<PlayerFormat> players;
        public List<EnemyFormat> enemies;
    }
}
