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

    public TilemapCollider2D redwallCollider;
    public TilemapCollider2D bluewallCollider;

    [HideInInspector] public bool hasKey = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        UpdateColor();
        UpdateWallColliders();
    }

    void Update()
    {
        float horizontalInput = joystick != null ? joystick.Horizontal : 0f;
        float verticalInput = joystick != null ? joystick.Vertical : 0f;
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
        if (spriteRenderer != null)
        {
            if (currentColor == PlayerColor.Red)
                spriteRenderer.color = Color.red;
            else if (currentColor == PlayerColor.Blue)
                spriteRenderer.color = Color.blue;
        }
    }

    private void UpdateWallColliders()
    {
        if (redwallCollider != null)
            redwallCollider.enabled = (currentColor != PlayerColor.Red);
        if (bluewallCollider != null)
            bluewallCollider.enabled = (currentColor != PlayerColor.Blue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Anahtar kontrolü
        if (other.GetComponent<KeyController>() != null)
        {
            hasKey = true;
            Destroy(other.gameObject);
            if (UIManager.Instance != null)
                UIManager.Instance.ShowKeyIcon();
            return;
        }

        // Düşman (iskelet) kontrolü
        SkeletonEnemy skeleton = other.GetComponent<SkeletonEnemy>();
        if (skeleton != null)
        {
            var skeletonRenderer = skeleton.GetComponent<SpriteRenderer>();
            if (skeletonRenderer != null)
            {
                Color playerColor = (currentColor == PlayerColor.Red) ? Color.red : Color.blue;
                if (Approximately(playerColor, skeletonRenderer.color))
                {
                    skeleton.Disappear();
                }
                else
                {
                    
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level3")
                        GameManager.Instance.GameOver();
                    else
                        GameManager.Instance.GameOver(); 
                }
            }
        }
    }

    private bool Approximately(Color a, Color b)
    {
        return Mathf.Approximately(a.r, b.r) &&
               Mathf.Approximately(a.g, b.g) &&
               Mathf.Approximately(a.b, b.b);
    }
}
