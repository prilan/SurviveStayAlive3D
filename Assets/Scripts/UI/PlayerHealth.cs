using Players;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SurviveStayAlive
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playerNumberText;
        [SerializeField] TextMeshProUGUI playerHealthText;

        Player player;

        private int playerIndex;

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
        }

        private void ShowPlayerHealth(Player player)
        {
            playerHealthText.text = player.Health.ToString();
        }
    }
}