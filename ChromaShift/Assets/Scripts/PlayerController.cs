using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlayerColor
{
    Red,
    Blue
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bl_Joystick joystick;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    public float movementDeadzone = 0.12f;
    public PlayerColor currentColor = PlayerColor.Red;

    public TilemapCollider2D redWallCollider;
    public TilemapCollider2D blueWallCollider;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        UpdateColor();
        UpdateWallColliders(); // Başlangıçta doğru collider aktif olsun
    }

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        if (direction.magnitude < movementDeadzone)
            direction = Vector2.zero;

        animator.SetBool("isRunning", direction.magnitude > 0f);

        if (direction.x < -0.01f)
            spriteRenderer.flipX = true;
        else if (direction.x > 0.01f)
            spriteRenderer.flipX = false;

        if (direction.magnitude > 0f)
        {
            Vector3 newPosition = transform.position + new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
            float clampedX = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
            float clampedY = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);
            transform.position = new Vector3(clampedX, clampedY, 0);
        }
    }

    public void SetRedColor()
    {
        currentColor = PlayerColor.Red;
        UpdateColor();
        UpdateWallColliders();
    }

    public void SetBlueColor()
    {
        currentColor = PlayerColor.Blue;
        UpdateColor();
        UpdateWallColliders();
    }

    private void UpdateColor()
    {
        if (currentColor == PlayerColor.Red)
            spriteRenderer.color = Color.red;
        else if (currentColor == PlayerColor.Blue)
            spriteRenderer.color = Color.blue;
    }

    // En doğru yöntem: Collider'ı devre dışı bırakmak!
    private void UpdateWallColliders()
    {
        if (redWallCollider != null)
            redWallCollider.enabled = (currentColor != PlayerColor.Red);  // Kırmızıdaysa kırmızı duvar yokmuş gibi olur
        if (blueWallCollider != null)
            blueWallCollider.enabled = (currentColor != PlayerColor.Blue); // Maviyse mavi duvar yokmuş gibi olur
    }
}
