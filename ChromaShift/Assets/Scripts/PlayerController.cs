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

    // Tilemap Collider 2D referansları (Inspector’dan atayacaksın)
    public TilemapCollider2D redwallCollider;
    public TilemapCollider2D bluewallCollider;

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

    private void UpdateWallColliders()
    {
        if (redwallCollider != null)
            redwallCollider.enabled = (currentColor != PlayerColor.Red);

        if (bluewallCollider != null)
            bluewallCollider.enabled = (currentColor != PlayerColor.Blue);
    }

    // İskeletle çarpışınca aynı renkteyse yok olur, farklıysa oyun biter
    private void OnTriggerEnter2D(Collider2D other)
    {
        SkeletonEnemy skeleton = other.GetComponent<SkeletonEnemy>();
        if (skeleton != null)
        {
            var skeletonRenderer = skeleton.GetComponent<SpriteRenderer>();
            if (skeletonRenderer != null)
            {
                // Renk karşılaştırmasını RGB ile yapıyoruz:
                Color playerColor = (currentColor == PlayerColor.Red) ? Color.red : Color.blue;
                if (Approximately(playerColor, skeletonRenderer.color))
                {
                    Debug.Log("Aynı renk, iskelet yok oldu!");
                    skeleton.Disappear();
                }
                else
                {
                    Debug.Log("Farklı renk: OYUN BİTTİ!");
                    GameManager.Instance.GameFinished();
                }
            }
        }
    }

    // Renkleri float hassasiyetine karşılaştırmak için yardımcı fonksiyon:
    private bool Approximately(Color a, Color b)
    {
        return Mathf.Approximately(a.r, b.r) &&
               Mathf.Approximately(a.g, b.g) &&
               Mathf.Approximately(a.b, b.b);
    }
}
