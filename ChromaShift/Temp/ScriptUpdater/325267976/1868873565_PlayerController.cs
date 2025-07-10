using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;              // Hareket hızı
    public bl_Joystick joystick;              // Joystick referansı

    public Vector2 minPosition;               // X ve Z sınırları için
    public Vector2 maxPosition;

    private Rigidbody rb;                     // Rigidbody referansı

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Hareket yönünü X (sağ-sol), Z (ileri-geri) olarak oluştur
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        // Rigidbody ile hareket uygula
        rb.linearVelocity = direction.normalized * moveSpeed;

        // Pozisyonu sınırla (X ve Z eksenlerinde)
        float clampedX = Mathf.Clamp(rb.position.x, minPosition.x, maxPosition.x);
        float clampedZ = Mathf.Clamp(rb.position.z, minPosition.y, maxPosition.y); // minPosition.y burada minZ'dir

        rb.position = new Vector3(clampedX, 0, clampedZ);
    }
}