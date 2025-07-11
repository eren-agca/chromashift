using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    [Tooltip("Bu portalı kullanmak için anahtar gerekiyor mu?")]
    public bool requiresKey = false;

    [Tooltip("Bu, oyunu bitiren son portal mı?")]
    public bool isFinalPortal = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        if (requiresKey && !player.hasKey)
        {
            Debug.Log("Key is missing! Cannot use the portal.");
            return;
        }

        if (isFinalPortal)
        {
            if (GameManager.Instance != null) GameManager.Instance.GameFinished();
        }
        else
        {
            if (GameManager.Instance != null) GameManager.Instance.LevelComplete();
        }
    }
}