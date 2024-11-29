using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;


    private Rigidbody2D rb;
    private Animator animator;

    private float xInput;
    private bool isMoving;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");

        rb.linearVelocityX = xInput * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) {
            
        }


        AnimatorController();
    }

    private void PlayerJump() {
        rb.linearVelocityY = jumpForce;

    }

    private void AnimatorController() {
        isMoving = xInput != 0f;

        animator.SetBool("IsMoving", isMoving);

    }
}
