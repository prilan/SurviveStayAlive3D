using System;
using UnityEngine;

namespace Factories
{
    public class StalkingEnemyFactory : MovableEnemyFactory
    {
        public override int Speed { get => 10; }

        public override int Damage { get => 15; }

        public virtual int Distance { get => 7; }

        public override Color Color { get => Color.red; }
    }
}
