using UnityEngine;

public class PlayerState {
    
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rigidbody2D;

    protected float xInput;
    protected float yInput;

    protected float stateTimer;

    protected bool triggerCalled;

    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) {
        this.player = _player;
        this.stateMachine = _stateMachine;
        
        this.animBoolName = _animBoolName;

    }


    public virtual void Enter() {
        player.animator.SetBool(animBoolName, true);

        rigidbody2D = player.rigidbody2D;

        triggerCalled = false;

    }


    public virtual void Update() {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        player.animator.SetFloat("yVelocity", rigidbody2D.linearVelocityY);

    }


    public virtual void Exit() {
        player.animator.SetBool(animBoolName, false);

    }

    public virtual void AnimationFinishTrigger() {
        triggerCalled = true;
    }

}
