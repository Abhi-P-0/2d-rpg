using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;

    [Header("Ground collosion")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

    
    private Rigidbody2D rb;
    private CapsuleCollider2D collider2D;
    private Animator animator;

    private float xInput;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        PlayerMovement();

        dashTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            dashTime = dashDuration;
        }

        if (dashTime > 0) {
            Debug.Log("Dashing");
        }

        AnimatorController();
    }

    private void PlayerInput() {
        xInput = Input.GetAxis("Horizontal");

        if (xInput > 0.01f) {
            transform.localScale = Vector3.one;

        } else if (xInput < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);

        }

    }

    private void PlayerMovement() {
        if (dashTime > 0) {
            //rb.linearVelocityX = xInput * dashSpeed;
            rb.linearVelocity = new Vector2(xInput * dashSpeed, 0);

        } else {
            rb.linearVelocityX = xInput * moveSpeed;

        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            //isGrounded = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

            PlayerJump();

        }

    }

    private void PlayerJump() {
        rb.linearVelocityY = jumpForce;

    }

    private bool IsGrounded() {
        return Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0, Vector2.down, groundCheckDistance, groundLayer);

    }

    private void FlipPlayer() {
        transform.Rotate(0, 180, 0);
    }

    private void AnimatorController() {
        bool isMoving = xInput != 0f;

        animator.SetBool("IsMoving", isMoving);

        animator.SetBool("IsGrounded", IsGrounded());

        animator.SetBool("IsDashing", dashTime > 0);

        animator.SetFloat("yVelocity", rb.linearVelocityY);

    }

    private void OnDrawGizmos() {
        Gizmos.DrawCube(new Vector3(collider2D.bounds.center.x, collider2D.bounds.center.y - collider2D.bounds.size.y / 2), new Vector3(collider2D.bounds.size.x, groundCheckDistance));

    }
}
