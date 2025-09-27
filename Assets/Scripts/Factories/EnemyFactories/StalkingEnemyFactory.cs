using Enemies;
using UnityEngine;

namespace Factories.EnemyFactories
{
    public class StalkingEnemyFactory : MovableEnemyFactory
    {
        public override int Speed { get => 9; }
        public override int Damage { get => 15; }
        public int Distance { get => 8; }
        public override Color Color { get => Color.red; }

        public StalkingEnemyFactory()
        {
            DistantEnemyLogic = new DistantEnemyLogic(Distance);
        }

        public readonly DistantEnemyLogic DistantEnemyLogic;
    }
}
