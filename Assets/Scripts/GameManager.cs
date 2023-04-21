using System.Collections;
using UnityEngine;
using Utility;

namespace SurviveStayAlive
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [Range(2, 5)]
        [SerializeField] int playerCount;

        [Range(2, 10)]
        [SerializeField] int enemyCount;

        public int PlayerCount
        {
            get
            {
                return playerCount;
            }
        }

        public int EnemyCount
        {
            get
            {
                return enemyCount;
            }
        }
    }
}