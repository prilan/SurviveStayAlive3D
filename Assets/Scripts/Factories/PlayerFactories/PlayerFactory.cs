using Players.Movement;

namespace Factories.PlayerFactories
{
    public abstract class PlayerFactory
    {
        public virtual int Health { get; set; }

        public abstract PlayerMovement CreateMovement();
    }
}
