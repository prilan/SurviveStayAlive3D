using SurviveStayAlive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float sensitivity = 1f;
        [SerializeField] Color defaultColor = Color.white;
        [SerializeField] Color activeColor = Color.blue;
        [SerializeField] Color finishedColor = Color.green;

        private Player currentPlayer;

        private MeshRenderer meshRenderer;
        
        private float sensitivityKoefficient = 0.1f;
        private float sensitivityShiftPerPress;

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
        }

        private void ProcessKeyPress()
        {
            if (Input.GetKeyDown("w")) {
                Vector3 newPosition = transform.position;
                newPosition.z += sensitivityShiftPerPress;
                transform.position = newPosition;
            } else if (Input.GetKeyDown("a")) {
                Vector3 newPosition = transform.position;
                newPosition.x -= sensitivityShiftPerPress;
                transform.position = newPosition;
            } else if (Input.GetKeyDown("s")) {
                Vector3 newPosition = transform.position;
                newPosition.z -= sensitivityShiftPerPress;
                transform.position = newPosition;
            } else if (Input.GetKeyDown("d")) {
                Vector3 newPosition = transform.position;
                newPosition.x += sensitivityShiftPerPress;
                transform.position = newPosition;
            }
        }

        private void ProcessPosition()
        {
            float distance = Vector3.Distance(meshRenderer.transform.position, transform.position);
            if (distance < GameModel.Instance.GameController.SphereCollider.radius) {
                // 
            } else {
                SetPlayerFinished();
            }
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
    }
}
