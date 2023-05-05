using Factories;
using Players;
using SurviveStayAlive;
using System;
using UnityEngine;
using Utility;

namespace Enemies
{
    public class StalkingEnemy : AbstractEnemy, IEnemyMovable, IEnemyDistant
    {
        public int Speed { get; }

        public int Distance { get; }

        public StalkingEnemy(StalkingEnemyFactory enemyFactory) : base(enemyFactory)
        {
            Speed = enemyFactory.Speed;
            Distance = enemyFactory.Distance;
            EnemyType = EnemyType.Stalking;
        }

        public override void UpdateAction(Transform transform, Vector3 startPosition)
        {
            CheckPlayerNear(transform);
        }

        private void CheckPlayerNear(Transform transform)
        {
            foreach (var playerPair in PlayersManager.Instance.PlayerDictionary) {
                PlayerController playerController = playerPair.Value;
                float distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);
                if (distanceToPlayer < Distance) {
                    if (distanceToPlayer < CommonUtility.MINIMAL_DISTANCE_TO_PLAYER) {
                        Attack(playerController.Player);
                    }

                    StalkingToPlayer(transform, playerController);
                }
            }
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

        public override void Attack(Player player)
        {
            player.Attacked(Damage);

            AttackDone();
        }

        public void Move(Transform transform)
        {
            
        }
    }
}
