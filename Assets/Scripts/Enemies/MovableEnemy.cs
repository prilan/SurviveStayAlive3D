using Factories.EnemyFactories;
using Players;
using UnityEngine;

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
