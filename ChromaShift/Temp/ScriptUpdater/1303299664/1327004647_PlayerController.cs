using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bl_Joystick joystick;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        if (direction.magnitude > 0.01f)
        {
            rb.linearVelocity = direction.normalized * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Joystick bırakıldığında karakter ANINDA DURUR
        }

        // Pozisyonu sınırla
        float clampedX = Mathf.Clamp(rb.position.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(rb.position.y, minPosition.y, maxPosition.y);
        rb.position = new Vector2(clampedX, clampedY);

        // Z'yi sıfırla (güvence için)
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}