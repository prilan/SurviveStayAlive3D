using Factories;
using Players;
using System;
using UnityEngine;

namespace Enemies
{
    public abstract class AbstractEnemy
    {
        public int Damage;

        public Color Color;

        public event Action OnAttackDone = () => { };

        public AbstractEnemy(AbstractEnemyFactory enemyFactory)
        {
            Damage = enemyFactory.Damage;

            Color = enemyFactory.Color;
        }

        public abstract void UpdateAction(Transform transform, Vector3 startPosition);

        public abstract void Attack(Player player);

        public void AttackDone()
        {
            OnAttackDone();
        }
    }
}
