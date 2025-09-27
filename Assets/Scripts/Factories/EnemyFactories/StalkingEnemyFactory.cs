using UnityEngine;

namespace Factories.EnemyFactories
{
    public class StalkingEnemyFactory : MovableEnemyFactory
    {
        public override int Speed { get => 9; }

        public override int Damage { get => 15; }

        public virtual int Distance { get => 8; }

        public override Color Color { get => Color.red; }
    }
}
