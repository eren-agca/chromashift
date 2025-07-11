using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject levelCompletePanel;
    public GameObject gameFinishedPanel;

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

    public void HideAllPanels()
    {
        Time.timeScale = 1f;
        if (levelCompletePanel != null) levelCompletePanel.SetActive(false);
        if (gameFinishedPanel != null) gameFinishedPanel.SetActive(false);
    }

    public void Button_GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Button_GoToLevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }

    public void Button_GoToNextLevel()
    {
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
    }
}