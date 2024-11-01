using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Image headingImage;

    [SerializeField] private Sprite wonSprite;
    [SerializeField] private Sprite lostSprite;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        restartButton.onClick.AddListener(OnRestartClicked);
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneConstants.MAIN_MENU);
    }

    private void OnRestartClicked()
    {
        SceneManager.LoadSceneAsync(SceneConstants.GAMEPLAY);
    }

    public void SetGameOverStatus(bool isWon)
    {
        if (isWon)
            headingImage.sprite = wonSprite;
        else
            headingImage.sprite = lostSprite;
    }
}
