using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.hasKey = true;

            
            if (UIManager.Instance != null){
                UIManager.Instance.ShowKeyIcon();

            }

            Destroy(gameObject);
        }
    }
}