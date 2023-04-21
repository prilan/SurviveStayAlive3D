using Enemies;
using Factories;
using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

namespace SurviveStayAlive
{
    public class AppModel : Singleton<AppModel>
    {
        private Dictionary<int, Player> playerDictionary = new Dictionary<int, Player>();
        private Dictionary<int, Enemy> enemyDictionary = new Dictionary<int, Enemy>();

        public Dictionary<int, Player> PlayerDictionary => playerDictionary;
        public Dictionary<int, Enemy> EnemyDictionary => enemyDictionary;

        public LogicState LogicState;

        public void Init(LogicState logicState)
        {
            LogicState = logicState;
        }

        public void CreatePlayers()
        {
            playerDictionary.Clear();

            for (int index = 0; index < GameManager.Instance.PlayerCount; index++) {
                Player player = new BasePlayer(new BasePlayerFactory());
                playerDictionary.Add(index, player);
            }
        }

        public void CreateEnemies()
        {
            enemyDictionary.Clear();

            int enemyCount = GameManager.Instance.EnemyCount;
            int enemyIndex = 0;

            Enemy bossEnemy = new Enemy(new BossEnemyFactory());
            enemyDictionary.Add(enemyIndex, bossEnemy);
            enemyIndex++;

            Enemy fastEnemy = new Enemy(new FastEnemyFactory());
            enemyDictionary.Add(enemyIndex, fastEnemy);
            enemyIndex++;

            if (enemyCount < 2)
                return;

            for (int index = enemyDictionary.Count; index < GameManager.Instance.EnemyCount; index++) {
                Enemy enemy = new Enemy(new DistantEnemyFactory());
                enemyDictionary.Add(index, enemy);
            }
        }

        public Player GetPlayerByIndex(int index)
        {
            Player player = playerDictionary[index];
            return player;
        }

        public Enemy GetEnemyByIndex(int index)
        {
            Enemy enemy = enemyDictionary[index];
            return enemy;
        }

        public void RemovePlayerFromActivePlayers(Player player)
        {
            var playerItem = playerDictionary.First(playerPair => playerPair.Value == player);

            playerDictionary.Remove(playerItem.Key);
        }
    }
}
