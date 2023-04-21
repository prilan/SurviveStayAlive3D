using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Factories
{
    public class BossEnemyFactory : EnemyFactory
    {
        public override int Damage { get => 50; }
        public override int Speed { get => 10; }
        public override int AttackDistance { get => 5; }

        public override Color Color { get => Color.magenta; }
    }
}
