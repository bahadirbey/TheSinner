using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //General elements Begin
    private Rigidbody2D rb;
    private Animator animator;
    //General elements End

    //Move Begin
    public float speed;
    private float horizontalMove;
    //Move End

    //Flip Begin
    bool facingRight;
    //Flip End

    //Jump Begin
    bool isGrounded;
    public Transform groundCheck;
    private float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    private bool extraJump;
    //Jump End
    void Start()
    {
        checkRadius = .1f;
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        horizontalMove = Input.GetAxis("Horizontal");
        Animate();
        Move();
        Flip();
        Jump();
    }

    void Animate()
    {
        animator.SetFloat("horizontalSpeed", Mathf.Abs(horizontalMove));
        animator.SetFloat("verticalSpeed", rb.velocity.y);
    }
    void Move()
    {
        rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb.velocity.y);
    }

    void Flip()
    {
        if (facingRight && horizontalMove < 0)
        {
            facingRight = false;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }else if (!facingRight && horizontalMove > 0)
        {
            facingRight = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            extraJump = true;
        }else if(Input.GetKeyDown(KeyCode.Space) && !isGrounded && extraJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            extraJump = false;
        }
    }
}
