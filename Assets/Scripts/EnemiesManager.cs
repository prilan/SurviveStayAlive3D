using Enemies;
using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;
using Random = System.Random;

namespace SurviveStayAlive
{
    public class EnemiesManager : MonoSingleton<EnemiesManager>
    {
        [SerializeField] EnemyController enemyControllerPrefab;
        [SerializeField] Transform enemiesTransform;

        private Dictionary<int, EnemyController> enemyDictionary = new Dictionary<int, EnemyController>();

        private Random random = new Random();

        public void CreateEnemy()
        {
            int index = enemyDictionary.Count;
            EnemyController enemyController = Instantiate(enemyControllerPrefab, enemiesTransform);
            Enemy enemy = AppModel.Instance.GetEnemyByIndex(index);
            enemyController.SetEnemy(enemy);
            enemyController.transform.position = GetRandomEnemyPosition();
            enemyDictionary.Add(index, enemyController);
        }

        public void RemovePlayerFromActivePlayers(PlayerController playerController)
        {
            var playerItem = enemyDictionary.First(playerPair => playerPair.Value == playerController);

            enemyDictionary.Remove(playerItem.Key);
        }

        private Vector3 GetRandomEnemyPosition()
        {
            float randomXPosition = GetRandomStartPosition();
            float randomZPosition = GetRandomStartPosition();
            Vector3 randomPosition = new Vector3(randomXPosition, 0.5f, randomZPosition);

            return randomPosition;
        }

        private float GetRandomStartPosition()
        {
            return ((float)random.NextDouble() - 0.5f) * 30;
        }
    }
}
