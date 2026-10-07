using UnityEngine;

public class Diamond : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if ((collision.gameObject.layer == LayerMask.NameToLayer("Ground"))&& collision.GetContact(0).normal.y > 0.5f)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}