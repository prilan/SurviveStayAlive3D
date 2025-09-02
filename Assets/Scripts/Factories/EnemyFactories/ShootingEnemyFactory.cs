using UnityEngine;

namespace Factories.EnemyFactories
{
    public class ShootingEnemyFactory : AbstractEnemyFactory
    {
        public virtual int Distance { get => 10; }

        public override int Damage { get => 25; }

        public override Color Color { get => Color.green; }
    }
}
