using UnityEngine;

public class AIPaddleController : MonoBehaviour
{
    public Transform ball;
    public float baseSpeed = 5f;
    [Range(1,5)]
    public int difficulty = 3;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (ball == null) return;
        float direction = Mathf.Clamp(ball.position.y - transform.position.y, -1f, 1f);
        float speed = baseSpeed * difficulty;
        rb.velocity = new Vector2(0f, direction) * speed;
    }
}
