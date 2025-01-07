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
    public float dashDir {  get; private set; }


    [Header("Collision Parameters")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;



    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    
    
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
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }

    public PlayerPrimaryAttack primaryAttackState { get; private set; }

    #endregion

    private void Awake() {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        
        moveState = new PlayerMoveState(this, stateMachine, "Move");

        jumpState = new PlayerJumpState(this, stateMachine, "Jump");

        airState = new PlayerAirState(this, stateMachine, "Jump");

        dashState = new PlayerDashState(this, stateMachine, "Dash");

        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");

        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");

        primaryAttackState = new PlayerPrimaryAttack(this, stateMachine, "Attack");


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
        if (WallDetected()) {
            return;

        }

        dashCDTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCDTimer < 0) {
            dashCDTimer = dashCooldown;

            dashDir = Input.GetAxisRaw("Horizontal");

            stateMachine.ChangeState(dashState);

        }

    }

    public bool IsGrounded() {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    public bool WallDetected() {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity) {
        rigidbody2D.linearVelocity = new Vector2(_xVelocity, _yVelocity);

        FlipController(_xVelocity);

    }

    public void FlipSprite() {
        facingDir *= -1;

        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);

    }

    public void FlipController(float _x) {
        if (_x > 0 && !facingRight) {
            FlipSprite();

        } else if (_x < 0 && facingRight) {
            FlipSprite();

        }

    }

    public void AnimationTrigger() {
        stateMachine.currentState.AnimationFinishTrigger();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }


}
