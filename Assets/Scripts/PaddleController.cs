using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public string inputAxis = "Vertical";
    public bool isLeftPlayer = true;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = isLeftPlayer ? Color.blue : Color.red;
        }
    }

    void FixedUpdate()
    {
        float move = Input.GetAxisRaw(inputAxis);
        rb.velocity = new Vector2(0f, move) * speed;
    }
}
