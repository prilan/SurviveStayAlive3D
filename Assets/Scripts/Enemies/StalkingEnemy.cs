using System;
using Factories.EnemyFactories;
using Players;
using SurviveStayAlive;
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

        public override void UpdateAction(Transform transform, Vector3 startPosition)
        {
            base.UpdateAction(transform, startPosition);
            
            CheckPlayerNear(transform);
        }

        public virtual void ActionWhenNear(Transform transform, PlayerController playerController)
        {
            StalkingToPlayer(transform, playerController);
        }

        private void CheckPlayerNear(Transform transform)
        {
            foreach (var playerPair in PlayersManager.Instance.PlayerDictionary) {
                var playerController = playerPair.Value;
                var distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);
                if (distanceToPlayer < CommonUtility.MINIMAL_DISTANCE_TO_PLAYER) {
                    Attack(playerController.Player);
                }

                if (distanceToPlayer < Distance)
                {
                    ActionWhenNear(transform, playerController);
                }
            }
        }

        private void StalkingToPlayer(Transform transform, Component playerController)
        {
            var speedDelta = Time.deltaTime * Speed * 0.2f;

            var position = transform.position;
            var playerPosition = playerController.transform.position;

            var deX = playerPosition.x - position.x;
            var deZ = playerPosition.z - position.z;

            var deltaX = (float)Math.Sqrt(Math.Pow(speedDelta, 2) / (1 + Math.Pow((deZ / deX), 2)));
            var deltaZ = (deltaX * deZ) / deZ;

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
