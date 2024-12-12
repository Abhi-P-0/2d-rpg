using UnityEngine;
using UnityEngine.Windows;

public class Entity : MonoBehaviour {

    [Header("Ground collosion")]
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected float groundCheckDistance;


    protected Rigidbody2D rigidbody2D;
    protected Collider2D collider2D;
    protected Animator animator;

    protected int facingDirection = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    protected virtual void Update() {

    }

    protected virtual bool IsGrounded() {
        return Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0, Vector2.down, groundCheckDistance, groundLayer);

    }

    protected virtual void FlipSpriteMoveFaceDirection() {
        //if (xInput > 0.01f) {
        if (rigidbody2D.linearVelocityX > 0.01f) {
            transform.localScale = Vector3.one;


            //} else if (xInput < -0.01f) {
        } else if (rigidbody2D.linearVelocityX < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);

        }
        facingDirection *= -1;
    }

    protected virtual void FlipSprite() {
        facingDirection *= -1;

        transform.localScale = new Vector3(facingDirection, 1, 1);
    }

    protected virtual void OnDrawGizmos() {
        Gizmos.DrawCube(new Vector3(collider2D.bounds.center.x, collider2D.bounds.center.y - collider2D.bounds.size.y / 2), new Vector3(collider2D.bounds.size.x, groundCheckDistance));

    }
}
