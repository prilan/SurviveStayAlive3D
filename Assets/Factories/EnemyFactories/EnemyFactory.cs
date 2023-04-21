using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Factories
{
    public abstract class EnemyFactory
    {
        public virtual int Damage { get; set; }
        public virtual int Speed { get; set; }
        public virtual int AttackDistance { get; set; }

        public virtual Color Color { get; set; }
    }
}
