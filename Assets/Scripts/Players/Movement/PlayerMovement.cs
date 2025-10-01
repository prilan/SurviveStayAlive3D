using UnityEngine;

namespace Players.Movement
{
    public abstract class PlayerMovement
    {
        protected const float SENSITIVITY_COEFFICIENT = 0.001f;
        
        protected Vector3 Position;
        protected int Speed;
        
        public abstract void Move(Vector3 direction);

        public void SetStartPosition(Vector3 position)
        {
            Position = position;
        }
        
        public Vector3 GetPosition => Position;
    }
}
