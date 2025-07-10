using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hareket hızı
    public bl_Joystick joystick; // Joystick referansı

    public Vector2 minPosition; // X ve Y için minimum sınırlar
    public Vector2 maxPosition; // X ve Y için maksimum sınırlar

    private Rigidbody2D rb; // Rigidbody2D referansı

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void FixedUpdate()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Debug.Log("Joystick: " + horizontalInput + ", " + verticalInput); // BURADA

        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        if (direction.magnitude > 0.01f)
        {
            rb.linearVelocity = direction.normalized * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        float clampedX = Mathf.Clamp(rb.position.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(rb.position.y, minPosition.y, maxPosition.y);
        rb.position = new Vector2(clampedX, clampedY);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

}