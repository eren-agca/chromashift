using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject levelCompletePanel;
    public GameObject gameFinishedPanel;
    public GameObject KeyIcon;
    public GameObject youDiedPanel;
    public Button playAgainButton;
    public Button mainMenuButton;
    public PlayerController playerController;

    void Awake()
    {
        Instance = this;
        HideKeyIcon();

        if (playAgainButton != null)
        {
            playAgainButton.onClick.RemoveAllListeners();
            playAgainButton.onClick.AddListener(RestartLevel);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(Button_GoToMainMenu);
        }
    }

    public void ShowLevelCompletePanel()
    {
        Time.timeScale = 0f;
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);
    }

    public void ShowGameFinishedPanel()
    {
        Time.timeScale = 0f;
        if (gameFinishedPanel != null)
            gameFinishedPanel.SetActive(true);
    }

    public void ShowYouDiedPanel()
    {
        Time.timeScale = 0f;
        if (youDiedPanel != null)
            youDiedPanel.SetActive(true);
    }

    public void HideAllPanels()
    {
        Time.timeScale = 1f;
        if (levelCompletePanel != null) levelCompletePanel.SetActive(false);
        if (gameFinishedPanel != null) gameFinishedPanel.SetActive(false);
        if (youDiedPanel != null) youDiedPanel.SetActive(false);
    }

    public void ShowKeyIcon()
    {
        if (KeyIcon != null)
            KeyIcon.SetActive(true);
    }

    public void HideKeyIcon()
    {
        if (KeyIcon != null)
            KeyIcon.SetActive(false);
    }

    public void Button_SetRedColor()
    {
        if (playerController != null)
            playerController.SetRedColor();
    }

    public void Button_SetBlueColor()
    {
        if (playerController != null)
            playerController.SetBlueColor();
    }

    public void Button_GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Button_GoToLevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelectMenu");
    }

    public void Button_GoToNextLevel()
    {
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
