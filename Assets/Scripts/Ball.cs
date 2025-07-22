using UnityEngine;

public class Ball : MonoBehaviour
{
    // Base speed of the ball. The final velocity depends on the difficulty
    // selected in GameManager.
    public float baseSpeed = 3f;
    private Rigidbody2D rb;

    private Vector2 storedVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetAndLaunch();
    }

    void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(-1f, 1f);
        Vector2 direction = new Vector2(x, y).normalized;
        float speed = GameManager.Instance != null
            ? GameManager.Instance.GetBallSpeed(baseSpeed)
            : baseSpeed;
        rb.velocity = direction * speed;
    }

    // Places the ball in the middle and launches it after 2 seconds.
    public void ResetAndLaunch()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
        Invoke(nameof(Launch), 2f);
    }

    // Stops the ball completely.
    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        CancelInvoke();
    }

    // Pauses or resumes movement preserving velocity.
    public void SetPaused(bool paused)
    {
        if (paused)
        {
            storedVelocity = rb.velocity;
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = storedVelocity;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            GameManager.Instance.Score(other.name);
            ResetAndLaunch();
        }
    }
}
