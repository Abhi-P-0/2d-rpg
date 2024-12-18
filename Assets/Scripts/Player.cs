using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Movement Parameters")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;


    #region Components

    public Animator animator { get; private set; }
    public Rigidbody2D rigidbody2D { get; private set; }

    #endregion


    #region States
    public PlayerStateMachine stateMachine { get; private set; }


    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }

    #endregion

    private void Awake() {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        
        moveState = new PlayerMoveState(this, stateMachine, "Move");

        jumpState = new PlayerJumpState(this, stateMachine, "Jump");

        airState = new PlayerAirState(this, stateMachine, "Jump");

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        animator = GetComponentInChildren<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);

    }

    // Update is called once per frame
    void Update() {
        stateMachine.currentState.Update();

    }

    public void SetVelocity(float _xVelocity, float _yVelocity) {
        rigidbody2D.linearVelocity = new Vector2(_xVelocity, _yVelocity);

    }

}
