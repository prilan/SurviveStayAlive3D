using Factories;
using Players;
using SurviveStayAlive;
using System;
using UnityEngine;
using Utility;

namespace Enemies
{
    public class StalkingEnemy : MovableEnemy, IEnemyDistant
    {
        public int Distance { get; }

        public StalkingEnemy(StalkingEnemyFactory enemyFactory) : base(enemyFactory)
        {
            Distance = enemyFactory.Distance;
            EnemyType = EnemyType.Stalking;
        }

        protected override void ActionWhenNear(Transform transform, PlayerController playerController)
        {
            base.ActionWhenNear(transform, playerController);
            
            StalkingToPlayer(transform, playerController);
        }

        private void StalkingToPlayer(Transform transform, PlayerController playerController)
        {
            float speedDelta = Time.deltaTime * Speed * 0.2f;

            Vector3 position = transform.position;
            Vector3 playerPosition = playerController.transform.position;

            float deX = playerPosition.x - position.x;
            float deZ = playerPosition.z - position.z;

            float deltaX = (float)Math.Sqrt(Math.Pow(speedDelta, 2) / (1 + Math.Pow((deZ / deX), 2)));
            float deltaZ = (deltaX * deZ) / deZ;

            if (deX < 0)
                deltaX *= -1;
            if (deZ < 0)
                deltaZ *= -1;

            float x = position.x + deltaX;
            float y = transform.position.y;
            float z = position.z + deltaZ;
            transform.position = new Vector3(x, y, z);
        }
    }
}
