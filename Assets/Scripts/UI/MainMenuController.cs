using System.Collections;
using System.Collections.Generic;
using EventEmitter;
using SaveSystem;
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
        [SerializeField] TMP_InputField slotInputField;

        [SerializeField] Button playerPrefsButton;
        [SerializeField] Button jsonButton;

        private int CurrentSlot = 1;

        private readonly Dictionary<int, PlayerHealth> playerHealthDictionary = new();

        private const string pauseText = "PAUSE";
        private const string winText = "YOU WIN";
        private const string loseText = "YOU FAIL";

        private void Awake()
        {
            GameEventEmitter.PlayersInited += OnPlayersInited;
            GameEventEmitter.RestartLevel += OnRestartLevel;
            GameEventEmitter.WinLevel += OnWinLevel;
            GameEventEmitter.LoseLevel += OnLoseLevel;

            saveButton.onClick.AddListener(OnSaveButtonClicked);
            loadButton.onClick.AddListener(OnLoadButtonClicked);
            slotInputField.onValueChanged.AddListener(OnSlotValueChanged);
            playerPrefsButton.onClick.AddListener(OnPlayerPrefsButton);
            jsonButton.onClick.AddListener(OnJsonButton);
        }

        private void OnDestroy()
        {
            GameEventEmitter.PlayersInited -= OnPlayersInited;
            GameEventEmitter.RestartLevel -= OnRestartLevel;
            GameEventEmitter.WinLevel -= OnWinLevel;
            GameEventEmitter.LoseLevel -= OnLoseLevel;
        }

        private void Update()
        {
            ProcessPauseUnPause();
        }

        private void OnPlayersInited()
        {
            foreach (var playerItem in AppModel.Instance.PlayerDictionary) {
                var playerIndex = playerItem.Key;
                var player = playerItem.Value;
                var playerHealth = Instantiate(playerHealthPrefab, playerHealthGridTransform);
                playerHealth.Init(playerIndex, player);
                playerHealthDictionary[playerIndex] = playerHealth;
            }
        }

        private void OnRestartLevel()
        {
            foreach(var playerHealthItem in playerHealthDictionary.Values) {
                Destroy(playerHealthItem.gameObject);
            }
            
            playerHealthDictionary.Clear();

            OnPlayersInited();
        }

        private void OnWinLevel()
        {
            SetMessage(true, winText);

            StartCoroutine(CoLoadLevelAfterDelay());
        }

        private void OnLoseLevel()
        {
            SetMessage(true, loseText);

            StartCoroutine(CoRestartLevelAfterDelay());
        }

        private void OnSaveButtonClicked()
        {
            AppModel.Instance.UpdateDataState();
            SaveStateManager.Instance.SaveSystem.SaveDataState(AppModel.Instance.SaveDataState, CurrentSlot);
        }

        private void OnLoadButtonClicked()
        {
            if (SaveStateManager.Instance.SaveSystem.HasSaveDataState(CurrentSlot)) {
                AppModel.Instance.SaveDataState = SaveStateManager.Instance.SaveSystem.LoadDataState(CurrentSlot);
                GameModel.Instance.GameController.LoadSavedState();
            }
        }

        private void OnSlotValueChanged(string slotValue)
        {
            if (IsSlotCorrect(slotInputField.text)) {
                SetActiveSaveLoadButtons(true);
            } else {
                slotInputField.text = string.Empty;
                SetActiveSaveLoadButtons(false);
            }
        }

        private void OnPlayerPrefsButton()
        {
            SaveStateManager.Instance.ChangeSaveSystem(new PlayerPrefsSaveSystem());
        }

        private void OnJsonButton()
        {
            SaveStateManager.Instance.ChangeSaveSystem(new JsonSaveSystem());
        }

        private void SetActiveSaveLoadButtons(bool isActive)
        {
            saveButton.interactable = isActive;
            loadButton.interactable = isActive;
        }

        private bool IsSlotCorrect(string slotValue)
        {
            var isNumber = int.TryParse(slotValue, out var intValue);
            if (!isNumber) {
                CurrentSlot = -1;
                return false;
            }

            if (intValue is < 1 or > 3) {
                slotInputField.text = string.Empty;
                CurrentSlot = -1;
                return false;
            }

            // Правильное значение 1, 2 или 3
            CurrentSlot = intValue;
            return true;
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

        private IEnumerator CoRestartLevelAfterDelay()
        {
            yield return new WaitForSeconds(2f);

            SetMessage(false);
            GameModel.Instance.GameController.RestartLevel();
        }

        private IEnumerator CoLoadLevelAfterDelay()
        {
            yield return new WaitForSeconds(2f);

            SetMessage(false);
            GameModel.Instance.GameController.LoadNextLevel();
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
