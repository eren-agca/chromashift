using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakter hareket hızı
    public bl_Joystick joystick; // Joystick referansı (Inspector’dan atanacak)

    public Vector2 minPosition; // Hareket edebileceği minimum X,Y sınırı
    public Vector2 maxPosition; // Hareket edebileceği maksimum X,Y sınırı

    private Rigidbody2D rb; // Rigidbody2D referansı

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Joystick değerlerini oku
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Hareket yönünü belirle
        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        // Hareket uygula
        rb.linearVelocity = direction.normalized * moveSpeed;

        // Konumu sınırla (clamp)
        float clampedX = Mathf.Clamp(rb.position.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(rb.position.y, minPosition.y, maxPosition.y);
        rb.position = new Vector2(clampedX, clampedY);

        // Z pozisyonunu sıfırla (2D kuş bakışı için)
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}
