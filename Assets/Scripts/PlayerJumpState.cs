using UnityEngine;

public class PlayerJumpState : PlayerState {
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();

        rigidbody2D.linearVelocityY = player.jumpForce;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        if (rigidbody2D.linearVelocityY < 0) {
            stateMachine.ChangeState(player.airState);
        }
    }
}
