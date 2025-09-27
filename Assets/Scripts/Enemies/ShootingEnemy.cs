using Factories.EnemyFactories;
using Players;
using SurviveStayAlive;
using UnityEngine;

namespace Enemies
{
    public class ShootingEnemy : AbstractEnemy, IEnemyShooting, IEnemyDistant
    {
        public int Distance { get; }

        private readonly DistantEnemyLogic DistantEnemyLogic;
        
        private Vector3 currentPosition;
        private PlayerController attackedPlayer;

        private float timeCounter;

        private const float RELOAD_TIME_SEC = 5f;

        public ShootingEnemy(ShootingEnemyFactory enemyFactory) : base(enemyFactory)
        {
            Distance = enemyFactory.Distance;
            EnemyType = EnemyType.Shooting;
            
            DistantEnemyLogic = enemyFactory.DistantEnemyLogic;
            DistantEnemyLogic.Initialize(ActionWhenClose, ActionWhenNear);
        }

        public override void UpdateAction(Transform transform, Vector3 startPosition)
        {
            ProcessShootingPause();
            if (timeCounter > 0)
                return;
            
            DistantEnemyLogic.CheckPlayerNear(transform);
        }

        public virtual void ActionWhenClose(PlayerController playerController)
        {
        }

        public virtual void ActionWhenNear(Transform transform, PlayerController playerController)
        {
            currentPosition = transform.position;
            attackedPlayer = playerController;
            Attack(playerController.Player);
            
            StartShootingPause();
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
