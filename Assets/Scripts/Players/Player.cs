using System;
using Factories.PlayerFactories;
using Players.Movement;
using UnityEngine;

namespace Players
{
    public abstract class Player
    {
        public int Health;

        private Vector3 Position;
        private readonly PlayerMovement Movement;

        protected Player(PlayerFactory playerFactory) {
            Health = playerFactory.Health;

            Movement = playerFactory.CreateMovement();
        }

        public void SetStartPosition(Vector3 position)
        {
            Movement.SetStartPosition(position);
            RefreshPosition();
        }

        public Vector3 GetPosition()
        {
            RefreshPosition();
            
            return Position;
        }

        public void Move(Vector3 direction)
        {
            Movement.Move(direction);
        }

        public void Attacked(int damage)
        {
            Health -= damage;

            if (Health < 0)
                Health = 0;
        }
        
        private void RefreshPosition()
        {
            Position = Movement.GetPosition;
        }

        public Action<bool> OnSetActive;
        public Action OnSetReachedGoal;
    }
}
