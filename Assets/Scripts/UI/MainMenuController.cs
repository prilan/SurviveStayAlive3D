using EventEmitter;
using Players;
using SurviveStayAlive;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SurviveStayAlive
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] Image background;
        [SerializeField] TextMeshProUGUI messageText;

        [SerializeField] Transform playerHealthGridTransform;
        [SerializeField] PlayerHealth playerHealthPrefab;

        private string pauseText = "PAUSE";

        private void Start()
        {
            GameEventEmitter.PlayersInited += OnPlayersInited;
        }

        private void OnDestroy()
        {
            GameEventEmitter.PlayersInited -= OnPlayersInited;
        }

        private void Update()
        {
            ProcessPauseUnPause();
        }

        private void OnPlayersInited()
        {
            foreach (var playerItem in AppModel.Instance.PlayerDictionary) {
                int playerIndex = playerItem.Key;
                Player player = playerItem.Value;
                PlayerHealth playerHealth = Instantiate(playerHealthPrefab, playerHealthGridTransform);
                playerHealth.Init(playerIndex, player);
            }
        }

        private void ProcessPauseUnPause()
        {
            if (AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PauseState) {
                if (!messageText.isActiveAndEnabled) {
                    messageText.text = pauseText;
                    messageText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                }
            } else if (AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PlayState) {
                if (messageText.isActiveAndEnabled) {
                    messageText.gameObject.SetActive(false);
                    background.gameObject.SetActive(false);
                }
            }
        }
    }
}