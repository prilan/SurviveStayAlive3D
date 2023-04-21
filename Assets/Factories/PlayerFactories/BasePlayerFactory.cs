using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factories
{
    public class BasePlayerFactory : PlayerFactory
    {
        public override int Health { get => 100; }
        public override int Speed { get => 10; }
    }
}
