using EventEmitter;
using UnityEngine;

namespace SurviveStayAlive
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] MeshRenderer levelSurface;

        private MeshCollider surfaceCollider;

        public MeshCollider SurfaceCollider { get { return surfaceCollider; } }

        private void Start()
        {
            surfaceCollider = levelSurface.GetComponent<MeshCollider>();

            Initialize();
        }

        private void Update()
        {
            if (PlayersManager.Instance.IsActivePlayersEmpty &&
                AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PlayState)
            {
                AppModel.Instance.LogicState.ChangeState(LogicStateEnum.PauseState);
            }
        }

        private void Initialize()
        {
            AppModel.Instance.Init(new LogicState(LogicStateEnum.PlayState));
            AppModel.Instance.LoadLevelsConfig();

            CreatePlayersAndEnemies();

            AppModel.Instance.UpdateDataState();

            GameEventEmitter.OnPlayersInited();
        }

        private static void CreatePlayersAndEnemies()
        {
            AppModel.Instance.CreatePlayers();
            AppModel.Instance.CreateEnemies();

            var playerCount = AppModel.Instance.CurrentLevel.playerCount;
            var enemyCount = AppModel.Instance.CurrentLevel.enemyCount;

            for (var index = 0; index < playerCount; index++) {
                PlayersManager.Instance.CreatePlayer();
            }

            for (var index = 0; index < enemyCount; index++) {
                EnemiesManager.Instance.CreateEnemy();
            }
        }

        public void RestartLevel()
        {
            AppModel.Instance.Init(new LogicState(LogicStateEnum.PlayState));

            RemoveAllGameEntities();
            CreatePlayersAndEnemies();

            GameEventEmitter.OnRestartLevel();
        }

        public void LoadNextLevel()
        {
            AppModel.Instance.Init(new LogicState(LogicStateEnum.PlayState));
            AppModel.Instance.LoadNextLevel();

            RemoveAllGameEntities();
            CreatePlayersAndEnemies();

            GameEventEmitter.OnRestartLevel();
        }

        public void LoadSavedState()
        {
            AppModel.Instance.LoadSavedState();

            RemoveAllGameEntities();
            CreatePlayersAndEnemies();

            var playerCount = AppModel.Instance.CurrentLevel.playerCount;
            var enemyCount = AppModel.Instance.CurrentLevel.enemyCount;

            var saveDataFormat = AppModel.Instance.SaveDataState.SaveData;

            for (var index = 0; index < playerCount; index++) {
                PlayersManager.Instance.UpdatePlayerFromState(index, saveDataFormat.players[index]);
            }

            for (var index = 0; index < enemyCount; index++) {
                EnemiesManager.Instance.UpdateEnemyFromState(index, saveDataFormat.enemies[index]);
            }

            GameEventEmitter.OnRestartLevel();
        }

        private static void RemoveAllGameEntities()
        {
            AppModel.Instance.RemovePlayersAndEnemies();
            PlayersManager.Instance.RemovePlayers();
            EnemiesManager.Instance.RemoveEnemies();
        }
    }
}
