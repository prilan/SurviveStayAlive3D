using Factories;
using Factories.EnemyFactories;
using UnityEngine;

namespace Enemies
{
	public class MovingRoundEnemy : MovableEnemy
	{
		private float timeCounter = 0;
		
		public MovingRoundEnemy(MovableEnemyFactory enemyFactory) : base(enemyFactory)
		{
		}
		
		public override void Move(Transform transform)
		{
			timeCounter += Time.deltaTime / 7 * Speed;

			float x = StartPosition.x + 4 * Mathf.Cos(timeCounter);
			float y = transform.position.y;
			float z = StartPosition.z - 4 * Mathf.Sin(timeCounter);
			transform.position = new Vector3(x, y, z);
		}
	}
}