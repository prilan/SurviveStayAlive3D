using Factories.EnemyFactories;
using Players;
using SurviveStayAlive;
using UnityEngine;
using Utility;

namespace Enemies
{
    public class MovableEnemy : AbstractEnemy, IEnemyMovable
    {
        public int Speed { get; }

        protected Vector3 StartPosition = Vector3.zero;

        protected MovableEnemy(MovableEnemyFactory enemyFactory) : base(enemyFactory)
        {
            Speed = enemyFactory.Speed;
            EnemyType = EnemyType.Movable;
        }

        public override void UpdateAction(Transform transform, Vector3 startPosition)
        {
            StartPosition = startPosition;
            
            Move(transform);
            CheckPlayerNear(transform);
        }

        protected virtual void ActionWhenNear(Transform transform, PlayerController playerController)
        {
        }

        private void CheckPlayerNear(Transform transform)
        {
            foreach (var playerPair in PlayersManager.Instance.PlayerDictionary) {
                var playerController = playerPair.Value;
                var distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);
                if (distanceToPlayer < CommonUtility.MINIMAL_DISTANCE_TO_PLAYER) {
                    Attack(playerController.Player);
                }
                
                ActionWhenNear(transform, playerController);
            }
        }

        public override void Attack(Player player)
        {
            player.Attacked(Damage);

            AttackDone();
        }

        public virtual void Move(Transform transform)
        {
        }

        public virtual void Move(Transform transform, Vector3 moveTo)
        {
            transform.position = moveTo;            
        }
    }
}
