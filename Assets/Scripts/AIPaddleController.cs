using UnityEngine;

public class AIPaddleController : MonoBehaviour
{
    public Transform ball;
    public float baseSpeed = 5f;
    [Range(1,5)]
    public int difficulty = 3;

    private float precision = 1f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        precision = 0.6f + 0.1f * Mathf.Clamp(difficulty, 1, 5);
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null && (GameManager.Instance.IsPaused || GameManager.Instance.GameEnded))
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (ball == null) return;
        float targetY = ball.position.y + Random.Range(-1f, 1f) * (1f - precision) * 2f;
        float direction = Mathf.Clamp(targetY - transform.position.y, -1f, 1f);
        float speed = baseSpeed * difficulty;
        rb.velocity = new Vector2(0f, direction) * speed;
    }
}
