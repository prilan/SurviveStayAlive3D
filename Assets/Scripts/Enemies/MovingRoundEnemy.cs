using Factories.EnemyFactories;
using UnityEngine;

namespace Enemies
{
    public class MovingRoundEnemy : MovableEnemy
    {
        private const int CIRCLE_SIZE = 4;
        private const float SPEED_COEFFICIENT = 1f/7;

        private float timeCounter;
		
        public MovingRoundEnemy(MovableEnemyFactory enemyFactory) : base(enemyFactory)
        {
        }
		
        public override void Move(Transform transform)
        {
            timeCounter += Time.deltaTime * SPEED_COEFFICIENT * Speed;

            var x = StartPosition.x + CIRCLE_SIZE * Mathf.Cos(timeCounter);
            var y = transform.position.y;
            var z = StartPosition.z - CIRCLE_SIZE * Mathf.Sin(timeCounter);
            
            Move(transform, new Vector3(x, y, z));
        }
    }
}
