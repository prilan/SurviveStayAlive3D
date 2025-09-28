using Enemies.Logic;
using UnityEngine;

namespace Factories.EnemyFactories
{
    public class MovableEnemyFactory : AbstractEnemyFactory
    {
        public virtual int Speed { get => 6; }

        public override int Damage { get => 8; }

        public override Color Color { get => Color.yellow; }

        public MovableEnemyFactory()
        {
            DistantEnemyLogic = new DistantEnemyLogic();
        }

        public readonly DistantEnemyLogic DistantEnemyLogic;
    }
}
