using EventEmitter;
using SurviveStayAlive;
using System;
using UnityEngine;

namespace SurviveStayAlive
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] MeshRenderer gameArea;
        [SerializeField] MeshRenderer levelSurface;

        private SphereCollider sphereCollider;
        private MeshCollider surfaceCollider;

        public SphereCollider SphereCollider { get { return sphereCollider; } }
        public MeshCollider SurfaceCollider { get { return surfaceCollider; } }

        public MeshRenderer GameArea
        {
            get
            {
                return gameArea;
            }
        }

        void Start()
        {
            sphereCollider = gameArea.GetComponent<SphereCollider>();
            surfaceCollider = levelSurface.GetComponent<MeshCollider>();

            Initialize();
        }

        void Update()
        {
            if (PlayersManager.Instance.IsActivePlayersEmpty && AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PlayState) {
                AppModel.Instance.LogicState.ChangeState(LogicStateEnum.PauseState);
            }
        }

        private void Initialize()
        {
            AppModel.Instance.Init(new LogicState(LogicStateEnum.PlayState));

            AppModel.Instance.LoadLevelsConfig();

            AppModel.Instance.CreatePlayers();
            AppModel.Instance.CreateEnemies();

            int playerCount = AppModel.Instance.CurrentLevel.playerCount;
            int enemyCount = AppModel.Instance.CurrentLevel.enemyCount;

            for (int i = 0; i < playerCount; i++) {
                PlayersManager.Instance.CreatePlayer();
            }

            for (int i = 0;i < enemyCount; i++) {
                EnemiesManager.Instance.CreateEnemy();
            }

            AppModel.Instance.UpdateDataState();

            GameEventEmitter.OnPlayersInited();
        }
    }
}