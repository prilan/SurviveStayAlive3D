using EventEmitter;
using SurviveStayAlive;
using System;
using System.Linq;
using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float sensitivity = 1f;
        [SerializeField] Color defaultColor = Color.white;
        [SerializeField] Color activeColor = Color.blue;
        [SerializeField] Color finishedColor = Color.green;

        [SerializeField] float backShiftPositionvalue = 0.3f;

        [Header("Visualization")]
        [SerializeField] int Health;

        private Player currentPlayer;

        private MeshRenderer meshRenderer;
        
        private float sensitivityKoefficient = 0.1f;
        private float sensitivityShiftPerPress;

        public Player Player => currentPlayer;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();

            sensitivityShiftPerPress = sensitivity * sensitivityKoefficient;
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
        }

        private void ProcessKeyPress()
        {
            if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) {
                Vector3 newPosition = transform.position;
                newPosition.z += sensitivityShiftPerPress;
                transform.position = newPosition;
            } else if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow)) {
                Vector3 newPosition = transform.position;
                newPosition.x -= sensitivityShiftPerPress;
                transform.position = newPosition;
            } else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)) {
                Vector3 newPosition = transform.position;
                newPosition.z -= sensitivityShiftPerPress;
                transform.position = newPosition;
            } else if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow)) {
                Vector3 newPosition = transform.position;
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
            int EXIT_AREA_LAYER_ID = 6;
            int exitAreaLayerMask = (1 << EXIT_AREA_LAYER_ID);

            bool isInExitArea = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity, exitAreaLayerMask);

            if (isInExitArea) {
                SetPlayerReachedGoal();
                CheckWinState();
            }
        }

        private void CheckWinState()
        {
            bool isWin = !PlayersManager.Instance.Players.Any();
            if (isWin) {
                AppModel.Instance.LogicState.ChangeState(LogicStateEnum.WinState);
                GameEventEmitter.OnWinLevel();
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
            int LEVEL_SURFACE_LAYER_ID = 3;
            int surfaceLayerMask = (1 << LEVEL_SURFACE_LAYER_ID);

            bool isOnLevelSurface = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity, surfaceLayerMask);

            if (!isOnLevelSurface) {
                SetPositionBack();
            }
        }

        private void SetPositionBack()
        {
            Vector3 position = meshRenderer.transform.position;

            if (position.x > 0) {
                position.x -= backShiftPositionvalue;
            } else { 
                position.x += backShiftPositionvalue;
            }

            if (position.z > 0) {
                position.z -= backShiftPositionvalue;
            } else {
                position.z += backShiftPositionvalue;
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

        public void SetPlayerFinished()
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
