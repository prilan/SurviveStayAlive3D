using DataModel;
using Enemies;
using Players;
using SaveState;
using System.Collections.Generic;
using System.Linq;
using Factories.EnemyFactories;
using Factories.PlayerFactories;
using UnityEngine;
using Utility;

namespace SurviveStayAlive
{
    public class AppModel : AbstractSingleton<AppModel>
    {
        private const int NON_SHOOTING_MAX_PLAYER_COUNT = 2;

        public GameConfiguration GameConfiguration;
        public LevelFormat CurrentLevel;
        public SaveDataState SaveDataState = new();
        public LogicState LogicState;

        public Dictionary<int, Player> PlayerDictionary { get; } = new();

        private Dictionary<int, AbstractEnemy> EnemyDictionary { get; } = new();

        private LevelsListFormat levelsListConfig;
        private int CurrentLevelIndex;

        public void Init(LogicState logicState)
        {
            LogicState = logicState;
        }

        public void CreatePlayers()
        {
            PlayerDictionary.Clear();

            for (var index = 0; index < Instance.CurrentLevel.playerCount; index++) {
                Player player = new BasePlayer(new BasePlayerFactory());
                PlayerDictionary.Add(index, player);
            }
        }

        public void CreateEnemies()
        {
            EnemyDictionary.Clear();

            var enemyCount = Instance.CurrentLevel.enemyCount;
            var enemyIndex = 0;

            // Один враг, двигающийся по кругу
            var movableEnemy = new MovingRoundEnemy(new MovableEnemyFactory());
            EnemyDictionary.Add(enemyIndex, movableEnemy);
            enemyIndex++;

            // Один враг, преследующий игрока
            var stalkingEnemy = new StalkingEnemy(new StalkingEnemyFactory());
            EnemyDictionary.Add(enemyIndex, stalkingEnemy);
            enemyIndex++;

            if (enemyCount <= NON_SHOOTING_MAX_PLAYER_COUNT)
                return;

            // Все остальные враги стреляющие
            for (var index = EnemyDictionary.Count; index < enemyCount; index++) {
                var shootingEnemy = new ShootingEnemy(new ShootingEnemyFactory());
                EnemyDictionary.Add(index, shootingEnemy);
            }
        }

        public void RemovePlayersAndEnemies()
        {
            PlayerDictionary.Clear();
            EnemyDictionary.Clear();
        }

        public Player GetPlayerByIndex(int index)
        {
            var player = PlayerDictionary[index];
            return player;
        }

        public AbstractEnemy GetEnemyByIndex(int index)
        {
            var enemy = EnemyDictionary[index];
            return enemy;
        }

        public void RemovePlayerFromActivePlayers(Player player)
        {
            var playerItem = PlayerDictionary.First(playerPair => playerPair.Value == player);

            PlayerDictionary.Remove(playerItem.Key);
        }

        public void LoadLevelsConfig()
        {
            var textAsset = Resources.Load<TextAsset>("config");
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

            for (var index = 0; index < CurrentLevel.playerCount; index++) {
                if (index >= PlayersManager.Instance.Players.Count)
                    continue;

                var playerController = PlayersManager.Instance.Players.Values.ElementAt(index);

                var player = new PlayerFormat
                {
                    health = playerController.Player.Health,
                    position = playerController.transform.position
                };

                SaveDataState.SaveData.players.Add(player);
            }

            SaveDataState.SaveData.enemies = new List<EnemyFormat>();

            for (var index = 0; index < CurrentLevel.enemyCount; index++) {
                if (index >= EnemiesManager.Instance.Enemies.Count)
                    continue;

                var enemyController = EnemiesManager.Instance.Enemies.Values.ElementAt(index);

                var enemy = new EnemyFormat
                {
                    enemyType = enemyController.Enemy.EnemyType.ToString(),
                    position = enemyController.StartPosition
                };

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
