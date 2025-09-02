namespace Factories.PlayerFactories
{
    public class BasePlayerFactory : PlayerFactory
    {
        public override int Health => 100;
        public override int Speed => 8;
    }
}
