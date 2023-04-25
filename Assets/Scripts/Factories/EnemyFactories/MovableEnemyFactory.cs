using System;
using UnityEngine;

namespace Factories
{
    public class MovableEnemyFactory : AbstractEnemyFactory
    {
        public virtual int Speed { get => 6; }

        public override int Damage { get => 8; }

        public override Color Color { get => Color.yellow; }
    }
}
