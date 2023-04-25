using System;

namespace Factories
{
    public class BasePlayerFactory : PlayerFactory
    {
        public override int Health { get => 100; }
        public override int Speed { get => 8; }
    }
}
