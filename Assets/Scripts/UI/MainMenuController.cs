using EventEmitter;
using Players;
using SaveSystem;
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

        [SerializeField] Button saveButton;
        [SerializeField] Button loadButton;

        private string pauseText = "PAUSE";
        private string winText = "YOU WIN";

        private void Awake()
        {
            GameEventEmitter.PlayersInited += OnPlayersInited;
            GameEventEmitter.WinLevel += OnWinLevel;

            saveButton.onClick.AddListener(OnSaveButtonClicked);
            loadButton.onClick.AddListener(OnLoadButtonClicked);
        }

        private void OnDestroy()
        {
            GameEventEmitter.PlayersInited -= OnPlayersInited;
            GameEventEmitter.WinLevel -= OnWinLevel;
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

        private void OnWinLevel()
        {
            SetMessage(true, winText);
        }

        private void OnSaveButtonClicked()
        {
            SaveStateManager.Instance.SaveDataState(AppModel.Instance.SaveDataState);
        }

        private void OnLoadButtonClicked()
        {
            if (SaveStateManager.Instance.HasSaveDataState())
                AppModel.Instance.SaveDataState = SaveStateManager.Instance.LoadDataState();
        }

        private void ProcessPauseUnPause()
        {
            if (AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PauseState) {
                if (!messageText.isActiveAndEnabled) {
                    SetMessage(true, pauseText);
                }
            } else if (AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PlayState) {
                if (messageText.isActiveAndEnabled) {
                    SetMessage(false);
                }
            }
        }

        private void SetMessage(bool isActive, string message = null)
        {
            if (message != null)
                messageText.text = message;

            messageText.gameObject.SetActive(isActive);
            background.gameObject.SetActive(isActive);
        }
    }
}