using System;
using Factories.EnemyFactories;
using Players;
using UnityEngine;

namespace Enemies
{
    public abstract class AbstractEnemy
    {
        protected readonly int Damage;

        public Color Color;

        public EnemyType EnemyType;

        public event Action OnAttackDone = () => { };

        protected AbstractEnemy(AbstractEnemyFactory enemyFactory)
        {
            Damage = enemyFactory.Damage;

            Color = enemyFactory.Color;
        }

        public abstract void UpdateAction(Transform transform, Vector3 startPosition);

        public abstract void Attack(Player player);

        protected void AttackDone()
        {
            OnAttackDone();
        }
    }
}
