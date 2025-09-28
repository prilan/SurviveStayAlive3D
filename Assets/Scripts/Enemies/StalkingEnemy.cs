using System;
using Enemies.Interfaces;
using Enemies.Logic;
using Factories.EnemyFactories;
using Players;
using UnityEngine;

namespace Enemies
{
    public class StalkingEnemy : MovableEnemy, IEnemyDistant
    {
        public int Distance { get; }
        
        private readonly DistantEnemyLogic DistantEnemyLogic;

        public StalkingEnemy(StalkingEnemyFactory enemyFactory) : base(enemyFactory)
        {
            Distance = enemyFactory.Distance;
            EnemyType = EnemyType.Stalking;

            DistantEnemyLogic = enemyFactory.DistantEnemyLogic;
            DistantEnemyLogic.Initialize(ActionWhenClose, ActionWhenNear);
        }

        public override void UpdateAction(Transform transform, Vector3 startPosition)
        {
            base.UpdateAction(transform, startPosition);
            
            DistantEnemyLogic.CheckPlayerNear(transform);
        }

        public virtual void ActionWhenClose(PlayerController playerController)
        {
            Attack(playerController.Player);
        }
        
        public virtual void ActionWhenNear(Transform transform, PlayerController playerController)
        {
            StalkingToPlayer(transform, playerController);
        }

        private void StalkingToPlayer(Transform transform, Component playerController)
        {
            var speedDelta = Time.deltaTime * Speed * 0.2f;

            var position = transform.position;
            var playerPosition = playerController.transform.position;

            var deX = playerPosition.x - position.x;
            var deZ = playerPosition.z - position.z;

            var deltaX = (float)Math.Sqrt(Math.Pow(speedDelta, 2) / (1 + Math.Pow(deZ / deX, 2)));
            var deltaZ = deltaX * deZ / deZ;

            if (deX < 0)
                deltaX *= -1;
            if (deZ < 0)
                deltaZ *= -1;

            var x = position.x + deltaX;
            var y = transform.position.y;
            var z = position.z + deltaZ;

            Move(transform, new Vector3(x, y, z));
        }
    }
}
