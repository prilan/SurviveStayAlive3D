using System;
using Players;
using SurviveStayAlive;
using UnityEngine;
using Utility;

namespace Enemies
{
    public class DistantEnemyLogic
    {
        private readonly int _distance;

        private Action<PlayerController> ActionWhenClose;
        private Action<Transform, PlayerController> ActionWhenNear;
        
        public DistantEnemyLogic(int distance)
        {
            _distance = distance;
        }
        
        public void Initialize(Action<PlayerController> actionWhenClose, Action<Transform, PlayerController> actionWhenNear)
        {
            ActionWhenClose = actionWhenClose;
            ActionWhenNear = actionWhenNear;
        }

        public void CheckPlayerNear(Transform transform)
        {
            foreach (var playerPair in PlayersManager.Instance.PlayerDictionary) {
                var playerController = playerPair.Value;
                var distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);
                if (distanceToPlayer < CommonUtility.MINIMAL_DISTANCE_TO_PLAYER) {
                    ActionWhenClose(playerController);
                }

                if (distanceToPlayer < _distance) {
                    ActionWhenNear(transform, playerController);
                }
            }
        }
    }
}
