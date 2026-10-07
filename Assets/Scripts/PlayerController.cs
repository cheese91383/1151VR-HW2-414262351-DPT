using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite walkSprite;
    SpriteRenderer sr;
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;
    bool isGrounded;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2( moveSpeed*x , rb.linearVelocity.y);
        if (x != 0)
        {
            sr.sprite = walkSprite;
        }
        else
        {
            sr.sprite = idleSprite;
        }

        if (x > 0) sr.flipX=true;
        if (x < 0) sr.flipX = false;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer); 
        if ((Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
        }
    }
}