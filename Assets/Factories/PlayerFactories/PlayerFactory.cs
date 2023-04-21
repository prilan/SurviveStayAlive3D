using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factories
{
    public abstract class PlayerFactory
    {
        public virtual int Health { get; set; }
        public virtual int Speed { get; set; }
    }
}
