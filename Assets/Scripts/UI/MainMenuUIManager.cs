using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void Start() => Cursor.lockState = CursorLockMode.Confined;
    private void OnStartButtonClicked() => SceneManager.LoadScene(SceneConstants.GAMEPLAY);

    private void OnQuitButtonClicked() => Application.Quit();
}
