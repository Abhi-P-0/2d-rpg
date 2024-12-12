using UnityEngine;

public class EnemySkeleton : Entity {

    [Header("Movement Info")]
    [SerializeField] private float moveSpeed;

    protected override void Start() {
        base.Start();

    }

    protected override void Update() {
        base.Update();

        rigidbody2D.linearVelocityX = moveSpeed * facingDirection;

        if (!IsGrounded()) {
            FlipSprite();


        }

    }
}
