using UnityEngine;

namespace Players.Movement
{
    public class NormalPlayerMovement : PlayerMovement
    {
        public NormalPlayerMovement()
        {
            Speed = 9;
        }

        public override void Move(Vector3 direction)
        {
            var positionShift = direction * (Speed * SENSITIVITY_COEFFICIENT);
            Position += positionShift;
        }
    }
}
