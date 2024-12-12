using UnityEngine;

public class PlayerScript : Entity {

    [Header("Player Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;


    [Header("Dash Info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;


    [Header("Basic Attack Info")]
    private bool isAttacking;
    private int comboCounter;



    private float xInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start() {
        base.Start();

    }


    // Update is called once per frame
    protected override void Update() {
        base.Update();

        PlayerInput();

        PlayerMovement();

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;

        AnimatorController();
    }


    private void PlayerInput() {
        xInput = Input.GetAxis("Horizontal");

        FlipSpriteFacingDirection();

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            isAttacking = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer < 0) {
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

    }




    private void PlayerMovement() {
        if (dashTime > 0) {
            //rb.linearVelocityX = xInput * dashSpeed;
            rigidbody2D.linearVelocity = new Vector2(transform.localScale.x * dashSpeed, 0);

        } else {
            rigidbody2D.linearVelocityX = xInput * moveSpeed;

        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            //isGrounded = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

            PlayerJump();

        }

    }


    private void PlayerJump() {
        rigidbody2D.linearVelocityY = jumpForce;

    }


    public void AttackOver() {
        isAttacking = false;

        comboCounter++;

        if (comboCounter > 2) {
            comboCounter = 0;
        }
    }


    private void AnimatorController() {
        bool isMoving = xInput != 0f;

        animator.SetBool("IsMoving", isMoving);

        animator.SetBool("IsGrounded", IsGrounded());

        animator.SetBool("IsDashing", dashTime > 0);

        animator.SetFloat("yVelocity", rigidbody2D.linearVelocityY);

        animator.SetBool("IsAttacking", isAttacking);

        animator.SetInteger("ComboCounter", comboCounter);

    }

}
