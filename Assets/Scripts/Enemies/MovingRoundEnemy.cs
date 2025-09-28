using Enemies.Interfaces;
using Enemies.Logic;
using Factories.EnemyFactories;
using Players;
using UnityEngine;

namespace Enemies
{
    public class MovingRoundEnemy : MovableEnemy, IEnemyDistant
    {
        private const int CIRCLE_SIZE = 4;
        private const float SPEED_COEFFICIENT = 1f/7;

        public int Distance => 0;

        private readonly DistantEnemyLogic DistantEnemyLogic;
        
        private float timeCounter;
		
        public MovingRoundEnemy(MovableEnemyFactory enemyFactory) : base(enemyFactory)
        {
            DistantEnemyLogic = enemyFactory.DistantEnemyLogic;
            DistantEnemyLogic.Initialize(ActionWhenClose, null);
        }
        
        public override void UpdateAction(Transform transform, Vector3 startPosition)
        {
            base.UpdateAction(transform, startPosition);
            
            DistantEnemyLogic.CheckPlayerNear(transform);
        }
		
        public override void Move(Transform transform)
        {
            timeCounter += Time.deltaTime * SPEED_COEFFICIENT * Speed;

            var x = StartPosition.x + CIRCLE_SIZE * Mathf.Cos(timeCounter);
            var y = transform.position.y;
            var z = StartPosition.z - CIRCLE_SIZE * Mathf.Sin(timeCounter);
            
            Move(transform, new Vector3(x, y, z));
        }


        public virtual void ActionWhenClose(PlayerController playerController)
        {
            Attack(playerController.Player);
        }

        public void ActionWhenNear(Transform transform, PlayerController playerController)
        {
        }
    }
}
