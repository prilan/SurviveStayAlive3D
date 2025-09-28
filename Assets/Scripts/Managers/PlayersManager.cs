using System.Collections.Generic;
using System.Linq;
using DataModel;
using Players;
using UnityEngine;
using Utility;
using Random = System.Random;

namespace SurviveStayAlive
{
    public class PlayersManager : MonoSingleton<PlayersManager>
    {
        private const float START_PLAYER_POSITION_HEIGHT = 0.5f;
        private const float MAX_START_POSITION_VALUE = 5;

        [SerializeField] private PlayerController playerControllerPrefab;
        [SerializeField] private Transform playersTransform;

        private readonly Dictionary<int, PlayerController> playerDictionary = new();
        private readonly Dictionary<int, PlayerController> removePlayerDictionary = new();

        private readonly Random random = new();

        public PlayerController CurrentPlayerController { get; private set; }

        public Dictionary<int, PlayerController> Players => playerDictionary;

        public bool IsActivePlayersEmpty => playerDictionary.Values.Count == 0;

        public Dictionary<int, PlayerController> PlayerDictionary => playerDictionary;

        public void CreatePlayer()
        {
            var index = playerDictionary.Count;
            var playerController = Instantiate(playerControllerPrefab, playersTransform);
            var player = AppModel.Instance.GetPlayerByIndex(index);
            playerController.SetPlayer(player);
            playerController.transform.position = GetRandomPlayerPosition();
            playerDictionary.Add(index, playerController);

            if (CurrentPlayerController == null) {
                CurrentPlayerController = playerDictionary.Values.First();
            }
        }

        public void UpdatePlayerFromState(int index, PlayerFormat playerFormat)
        {
            var playerController = playerDictionary[index];
            var player = AppModel.Instance.GetPlayerByIndex(index);
            playerController.transform.position = playerFormat.position;
            player.Health = playerFormat.health;
        }

        public void RemovePlayers()
        {
            foreach (var playerController in playerDictionary.Values) {
                Destroy(playerController.gameObject);
            }

            foreach (var playerController in removePlayerDictionary.Values) {
                Destroy(playerController.gameObject);
            }

            playerDictionary.Clear();
            removePlayerDictionary.Clear();

            ResetCurrentPlayerController();
        }

        public void ResetCurrentPlayerController()
        {
            CurrentPlayerController = null;
        }

        public void SetNextCurrentPlayer()
        {
            if (playerDictionary.Count < 1)
                return;

            if (CurrentPlayerController == null) {
                if (playerDictionary.Count > 0) {
                    CurrentPlayerController = playerDictionary.Values.First();
                    
                    UpdateCurrentPlayerColors();

                    return;
                }
            }

            var currentPlayerIndex = playerDictionary.Where(playerPair => playerPair.Value == CurrentPlayerController)
                .Select(playerPair => playerPair.Key)
                .FirstOrDefault();

            int nextPlayerIndex;
            if (currentPlayerIndex >= playerDictionary.Count - 1) {
                nextPlayerIndex = 0;
            } else {
                nextPlayerIndex = currentPlayerIndex + 1;
            }

            if (!playerDictionary.ContainsKey(nextPlayerIndex))
                return;

            CurrentPlayerController = playerDictionary[nextPlayerIndex];

            UpdateCurrentPlayerColors();
        }

        private void UpdateCurrentPlayerColors()
        {
            foreach (var playerPair in playerDictionary) {
                var player = playerPair.Value;
                var isCurrentPlayer = player.Equals(CurrentPlayerController);
                player.SetPlayerActive(isCurrentPlayer);
                player.Player.OnSetActive(isCurrentPlayer);
            }
        }

        public void RemovePlayerFromActivePlayers(PlayerController playerController)
        {
            var playerItem = playerDictionary.First(playerPair => playerPair.Value == playerController);

            removePlayerDictionary[playerItem.Key] = playerItem.Value;

            playerDictionary.Remove(playerItem.Key);
        }

        private Vector3 GetRandomPlayerPosition()
        {
            var randomXPosition = GetRandomStartPosition();
            var randomZPosition = GetRandomStartPosition();
            var randomPosition = new Vector3(randomXPosition, START_PLAYER_POSITION_HEIGHT, randomZPosition);

            return randomPosition;
        }

        private float GetRandomStartPosition()
        {
            return ((float)random.NextDouble() - 0.5f) * MAX_START_POSITION_VALUE;
        }
    }
}
