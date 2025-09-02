using Players;
using SurviveStayAlive;
using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        private AbstractEnemy currentEnemy;
        private Player currentAttackedPlayer;
        private MeshRenderer meshRenderer;
        private Vector3 startPosition;

        public Vector3 StartPosition => startPosition;

        private bool isInitialized;

        public AbstractEnemy Enemy => currentEnemy;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();

            startPosition = transform.position;
        }

        private void OnDestroy()
        {
            currentEnemy.OnAttackDone -= OnAttackDone;
        }

        private void Update()
        {
            if (AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PauseState)
                return;

            if (!isInitialized)
                return;

            ProcessPosition();
        }

        public void SetStartPosition(Vector3 position)
        {
            startPosition = position;
            transform.position = position;

            isInitialized = true;
        }

        private void ProcessPosition()
        {
            currentEnemy.UpdateAction(transform, startPosition);
        }

        public void SetEnemy(AbstractEnemy enemy)
        {
            currentEnemy = enemy;

            SetEnemyColor();
        }

        public void AddListener()
        {
            currentEnemy.OnAttackDone += OnAttackDone;
        }

        private void SetEnemyColor()
        {
            meshRenderer.material.SetColor("_Color", currentEnemy.Color);
        }

        private void OnAttackDone()
        {
            SetActive(false);

            EnemiesManager.Instance.RemoveEnemy(this);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
