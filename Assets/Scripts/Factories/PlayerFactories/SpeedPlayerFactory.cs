using Players.Movement;

namespace Factories.PlayerFactories
{
    public class SpeedPlayerFactory : BasePlayerFactory
    {
        public override PlayerMovement CreateMovement()
        {
            return new SpeedPlayerMovement();
        }
    }
}
