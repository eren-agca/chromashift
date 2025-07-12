using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            
            if (sceneName == "Level2" || sceneName == "LevelX") 
            {
                if (!player.hasKey)
                {
                    Debug.Log("Anahtar yok, portala girilemez!");
                    return;
                }
            }

            
            if (sceneName == "Level3")
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
