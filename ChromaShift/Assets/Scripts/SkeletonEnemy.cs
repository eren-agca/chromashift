using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform upLimit;   // Yukarıdaki sınır noktası (empty GameObject)
    public Transform downLimit; // Aşağıdaki sınır noktası (empty GameObject)

    private int direction = 1; // 1 = yukarı, -1 = aşağı
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue; // İskelet hep mavi
    }

    void Update()
    {
        // Yukarı veya aşağı hareket
        transform.position += new Vector3(0, direction * moveSpeed * Time.deltaTime, 0);

        // Sınırları kontrol et, yön değiştir
        if (transform.position.y > upLimit.position.y)
            direction = -1;
        else if (transform.position.y < downLimit.position.y)
            direction = 1;
    }
    public void Disappear()
    {
        // Burada efekt/animasyon oynatmak istersen ekleyebilirsin.
        Destroy(gameObject);
    }
}