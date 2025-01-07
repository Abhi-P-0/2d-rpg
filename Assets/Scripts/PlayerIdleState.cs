using UnityEngine;

public class PlayerIdleState : PlayerGroundedState {

    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {

    }

    public override void Enter() {
        base.Enter();

        rigidbody2D.linearVelocity = Vector2.zero;

    }

    public override void Exit() {
        base.Exit();

    }

    public override void Update() {
        base.Update();

        if (xInput == player.facingDir && player.WallDetected()) {
            return;
        }

        if (xInput != 0) {
            stateMachine.ChangeState(player.moveState);
        }

    }
}
