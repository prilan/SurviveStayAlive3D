using Factories;
using System;

namespace Players
{
    public class BasePlayer : Player
    {
        public BasePlayer(PlayerFactory playerFactory) : base(playerFactory)
        {
        }
    }
}
