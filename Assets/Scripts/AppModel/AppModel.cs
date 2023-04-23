using DataModel;
using Enemies;
using Factories;
using Players;
using SaveState;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Utility;

namespace SurviveStayAlive
{
    public class AppModel : AbstractSingleton<AppModel>
    {
        public GameConfiguration GameConfiguration;
        public LevelsListFormat levelsListConfig;

        public LevelFormat CurrentLevel;
        public int CurrentLevelIndex;

        public SaveDataState SaveDataState = new SaveDataState();

        private Dictionary<int, Player> playerDictionary = new Dictionary<int, Player>();
        private Dictionary<int, AbstractEnemy> enemyDictionary = new Dictionary<int, AbstractEnemy>();

        public Dictionary<int, Player> PlayerDictionary => playerDictionary;
        public Dictionary<int, AbstractEnemy> EnemyDictionary => enemyDictionary;

        public LogicState LogicState;

        public void Init(LogicState logicState)
        {
            LogicState = logicState;
        }

        public void CreatePlayers()
        {
            playerDictionary.Clear();

            for (int index = 0; index < AppModel.Instance.CurrentLevel.playerCount; index++) {
                Player player = new BasePlayer(new BasePlayerFactory());
                playerDictionary.Add(index, player);
            }
        }

        public void CreateEnemies()
        {
            enemyDictionary.Clear();

            int enemyCount = AppModel.Instance.CurrentLevel.enemyCount;
            int enemyIndex = 0;

            // Один враг двигающийся по своей траектории
            MovableEnemy movableEnemy = new MovableEnemy(new MovableEnemyFactory());
            enemyDictionary.Add(enemyIndex, movableEnemy);
            enemyIndex++;

            // Один враг, преследующий объект игрока
            StalkingEnemy stalkingEnemy = new StalkingEnemy(new StalkingEnemyFactory());
            enemyDictionary.Add(enemyIndex, stalkingEnemy);
            enemyIndex++;

            if (enemyCount <= 2)
                return;

            // Если врагов более 2, то все остальные враги стреляющие
            for (int index = enemyDictionary.Count; index < enemyCount; index++) {
                ShootingEnemy shootingEnemy = new ShootingEnemy(new ShootingEnemyFactory());
                enemyDictionary.Add(index, shootingEnemy);
            }
        }

        public Player GetPlayerByIndex(int index)
        {
            Player player = playerDictionary[index];
            return player;
        }

        public AbstractEnemy GetEnemyByIndex(int index)
        {
            AbstractEnemy enemy = enemyDictionary[index];
            return enemy;
        }

        public void RemovePlayerFromActivePlayers(Player player)
        {
            var playerItem = playerDictionary.First(playerPair => playerPair.Value == player);

            playerDictionary.Remove(playerItem.Key);
        }

        public void LoadLevelsConfig()
        {
            TextAsset textAsset = Resources.Load<TextAsset>("config");
            if (textAsset == null)
                return;

            levelsListConfig = JsonUtility.FromJson<LevelsListFormat>(textAsset.text);

            if (levelsListConfig != null && levelsListConfig.levels != null && levelsListConfig.levels.Any()) {
                CurrentLevelIndex = 0;
                CurrentLevel = levelsListConfig.levels[CurrentLevelIndex];

                SaveDataState.SaveData.currentLevel = CurrentLevel;
                
            }
        }

        public void UpdateDataState()
        {
            SaveDataState.SaveData.currentLevel = CurrentLevel;

            if (SaveDataState.SaveData.players == null || !SaveDataState.SaveData.players.Any()) {
                SaveDataState.SaveData.players = Enumerable.Repeat(new PlayerFormat(), CurrentLevel.playerCount).ToList();
            }

            for (int index = 0; index < CurrentLevel.playerCount; index++) {
                if ((index >= PlayersManager.Instance.Players.Count) || (index >= SaveDataState.SaveData.players.Count))
                    continue;

                PlayerController playerController = PlayersManager.Instance.Players.Values.ElementAt(index);

                SaveDataState.SaveData.players[index].health = playerController.Player.Health;
                SaveDataState.SaveData.players[index].position = playerController.transform.position;
            }
        }
    }
}
