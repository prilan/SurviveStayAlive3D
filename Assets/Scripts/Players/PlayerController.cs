using System.Linq;
using EventEmitter;
using SurviveStayAlive;
using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        private const float SENSITIVITY_COEFFICIENT = 0.001f;

        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color activeColor = Color.blue;
        [SerializeField] private Color finishedColor = Color.green;

        [SerializeField] private float backShiftPositionValue = 1f;

        [Header("Visualization")]
        [SerializeField] int Health;

        private Player currentPlayer;

        private MeshRenderer meshRenderer;

        private float sensitivityShiftPerPress;

        private Vector3 force = Vector3.zero;

        public Player Player => currentPlayer;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();

            sensitivityShiftPerPress = currentPlayer.Speed * SENSITIVITY_COEFFICIENT;
        }

        private void Update()
        {
            if (AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PauseState)
                return;

            if (PlayersManager.Instance.CurrentPlayerController != this)
                return;

            ProcessKeyPress();
            ProcessPosition();

            ShowHealth();
            CheckLoseState();
        }

        private void ProcessKeyPress()
        {
            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) {
                var newPosition = transform.position;
                newPosition.z += sensitivityShiftPerPress;
                transform.position = newPosition;
            }
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)) {
                var newPosition = transform.position;
                newPosition.x -= sensitivityShiftPerPress;
                transform.position = newPosition;
            }
            if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)) {
                var newPosition = transform.position;
                newPosition.z -= sensitivityShiftPerPress;
                transform.position = newPosition;
            }
            if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)) {
                var newPosition = transform.position;
                newPosition.x += sensitivityShiftPerPress;
                transform.position = newPosition;
            }
        }

        private void ProcessPosition()
        {
            ProcessInExitArea();
            ProcessOnLevelSurface();
        }

        private void ProcessInExitArea()
        {
            const int EXIT_AREA_LAYER_ID = 6;
            const int exitAreaLayerMask = 1 << EXIT_AREA_LAYER_ID;

            var isInExitArea = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity, exitAreaLayerMask);

            if (isInExitArea) {
                SetPlayerReachedGoal();
                CheckWinState();
            }
        }

        private void CheckWinState()
        {
            var isWin = !PlayersManager.Instance.Players.Any();
            if (isWin) {
                AppModel.Instance.LogicState.ChangeState(LogicStateEnum.WinState);
                GameEventEmitter.OnWinLevel();
            }
        }

        private void CheckLoseState()
        {
            if (AppModel.Instance.LogicState.CurrentLogicState != LogicStateEnum.PlayState)
                return;

            if (currentPlayer.Health <= 0) {
                AppModel.Instance.LogicState.ChangeState(LogicStateEnum.LoseState);
                GameEventEmitter.OnLoseLevel();
            }
        }

        private void SetPlayerReachedGoal()
        {
            SetPlayerFinished();
            PlayersManager.Instance.ResetCurrentPlayerController();

            currentPlayer.OnSetReachedGoal();
        }

        private void ProcessOnLevelSurface()
        {
            const int LEVEL_SURFACE_LAYER_ID = 3;
            const int surfaceLayerMask = 1 << LEVEL_SURFACE_LAYER_ID;

            var isOnLevelSurface = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity, surfaceLayerMask);

            if (!isOnLevelSurface) {
                SetPositionBack();
            }
        }

        private void SetPositionBack()
        {
            var position = meshRenderer.transform.position;

            if (position.x > 0) {
                position.x -= backShiftPositionValue;
            } else { 
                position.x += backShiftPositionValue;
            }

            if (position.z > 0) {
                position.z -= backShiftPositionValue;
            } else {
                position.z += backShiftPositionValue;
            }

            meshRenderer.transform.position = position;
        }

        public void SetPlayer(Player player)
        {
            currentPlayer = player;
        }

        public void SetPlayerActive(bool isActive)
        {
            meshRenderer.material.SetColor("_Color", isActive ? activeColor : defaultColor);
        }

        private void SetPlayerFinished()
        {
            meshRenderer.material.SetColor("_Color", finishedColor);

            PlayersManager.Instance.RemovePlayerFromActivePlayers(this);
            AppModel.Instance.RemovePlayerFromActivePlayers(currentPlayer);
        }

        private void ShowHealth()
        {
            Health = currentPlayer.Health;
        }
    }
}
