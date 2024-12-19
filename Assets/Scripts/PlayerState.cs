using UnityEngine;

public class PlayerState {
    
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rigidbody2D;

    protected float xInput;
    protected float stateTimer;

    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) {
        this.player = _player;
        this.stateMachine = _stateMachine;
        
        this.animBoolName = _animBoolName;

    }


    public virtual void Enter() {
        player.animator.SetBool(this.animBoolName, true);

        rigidbody2D = player.rigidbody2D;

    }


    public virtual void Update() {
        xInput = Input.GetAxisRaw("Horizontal");

        stateTimer -= Time.deltaTime;


        player.animator.SetFloat("yVelocity", rigidbody2D.linearVelocityY);

    }


    public virtual void Exit() {
        player.animator.SetBool(this.animBoolName, false);

    }

}
