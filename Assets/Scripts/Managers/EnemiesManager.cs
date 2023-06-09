﻿using DataModel;
using Enemies;
using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public Dictionary<int, EnemyController> Enemies => enemyDictionary;

        public void CreateEnemy()
        {
            int index = enemyDictionary.Count;
            EnemyController enemyController = Instantiate(enemyControllerPrefab, enemiesTransform);
            AbstractEnemy enemy = AppModel.Instance.GetEnemyByIndex(index);
            enemyController.SetEnemy(enemy);
            enemyController.AddListener();
            enemyController.SetStartPosition(GetRandomEnemyPosition());
            enemyDictionary.Add(index, enemyController);
        }

        public void UpdateEnemyFromState(int index, EnemyFormat enemyFormat)
        {
            EnemyController enemyController = enemyDictionary[index];
            AbstractEnemy enemy = AppModel.Instance.GetEnemyByIndex(index);
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
