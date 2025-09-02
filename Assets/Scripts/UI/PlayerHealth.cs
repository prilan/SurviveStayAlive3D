using Players;
using TMPro;
using UnityEngine;

namespace SurviveStayAlive
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerText;
        [SerializeField] private TextMeshProUGUI playerNumberText;
        [SerializeField] private TextMeshProUGUI playerHealthText;

        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color activeColor = Color.blue;

        private Player _player;
        private int _playerIndex;

        private void OnDestroy()
        {
            _player.OnSetActive -= OnSetActive;
            _player.OnSetReachedGoal -= OnSetReachedGoal;
        }

        private void Update()
        {
            ShowPlayerHealth(_player);
        }

        public void Init(int playerIndex, Player player)
        {
            _playerIndex = playerIndex;
            _player = player;

            playerNumberText.text = (playerIndex + 1).ToString();
            ShowPlayerHealth(player);

            player.OnSetActive += OnSetActive;
            player.OnSetReachedGoal += OnSetReachedGoal;
        }

        private void OnSetActive(bool isActive)
        {
            var color = isActive ? activeColor : defaultColor;
            playerText.color = color;
            playerNumberText.color = color;
        }

        private void OnSetReachedGoal()
        {
            gameObject.SetActive(false);
        }

        private void ShowPlayerHealth(Player player)
        {
            playerHealthText.text = player.Health.ToString();
        }
    }
}
