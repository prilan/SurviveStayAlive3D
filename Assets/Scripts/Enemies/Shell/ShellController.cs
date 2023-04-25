using Players;
using SurviveStayAlive;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Utility;

namespace Enemies.Shell
{
    public class ShellController : MonoBehaviour
    {
        private int currentDamage;
        
        private Vector3 moveVector = Vector3.zero;

        private const float TIME_ALIVE_SEC = 10f;

        private float timerAlive = 0;

        private void Start()
        {

        }

        private void Update()
        {
            if (AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PauseState)
                return;

            if (moveVector.Equals(Vector3.zero))
                return;

            ShellMove();
            CheckPlayerNear(transform);
            CheckTimeAlive();
        }

        private void CheckTimeAlive()
        {
            timerAlive += Time.deltaTime;

            if (timerAlive > TIME_ALIVE_SEC) {
                SetActive(false);
            }
        }

        private void ShellMove()
        {
            Vector3 position = transform.position;

            float x = position.x + moveVector.x;
            float y = transform.position.y;
            float z = position.z + moveVector.z;

            transform.position = new Vector3(x, y, z);
        }

        public void SetShellDamge(int damage)
        {
            currentDamage = damage;
        }

        public void StartMove(Vector3 targetVector)
        {
            moveVector = targetVector * Time.deltaTime;
        }

        private void CheckPlayerNear(Transform transform)
        {
            foreach (var playerPair in PlayersManager.Instance.PlayerDictionary) {
                PlayerController playerController = playerPair.Value;
                float distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);
                if (distanceToPlayer < CommonUtility.MINIMAL_DISTANCE_TO_PLAYER) {
                    Attack(playerController.Player);
                }
            }
        }

        private void Attack(Player player)
        {
            player.Attacked(currentDamage);

            SetActive(false);
            Destroy(gameObject);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}