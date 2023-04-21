using Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    public abstract class Player
    {
        public int Health;
        public int Speed;

        public Player(PlayerFactory playerFactory) {
            Health = playerFactory.Health;
            Speed = playerFactory.Speed;
        }
    }
}
