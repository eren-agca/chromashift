using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            // --- ANAHTAR KONTROLÜ ---
            // SADECE 2. level ve gerekirse diğer level'larda kontrol et
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            // Eğer Level 2 veya anahtar gerektiren başka level ise:
            if (sceneName == "Level2" || sceneName == "LevelX") // Diğer level isimleri de eklenebilir
            {
                if (!player.hasKey)
                {
                    Debug.Log("Anahtar yok, portala girilemez!");
                    return;
                }
            }

            // Geçiş izni varsa:
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
