using Factories.EnemyFactories;
using Players;
using SurviveStayAlive;
using UnityEngine;

namespace Enemies
{
    public class ShootingEnemy : AbstractEnemy, IEnemyShooting, IEnemyDistant
    {
        public int Distance { get; }

        private Vector3 currentPosition;
        private PlayerController attackedPlayer;

        private float timeCounter;

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
                var playerController = playerPair.Value;
                var distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);
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
            ShellsManager.Instance.LaunchShell(Damage, currentPosition, attackedPlayer.transform.position);
        }
    }
}
