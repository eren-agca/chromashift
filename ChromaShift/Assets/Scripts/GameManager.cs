using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager uiManager;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        Time.timeScale = 1f;
    }

    public void LevelComplete()
    {
        int currentLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;

        if (PlayerPrefs.GetInt("LevelUnlocked", 1) < nextLevelIndex)
        {
            PlayerPrefs.SetInt("LevelUnlocked", nextLevelIndex);
            PlayerPrefs.Save(); // Burası önemli!
        }

        if (uiManager != null)
            uiManager.ShowLevelCompletePanel();
    }

    public void GameFinished()
    {
        if (uiManager != null)
            uiManager.ShowGameFinishedPanel();
    }

    public void GameOver()
    {
        if (uiManager != null)
            uiManager.ShowYouDiedPanel();
    }

    // TEST için bir butona bu fonksiyonu bağlayabilirsin:
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("LevelUnlocked");
        PlayerPrefs.Save();
    }
}