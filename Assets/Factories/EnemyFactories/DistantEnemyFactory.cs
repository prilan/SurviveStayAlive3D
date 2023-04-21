using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Factories
{
    public class DistantEnemyFactory : EnemyFactory
    {
        public override int Damage { get => 10; }
        public override int Speed { get => 1; }
        public override int AttackDistance { get => 10; }

        public override Color Color { get => Color.green; }
    }
}
