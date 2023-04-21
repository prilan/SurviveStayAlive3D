using Enemies;
using SurviveStayAlive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Players
{
    public class EnemyController : MonoBehaviour
    {
        private Enemy currentEnemy;

        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PauseState)
                return;

            if (PlayersManager.Instance.CurrentPlayerController != this)
                return;

            ProcessPosition();
        }

        private void ProcessPosition()
        {
            //
        }

        public void SetEnemy(Enemy enemy)
        {
            currentEnemy = enemy;

            SetEnemyColor();
        }

        private void SetEnemyColor()
        {
            meshRenderer.material.SetColor("_Color", currentEnemy.Color);
        }
    }
}
