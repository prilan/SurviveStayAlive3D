using Enemies.Logic;
using UnityEngine;

namespace Factories.EnemyFactories
{
    public class ShootingEnemyFactory : AbstractEnemyFactory
    {
        public override int Damage { get => 25; }
        public int Distance { get => 10; }
        public override Color Color { get => Color.green; }

        public ShootingEnemyFactory()
        {
            DistantEnemyLogic = new DistantEnemyLogic(Distance);
        }
        
        public readonly DistantEnemyLogic DistantEnemyLogic;
    }
}
