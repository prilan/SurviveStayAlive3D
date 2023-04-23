using Players;
using System;
using TMPro;
using UnityEngine;

namespace SurviveStayAlive
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playerText;
        [SerializeField] TextMeshProUGUI playerNumberText;
        [SerializeField] TextMeshProUGUI playerHealthText;

        [SerializeField] Color defaultColor = Color.white;
        [SerializeField] Color activeColor = Color.blue;

        Player player;

        private int playerIndex;

        private void OnDestroy()
        {
            player.OnSetActive -= OnSetActive;
            player.OnSetReachedGoal -= OnSetReachedGoal;
        }

        private void Update()
        {
            ShowPlayerHealth(player);
        }

        public void Init(int playerIndex, Player player)
        {
            this.playerIndex = playerIndex;
            this.player = player;

            playerNumberText.text = (playerIndex + 1).ToString();
            ShowPlayerHealth(player);

            player.OnSetActive += OnSetActive;
            player.OnSetReachedGoal += OnSetReachedGoal;
        }

        private void OnSetActive(bool isActive)
        {
            Color color = isActive ? activeColor : defaultColor;
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