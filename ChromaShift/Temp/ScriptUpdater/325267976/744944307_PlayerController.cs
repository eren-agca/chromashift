using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;              // Karakterin hareket hızı
    public bl_Joystick joystick;              // Joystick referansı

    public Vector2 minPosition;               // X ve Z için minimum değerler (örn: x:-8, y:-4)
    public Vector2 maxPosition;               // X ve Z için maksimum değerler (örn: x:8, y:4)

    private Rigidbody rb;                     // Rigidbody referansı

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        // JOYSTICK SIFIRSA ANINDA DURDUR (kayma engelleyici)
        if (direction.magnitude < 0.01f)
        {
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            rb.linearVelocity = direction.normalized * moveSpeed;
        }

        // X ve Z ekseninde pozisyonu sınırla
        float clampedX = Mathf.Clamp(rb.position.x, minPosition.x, maxPosition.x);
        float clampedZ = Mathf.Clamp(rb.position.z, minPosition.y, maxPosition.y);
        rb.position = new Vector3(clampedX, 0, clampedZ);
    }
}