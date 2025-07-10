using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;                // Hareket hızı
    public bl_Joystick joystick;                // Joystick referansı

    public Vector2 minPosition;                 // Minimum (X, Y) sınırları
    public Vector2 maxPosition;                 // Maksimum (X, Y) sınırları

    private Rigidbody2D rb;                     // Rigidbody2D referansı

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // X ve Y ekseninde hareket vektörü oluştur
        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        // Rigidbody2D ile hareket uygula
        rb.linearVelocity = direction.normalized * moveSpeed;

        // Pozisyonu sınırla
        float clampedX = Mathf.Clamp(rb.position.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(rb.position.y, minPosition.y, maxPosition.y);
        rb.position = new Vector2(clampedX, clampedY);

        // Z'yi sıfırla (güvence için)
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}