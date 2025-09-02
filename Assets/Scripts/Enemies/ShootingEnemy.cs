using Enemies;
using Factories;
using Players;
using SurviveStayAlive;
using System;
using Factories.EnemyFactories;
using UnityEngine;

namespace Enemies
{
    public class ShootingEnemy : AbstractEnemy, IEnemyDistant
    {
        public int Distance { get; }

        private Vector3 currentPosition;
        private PlayerController attackedPlayer;

        private float timeCounter = 0;

        private const float RELOAD_TIME_SEC = 5f;

        public ShootingEnemy(ShootingEnemyFactory enemyFactory) : base(enemyFactory)
        {
            Distance = enemyFactory.Distance;
            EnemyType = EnemyType.Shooting;
        }

        public override void UpdateAction(Transform transform, Vector3 startPosition)
        {
            CheckPlayerNear(transform);
        }

        private void CheckPlayerNear(Transform transform)
        {
            ProcessShootingPause();
            if (timeCounter > 0)
                return;

            foreach (var playerPair in PlayersManager.Instance.PlayerDictionary) {
                PlayerController playerController = playerPair.Value;
                float distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);
                if (distanceToPlayer < Distance) {
                    currentPosition = transform.position;
                    attackedPlayer = playerController;
                    Attack(playerController.Player);
                    StartShootingPause();
                }
            }
        }

        private void ProcessShootingPause()
        {
            if (timeCounter == 0)
                return;

            timeCounter += Time.deltaTime;

            if (timeCounter > RELOAD_TIME_SEC) {
                timeCounter = 0;
            }
        }

        private void StartShootingPause()
        {
            timeCounter += Time.deltaTime;
        }

        public override void Attack(Player player)
        {
            ShellsManager.Instance.LauchShell(Damage, currentPosition, attackedPlayer.transform.position);
        }
    }
}
