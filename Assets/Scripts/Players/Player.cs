using System;
using Factories.PlayerFactories;

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

        public void Attacked(int damage)
        {
            Health -= damage;

            if (Health < 0)
                Health = 0;
        }

        public Action<bool> OnSetActive;
        public Action OnSetReachedGoal;
    }
}
