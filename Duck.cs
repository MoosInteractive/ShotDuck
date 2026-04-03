using UnityEngine;

public enum DuckType
{
    Walker,
    Jumper,
    Charger
}

[RequireComponent(typeof(Rigidbody2D))]
public class Duck : MonoBehaviour
{
    [Header("Duck Settings")]
    public DuckType duckType = DuckType.Walker;
    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    public float detectionRange = 5f;

    private Rigidbody2D rb;
    private Transform player;
    private bool movingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        switch (duckType)
        {
            case DuckType.Walker:
                Walk();
                break;
            case DuckType.Jumper:
                JumpBehavior();
                break;
            case DuckType.Charger:
                Charge();
                break;
        }
    }

    private void Walk()
    {
        rb.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.velocity.y);

        // Flip sprite
        transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1);
    }

    private void JumpBehavior()
    {
        Walk();

        if (Random.Range(0f, 1f) < 0.01f && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Charge()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= detectionRange)
            {
                movingRight = player.position.x > transform.position.x;
                rb.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed * 2, rb.velocity.y);
            }
            else
            {
                Walk();
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            movingRight = !movingRight;
        }
    }
}
