using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;                // Hareket hızı
    public bl_Joystick joystick;                // Kendi joystick referansını kullan

    private Rigidbody2D rb;                     // Fizik motoru için referans

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Joystick değerlerini oku
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Hareket yönünü vektör olarak oluştur
        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        // Rigidbody2D ile hareket uygula
        rb.linearVelocity = direction.normalized * moveSpeed;
    }
}