using System;
using UnityEngine;

namespace Factories
{
    public class ShootingEnemyFactory : AbstractEnemyFactory
    {
        public virtual int Distance { get => 10; }

        public override int Damage { get => 5; }

        public override Color Color { get => Color.green; }
    }
}
