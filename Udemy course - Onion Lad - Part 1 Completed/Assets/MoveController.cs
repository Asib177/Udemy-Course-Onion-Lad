using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;



    [Header("Collision check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;


    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimationControllers();
        CollisionChecks();
        FlipController();

        xInput = Input.GetAxisRaw("Horizontal");


        Movement();



        if (Input.GetKeyDown(KeyCode.Space))
            Jump();


    }

    private void AnimationControllers()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }


    private void Jump()
    {
        if (isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void Movement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void FlipController()
    {
        if (rb.linearVelocity.x < 0 && facingRight)
            Flip();
        else if (rb.linearVelocity.x > 0 && !facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight; // works as a switcher
        transform.Rotate(0, 180, 0);
    }


    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
