using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GamePlayPauseMenu pauseMenuUI;
    [SerializeField] private GameOverUI gameOverUI;

    private GameUIState currentUIState;

    private void Awake()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventsModel.UPDATE_PLAYER_HEALTH += UpdatePlayerHealthUI;
        EventsModel.COINS_ECONOMY_CHANGED += UpdateCoinText;
        EventsModel.ON_PAUSE_CLICKED += TogglePauseMenu;
        EventsModel.ON_RESUME_CLICKED += TogglePauseMenu;
        EventsModel.PLAYER_DEAD += OnPlayerDead;
        EventsModel.ALL_REGIONS_CLEARED += OnGameFinished;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.UPDATE_PLAYER_HEALTH -= UpdatePlayerHealthUI;
        EventsModel.COINS_ECONOMY_CHANGED -= UpdateCoinText;
        EventsModel.ON_PAUSE_CLICKED -= TogglePauseMenu;
        EventsModel.ON_RESUME_CLICKED -= TogglePauseMenu;
        EventsModel.PLAYER_DEAD -= OnPlayerDead;
        EventsModel.ALL_REGIONS_CLEARED -= OnGameFinished;
    }

    private void Start()
    {
        UpdateCoinText();
        currentUIState = GameUIState.GamePlay;
    }

    private void UpdatePlayerHealthUI(int currentHealth) => healthSlider.value = (float)currentHealth / Constants.MAX_HEALTH;

    private void UpdateCoinText()
    {
        coinText.text = $"{DI.di.economy.coins}";
    }

    private void TogglePauseMenu()
    {
        switch (currentUIState)
        {
            case GameUIState.GamePlay:
                pauseMenuUI.gameObject.SetActive(true);
                currentUIState = GameUIState.Pause;
                Time.timeScale = 0;
                break;
            case GameUIState.Pause:
                pauseMenuUI.gameObject.SetActive(false);
                currentUIState = GameUIState.GamePlay;
                Time.timeScale = 1;
                break;
        }
    }

    private void OnPlayerDead()
    {
        Time.timeScale = 0;
        currentUIState = GameUIState.GameOver;
        gameOverUI.SetGameOverStatus(false);
        gameOverUI.gameObject.SetActive(true);
    }

    private void OnGameFinished()
    {
        Time.timeScale = 0;
        currentUIState = GameUIState.GameFinished;
        gameOverUI.SetGameOverStatus(true);
        gameOverUI.gameObject.SetActive(false);
    }


    private void OnDestroy() => UnsubscribeEvents();

    public enum GameUIState
    {
        GamePlay,
        Pause,
        GameOver,
        GameFinished
    }


}
