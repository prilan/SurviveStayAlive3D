using UnityEngine;

namespace Factories.EnemyFactories
{
    public abstract class AbstractEnemyFactory
    {
        public virtual int Damage { get; set; }

        public virtual Color Color { get; set; }
    }
}
