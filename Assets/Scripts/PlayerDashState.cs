using UnityEngine;

public class PlayerDashState : PlayerGroundedState {

    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {

    }

    public override void Enter() {
        base.Enter();

        stateTimer = player.dashDuration;
    }

    public override void Exit() {
        player.SetVelocity(0f, rigidbody2D.linearVelocityY);

        base.Exit();
    }

    public override void Update() {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.transform.localScale.x, 0);

        if (stateTimer < 0) stateMachine.ChangeState(player.idleState);

    }
}
