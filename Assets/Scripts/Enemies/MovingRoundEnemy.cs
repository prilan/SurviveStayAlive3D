using Factories.EnemyFactories;
using UnityEngine;

namespace Enemies
{
    public class MovingRoundEnemy : MovableEnemy
    {
        private float timeCounter;
		
        public MovingRoundEnemy(MovableEnemyFactory enemyFactory) : base(enemyFactory)
        {
        }
		
        public override void Move(Transform transform)
        {
            timeCounter += Time.deltaTime / 7 * Speed;

            var x = StartPosition.x + 4 * Mathf.Cos(timeCounter);
            var y = transform.position.y;
            var z = StartPosition.z - 4 * Mathf.Sin(timeCounter);
            
            Move(transform, new Vector3(x, y, z));
        }
    }
}
