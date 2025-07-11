using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform upLimit;
    public Transform downLimit;

    private int direction = 1; // 1 = yukarı, -1 = aşağı
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue; // iskelet mavi
    }

    void Update()
    {
        transform.position += new Vector3(0, direction * moveSpeed * Time.deltaTime, 0);

        if (transform.position.y > upLimit.position.y)
            direction = -1;
        else if (transform.position.y < downLimit.position.y)
            direction = 1;
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}
