using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Movement Parameters")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Dash Parameters")]
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    private float dashCDTimer;


    [Header("Collision Parameters")]
    public LayerMask groundLayer;
    public float groundCheckDistance;


    public int facingDirection = 1;


    #region Components

    public Animator animator { get; private set; }
    public Rigidbody2D rigidbody2D { get; private set; }
    public Collider2D collider2D { get; private set; }

    #endregion


    #region States
    public PlayerStateMachine stateMachine { get; private set; }


    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerDashState dashState { get; private set; }

    #endregion

    private void Awake() {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        
        moveState = new PlayerMoveState(this, stateMachine, "Move");

        jumpState = new PlayerJumpState(this, stateMachine, "Jump");

        airState = new PlayerAirState(this, stateMachine, "Jump");

        dashState = new PlayerDashState(this, stateMachine, "Dash");

        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();

        animator = GetComponentInChildren<Animator>();

        stateMachine.Initialize(idleState);

    }

    // Update is called once per frame
    void Update() {
        DashManager();

        stateMachine.currentState.Update();

        
    }

    private void DashManager() {
        dashCDTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCDTimer < 0) {
            dashCDTimer = dashCooldown;

            stateMachine.ChangeState(dashState);

        }

    }

    public bool IsGrounded() {
        return Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0, Vector2.down, groundCheckDistance, groundLayer);
    }

    public bool IsWallDetected() {
        return Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 0.5f, groundLayer);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity) {
        if (_xVelocity > 0.01f) {
            transform.localScale = Vector3.one;

        } else if (_xVelocity < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);

        }

        rigidbody2D.linearVelocity = new Vector2(_xVelocity, _yVelocity);

    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + 1f * transform.localScale.x, transform.position.y));
    }

    public void FlipSprite() {

    }

}
