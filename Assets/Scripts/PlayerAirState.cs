using UnityEngine;

public class PlayerAirState : PlayerState {
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        if (player.WallDetected()) {
            stateMachine.ChangeState(player.wallSlideState);
        }

        if (player.IsGrounded()) {
            stateMachine.ChangeState(player.idleState);
        }

        if (xInput != 0) {
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rigidbody2D.linearVelocityY);
        }
    }
}
