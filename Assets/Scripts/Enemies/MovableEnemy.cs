using Enemies;
using Factories;
using Players;
using SurviveStayAlive;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Enemies
{
    public class MovableEnemy : AbstractEnemy, IEnemyMovable
    {
        public int Speed { get; }

        private float timeCounter = 0;

        Vector3 startPosition = Vector3.zero;

        public MovableEnemy(MovableEnemyFactory enemyFactory) : base(enemyFactory)
        {
            Speed = enemyFactory.Speed;
            EnemyType = EnemyType.Movable;
        }

        public override void UpdateAction(Transform transform, Vector3 startPosition)
        {
            timeCounter += Time.deltaTime / 7 * Speed;

            float x = startPosition.x + 4 * Mathf.Cos(timeCounter);
            float y = transform.position.y;
            float z = startPosition.z - 4 * Mathf.Sin(timeCounter);
            transform.position = new Vector3(x, y, z);

            CheckPlayerNear(transform);
        }

        private void CheckPlayerNear(Transform transform)
        {
            foreach (var playerPair in PlayersManager.Instance.PlayerDictionary) {
                PlayerController playerController = playerPair.Value;
                float distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);
                if (distanceToPlayer < CommonUtility.MINIMAL_DISTANCE_TO_PLAYER) {
                    Attack(playerController.Player);
                }
            }
        }

        public override void Attack(Player player)
        {
            player.Attacked(Damage);

            AttackDone();
        }
    }
}
