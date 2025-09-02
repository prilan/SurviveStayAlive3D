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
using Factories.EnemyFactories;
using Factories.PlayerFactories;
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
            MovingRoundEnemy movableEnemy = new MovingRoundEnemy(new MovableEnemyFactory());
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

        public void RemovePlayersAndEnemies()
        {
            playerDictionary.Clear();
            enemyDictionary.Clear();
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

        public void LoadNextLevel()
        {
            if (CurrentLevelIndex + 1 < levelsListConfig.levels.Count)
                CurrentLevelIndex++;
            else
                CurrentLevelIndex = 0;

            CurrentLevel = levelsListConfig.levels[CurrentLevelIndex];
        }

        public void UpdateDataState()
        {
            SaveDataState.SaveData.currentLevelIndex = CurrentLevelIndex;
            SaveDataState.SaveData.currentLevel = CurrentLevel;

            SaveDataState.SaveData.players = new List<PlayerFormat>();

            for (int index = 0; index < CurrentLevel.playerCount; index++) {
                if (index >= PlayersManager.Instance.Players.Count)
                    continue;

                PlayerController playerController = PlayersManager.Instance.Players.Values.ElementAt(index);

                PlayerFormat player = new PlayerFormat();

                player.health = playerController.Player.Health;
                player.position = playerController.transform.position;

                SaveDataState.SaveData.players.Add(player);
            }

            SaveDataState.SaveData.enemies = new List<EnemyFormat>();

            for (int index = 0; index < CurrentLevel.enemyCount; index++) {
                if (index >= EnemiesManager.Instance.Enemies.Count)
                    continue;

                EnemyController enemyController = EnemiesManager.Instance.Enemies.Values.ElementAt(index);

                EnemyFormat enemy = new EnemyFormat();

                enemy.enemyType = enemyController.Enemy.EnemyType.ToString();
                enemy.position = enemyController.StartPosition;

                SaveDataState.SaveData.enemies.Add(enemy);
            }

            if (SaveDataState.SaveData.currentLevel.playerCount > SaveDataState.SaveData.players.Count)
                SaveDataState.SaveData.currentLevel.playerCount = SaveDataState.SaveData.players.Count;

            if (SaveDataState.SaveData.currentLevel.enemyCount > SaveDataState.SaveData.enemies.Count)
                SaveDataState.SaveData.currentLevel.enemyCount = SaveDataState.SaveData.enemies.Count;
        }

        public void LoadSavedState()
        {
            CurrentLevelIndex = SaveDataState.SaveData.currentLevelIndex;
            CurrentLevel = SaveDataState.SaveData.currentLevel;
        }
    }
}
