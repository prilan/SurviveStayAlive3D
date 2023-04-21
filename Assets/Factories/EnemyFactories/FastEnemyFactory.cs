using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Factories
{
    public class FastEnemyFactory : EnemyFactory
    {
        public override int Damage { get => 5; }
        public override int Speed { get => 30; }
        public override int AttackDistance { get => 3; }

        public override Color Color { get => Color.yellow; }
    }
}
