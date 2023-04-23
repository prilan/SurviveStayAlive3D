using System;
using UnityEngine;

namespace Factories
{
    public class StalkingEnemyFactory : MovableEnemyFactory
    {
        public override int Speed { get => 20; }

        public override int Damage { get => 3; }

        public virtual int Distance { get => 5; }

        public override Color Color { get => Color.red; }
    }
}
