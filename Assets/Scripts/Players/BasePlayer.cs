using Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    public class BasePlayer : Player
    {
        public BasePlayer(PlayerFactory playerFactory) : base(playerFactory)
        {
        }
    }
}
