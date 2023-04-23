using System;
using UnityEngine;

namespace Factories
{
    public class MovableEnemyFactory : AbstractEnemyFactory
    {
        public virtual int Speed { get => 10; }

        public override int Damage { get => 5; }

        public override Color Color { get => Color.yellow; }
    }
}
