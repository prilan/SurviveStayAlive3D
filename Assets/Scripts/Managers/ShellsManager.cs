using System.Collections.Generic;
using Enemies.Controller;
using Enemies.Controller.Shell;
using UnityEngine;
using Utility;
using Random = System.Random;

namespace SurviveStayAlive
{
    public class ShellsManager : MonoSingleton<ShellsManager>
    {
        [SerializeField] ShellController shellControllerPrefab;
        [SerializeField] Transform shellsTransform;

        private readonly Dictionary<int, EnemyController> enemyDictionary = new();

        private int currentDamage;

        private readonly Random random = new();

        public void LaunchShell(int damage, Vector3 startPosition, Vector3 targetPosition)
        {
            var shellController = CreateShell(startPosition);

            currentDamage = damage;

            var deltaVector = targetPosition - startPosition;

            shellController.SetShellDamage(currentDamage);
            shellController.StartMove(deltaVector);
        }

        private ShellController CreateShell(Vector3 startPosition)
        {
            var shellController = Instantiate(shellControllerPrefab, shellsTransform);
            shellController.transform.position = startPosition;
            return shellController;
        }
    }
}
