using System;
using System.Collections.Generic;

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
