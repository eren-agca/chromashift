using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nextLevelName = "Level2"; // Inspector’dan düzenleyebilirsin

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player objesinin tag’ı “Player” olmalı
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}