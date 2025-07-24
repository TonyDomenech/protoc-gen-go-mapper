using UnityEngine;

public class Ball : MonoBehaviour
{
    // Base speed of the ball. The final velocity depends on the difficulty
    // selected in GameManager.
    public float baseSpeed = 3f;

    // Optional sounds for collisions and scoring.
    public AudioSource bounceSound;
    public AudioSource scoreSound;

    private Rigidbody2D rb;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            if (scoreSound != null) scoreSound.Play();
            GameManager.Instance.Score(other.name);
            ResetAndLaunch();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounceSound != null) bounceSound.Play();
    }
}
