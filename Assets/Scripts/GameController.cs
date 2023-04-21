using EventEmitter;
using SurviveStayAlive;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace SurviveStayAlive
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] MeshRenderer gameArea;

        private SphereCollider sphereCollider;

        public SphereCollider SphereCollider { get { return sphereCollider; } }

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
            AppModel.Instance.CreatePlayers();
            AppModel.Instance.CreateEnemies();

            int playerCount = GameManager.Instance.PlayerCount;
            int enemyCount = GameManager.Instance.EnemyCount;

            for (int i = 0; i < playerCount; i++) {
                PlayersManager.Instance.CreatePlayer();
            }

            for (int i = 0;i < enemyCount; i++) {
                EnemiesManager.Instance.CreateEnemy();
            }

            GameEventEmitter.OnPlayersInited();
        }
    }
}