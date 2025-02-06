using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpTime = 0.35f; // Max time jump force is applied
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Start jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Continue jump while button is held
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Stop jump when button is released
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    // Visualize ground check in the editor
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}