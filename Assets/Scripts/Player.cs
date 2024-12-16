using UnityEngine;

public class Player : MonoBehaviour {

    #region Components
    public Animator animator {  get; private set; }

    #endregion


    #region States
    public PlayerStateMachine stateMachine {  get; private set; }


    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    #endregion

    private void Awake() {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        animator = GetComponentInChildren<Animator>();
        
        stateMachine.Initialize(idleState);        

    }

    // Update is called once per frame
    void Update() {
        stateMachine.currentState.Update();

        if (Input.GetKeyDown(KeyCode.N)) {
            if (stateMachine.currentState.ToString().Equals("PlayerIdleState")) {
                stateMachine.ChangeState(moveState);

            } else {
                stateMachine.ChangeState(idleState);
            }
            
        }

    }
}