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
    public class PlayersManager : MonoSingleton<PlayersManager>
    {
        [SerializeField] PlayerController playerControllerPrefab;
        [SerializeField] Transform playersTransform;

        private Dictionary<int, PlayerController> playerDictionary = new Dictionary<int, PlayerController>();

        private PlayerController currentPlayerController;

        private Random random = new Random();

        public PlayerController CurrentPlayerController { get {  return currentPlayerController; } }

        public Dictionary<int, PlayerController> Players => playerDictionary;

        public bool IsActivePlayersEmpty
        {
            get
            {
                return playerDictionary.Values.Count == 0;
            }
        }

        public Dictionary<int, PlayerController> PlayerDictionary => playerDictionary;

        public void CreatePlayer()
        {
            int index = playerDictionary.Count;
            PlayerController playerController = Instantiate(playerControllerPrefab, playersTransform);
            Player player = AppModel.Instance.GetPlayerByIndex(index);
            playerController.SetPlayer(player);
            playerController.transform.position = GetRandomPlayerPosition();
            playerDictionary.Add(index, playerController);

            if (currentPlayerController == null) {
                currentPlayerController = playerDictionary.Values.First();
            }
        }

        public void ResetCurrentPlayerController()
        {
            currentPlayerController = null;
        }

        public void SetNextCurrentPlayer()
        {
            if (playerDictionary.Count < 1)
                return;

            if (currentPlayerController == null) {
                if (playerDictionary.Count > 0) {
                    currentPlayerController = playerDictionary.Values.First();
                    
                    UpdateCurrentPlayerColors();

                    return;
                }
            }

            int currentPlayerIndex = playerDictionary.Where(playePair => playePair.Value == currentPlayerController)
                    .Select(playePair => playePair.Key)
                    .FirstOrDefault();

            int nextPlayerIndex;
            if (currentPlayerIndex >= playerDictionary.Count - 1) {
                nextPlayerIndex = 0;
            } else {
                nextPlayerIndex = currentPlayerIndex + 1;
            }

            if (!playerDictionary.ContainsKey(nextPlayerIndex))
                return;

            currentPlayerController = playerDictionary[nextPlayerIndex];

            UpdateCurrentPlayerColors();
        }

        private void UpdateCurrentPlayerColors()
        {
            foreach (var playerPair in playerDictionary) {
                PlayerController player = playerPair.Value;
                bool isCurrentPlayer = player.Equals(currentPlayerController);
                player.SetPlayerActive(isCurrentPlayer);
                player.Player.OnSetActive(isCurrentPlayer);
            }
        }

        public void RemovePlayerFromActivePlayers(PlayerController playerController)
        {
            var playerItem = playerDictionary.First(playerPair => playerPair.Value == playerController);

            playerDictionary.Remove(playerItem.Key);
        }

        private Vector3 GetRandomPlayerPosition()
        {
            float randomXPosition = GetRandomStartPosition();
            float randomZPosition = GetRandomStartPosition();
            Vector3 randomPosition = new Vector3(randomXPosition, 0.5f, randomZPosition);

            return randomPosition;
        }

        private float GetRandomStartPosition()
        {
            return ((float)random.NextDouble() - 0.5f) * 5;
        }
    }
}
