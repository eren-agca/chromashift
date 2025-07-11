using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager uiManager; // Inspector’dan atayacaksın!

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        Time.timeScale = 1f;
    }

    public void LevelComplete()
    {
        if (uiManager != null)
            uiManager.ShowLevelCompletePanel();
    }

    public void GameFinished()
    {
        if (uiManager != null)
            uiManager.ShowGameFinishedPanel();
    }
}