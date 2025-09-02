using System;

namespace Factories.PlayerFactories
{
    public abstract class PlayerFactory
    {
        public virtual int Health { get; set; }
        public virtual int Speed { get; set; }
    }
}
