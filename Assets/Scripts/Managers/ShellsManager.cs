using Enemies;
using Enemies.Shell;
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
    public class ShellsManager : MonoSingleton<ShellsManager>
    {
        [SerializeField] ShellController shellControllerPrefab;
        [SerializeField] Transform shellsTransform;

        private Dictionary<int, EnemyController> enemyDictionary = new Dictionary<int, EnemyController>();

        private int currentDamage;

        private Random random = new Random();

        public void LauchShell(int damage, Vector3 startPosition, Vector3 targetPosition)
        {
            ShellController shellController = CreateShell(startPosition);

            currentDamage = damage;

            Vector3 deltaVector = targetPosition - startPosition;

            shellController.SetShellDamge(currentDamage);
            shellController.StartMove(deltaVector);
        }

        private ShellController CreateShell(Vector3 startPosition)
        {
            ShellController shellController = Instantiate(shellControllerPrefab, shellsTransform);
            shellController.transform.position = startPosition;
            return shellController;
        }

        public void RemoveEnemy(EnemyController enemyController)
        {
            var enemyItem = enemyDictionary.First(enemyPair => enemyPair.Value == enemyController);

            enemyDictionary.Remove(enemyItem.Key);
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
