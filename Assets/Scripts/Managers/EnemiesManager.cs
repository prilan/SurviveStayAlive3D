using System.Linq;
using System.Collections.Generic;
using DataModel;
using Enemies;
using Enemies.Controller;
using UnityEngine;
using Utility;
using Random = System.Random;

namespace SurviveStayAlive
{
    public class EnemiesManager : MonoSingleton<EnemiesManager>
    {
        [SerializeField] private EnemyController enemyControllerPrefab;
        [SerializeField] private Transform enemiesTransform;

        private readonly Dictionary<int, EnemyController> enemyDictionary = new();

        private readonly Random random = new();

        public Dictionary<int, EnemyController> Enemies => enemyDictionary;

        public void CreateEnemy()
        {
            var index = enemyDictionary.Count;
            var enemyController = Instantiate(enemyControllerPrefab, enemiesTransform);
            var enemy = AppModel.Instance.GetEnemyByIndex(index);
            enemyController.SetEnemy(enemy);
            enemyController.AddListener();
            enemyController.SetStartPosition(GetRandomEnemyPosition());
            enemyDictionary.Add(index, enemyController);
        }

        public void UpdateEnemyFromState(int index, EnemyFormat enemyFormat)
        {
            var enemyController = enemyDictionary[index];
            var enemy = AppModel.Instance.GetEnemyByIndex(index);
            enemy.EnemyType = EnemyUtility.GetEnumValueFromDescription<EnemyType>(enemyFormat.enemyType);
            enemyController.SetEnemy(enemy);
            enemyController.SetStartPosition(enemyFormat.position);
        }

        public void RemoveEnemies()
        {
            foreach (EnemyController enemyController in enemyDictionary.Values) {
                Destroy(enemyController.gameObject);
            }

            enemyDictionary.Clear();
        }

        public void RemoveEnemy(EnemyController enemyController)
        {
            if (enemyDictionary.Any(enemyPair => enemyPair.Value == enemyController)) {
                var enemyItem = enemyDictionary.First(enemyPair => enemyPair.Value == enemyController);
                enemyDictionary.Remove(enemyItem.Key);
            }
        }

        private Vector3 GetRandomEnemyPosition()
        {
            var randomXPosition = GetRandomStartPosition();
            var randomZPosition = GetRandomStartPosition();
            var randomPosition = new Vector3(randomXPosition, 0.5f, randomZPosition);

            return randomPosition;
        }

        private float GetRandomStartPosition()
        {
            return ((float)random.NextDouble() - 0.5f) * 30;
        }
    }
}
