using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.hasKey = true;

            // Artık UIManager.Instance yerine GameManager üzerinden UI'a ulaşıyoruz.
            // Bu çok daha güvenli bir yöntem.
            if (UIManager.Instance != null){
                UIManager.Instance.ShowKeyIcon();

            }

            Destroy(gameObject);
        }
    }
}