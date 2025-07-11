using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (currentScene == "Level3") // 3. levelde oyunu bitir
            {
                GameManager.Instance.GameFinished();
            }
            else
            {
                GameManager.Instance.LevelComplete();
            }
        }
    }
}