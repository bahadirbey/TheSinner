using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //General elements Begin
    public static PlayerMovement instance;
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

    //Roll Begin
    private float rollingSpeed;
    public float slindingTime;
    //Roll End

    //Melee attack Begin
    public float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;

    public int damage;

    public int noOfClicks = 0;
    float lastClickedTime;
    public float maxComboDelay;

    bool attacking;
    float attackTime;
    public float startAttackTime;
    float realGravityScale;
    //Melee attack End

    private State state;
    enum State
    {
        Normal,
        DodgeRollSliding,
    }

    void Start()
    {
        state = State.Normal;
        checkRadius = .1f;
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        realGravityScale = rb.gravityScale;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        horizontalMove = Input.GetAxis("Horizontal");

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        switch (state)
        {
            case State.Normal:
                Animate();
                if (!attacking)
                {
                    Move();
                    Flip();
                    Jump();
                    Roll();
                }
                MeleeAttack();
                break;
            case State.DodgeRollSliding:
                RollSliding();
                break;
        }
    }
    void Animate()
    {
        animator.SetFloat("horizontalSpeed", Mathf.Abs(horizontalMove));
        animator.SetFloat("verticalSpeed", rb.velocity.y);
        animator.SetBool("attacking", attacking);
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
        }
        else if (!facingRight && horizontalMove > 0)
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
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && extraJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            extraJump = false;
        }
    }


    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            state = State.DodgeRollSliding;
            rollingSpeed = 1600f;
            animator.SetTrigger("roll");
        }
    }

    void RollSliding()
    {
        if (facingRight)
        {
            rb.velocity = new Vector2(rollingSpeed * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-rollingSpeed * Time.deltaTime, rb.velocity.y);
        }

        rollingSpeed -= rollingSpeed * slindingTime * Time.deltaTime;

        if (rollingSpeed < 200f)
        {
            state = State.Normal;
        }
    }

    void MeleeAttack()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                attacking = true;               
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                attackTime = startAttackTime;

                lastClickedTime = Time.time;
                noOfClicks++;
                if (noOfClicks == 1)
                {
                    animator.SetTrigger("attack");
                    animator.SetBool("attack1", true);
                }
                noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<TakeDamage>().GetDamage(damage);
                }
            }
            if (attackTime > 0)
            {
                attackTime -= Time.deltaTime;
            }
            else if(attacking)
            {
                timeBtwAttack = startTimeBtwAttack;
                attacking = false;
            }
        }
        else
        {
            rb.gravityScale = realGravityScale;
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void returnFirst()
    {
        if (noOfClicks >= 2)
        {
            animator.SetBool("attack2", true);
        }
        else
        {
            animator.SetBool("attack1", false);
            noOfClicks = 0;
        }
    }

    public void returnSecond()
    {
        if (noOfClicks >= 3)
        {
            animator.SetBool("attack3", true);
        }
        else
        {
            animator.SetBool("attack2", false);
            noOfClicks = 0;
        }
    }

    public void returnLast()
    {
        animator.SetBool("attack1", false);
        animator.SetBool("attack2", false);
        animator.SetBool("attack3", false);
        noOfClicks = 0;
        attackTime = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
