using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //General elements Begin
    public static PlayerMovement instance;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject shield;
    SpriteRenderer sprite;
    //General elements End

    //Move Begin
    public float speed;
    private float horizontalMove;
    //Move End

    //Flip Begin
    bool facingRight;
    bool canFlip;
    //Flip End

    //Jump Begin
    bool isGrounded;
    public Transform groundCheck;
    private float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    private bool extraJump;

    public GameObject jumpEffect;
    //Jump End

    //Roll Begin
    private float rollingSpeed;
    public float slindingTime;
    //Roll End

    //Melee attack Begin
    float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;

    public int damage;

    int noOfClicks = 0;
    float lastClickedTime;
    public float maxComboDelay;

    bool attacking;
    float attackTime;
    public float startAttackTime;
    float realGravityScale;

    bool canAttack;
    //Melee attack End

    //Take Damage Begin
    public int health;
    public bool hittable;
    public bool blocking;

    bool daze;
    float dazedTime;
    public float startDazedTime;
    //Take Damage End

    //Block Begin
    public float startBlockTime;
    float blockTime;

    public float startTimeBtwBlocks;
    float timeBtwBlocks;

    bool canBlock;
    //Block End

    //CompanionCD Begin
    public GameObject companionCD;
    //CompannionCD End

    //Death Begin
    public bool dead;
    public GameObject deathPanel;
    //Death End

    //States Begin
    private State state;
    enum State
    {
        Normal,
        DodgeRollSliding,
        Death
    }
    //States End

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        dazedTime = startDazedTime;
        canFlip = true;
        hittable = true;
        state = State.Normal;
        checkRadius = .1f;
        facingRight = true;
        realGravityScale = rb.gravityScale;
        shield.SetActive(false);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        horizontalMove = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            canAttack = true;
            canBlock = true;
        }

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
                    Block();
                    Daze();
                }
                MeleeAttack();
                Death();
                break;
            case State.DodgeRollSliding:
                RollSliding();
                break;
            case State.Death:
                break;
        }
    }
    void Animate()
    {
        animator.SetFloat("horizontalSpeed", Mathf.Abs(horizontalMove));
        animator.SetFloat("verticalSpeed", rb.velocity.y);
        animator.SetBool("attacking", attacking);
        animator.SetBool("blocking", blocking);
    }
    void Move()
    {
        rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb.velocity.y);
    }

    void Flip()
    {
        if (canFlip)
        {
            if (facingRight && horizontalMove < 0)
            {
                facingRight = false;
                transform.eulerAngles = new Vector3(0, 180, 0);
                companionCD.transform.eulerAngles = new Vector3(0,0,0);
            }
            else if (!facingRight && horizontalMove > 0)
            {
                facingRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
                companionCD.transform.eulerAngles = new Vector3(0, 0, 0);
            }
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
            Instantiate(jumpEffect, transform.position, Quaternion.identity);
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

        hittable = false;
        rollingSpeed -= rollingSpeed * slindingTime * Time.deltaTime;

        if (rollingSpeed < 200f)
        {
            hittable = true;
            state = State.Normal;
        }
    }

    void MeleeAttack()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.X) && canAttack)
            {
                attacking = true;
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                attackTime = startAttackTime;
                canFlip = false;

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
            else if (attacking)
            {
                timeBtwAttack = startTimeBtwAttack;
                attacking = false;
                canFlip = true;
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

        if (!isGrounded)
        {
            canBlock = false;
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

        if (!isGrounded)
        {
            canBlock = false;
        }
    }

    public void returnLast()
    {
        animator.SetBool("attack1", false);
        animator.SetBool("attack2", false);
        animator.SetBool("attack3", false);
        noOfClicks = 0;
        attackTime = 0f;

        if (!isGrounded)
        {
            canBlock = false;
        }
    }

    public void Block()
    {
        if (timeBtwBlocks <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Z) && canBlock)
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                blockTime = startBlockTime;
                blocking = true;
                shield.SetActive(true);
                animator.SetTrigger("block");
                canFlip = false;
                canBlock = false;
            }
            if (blockTime > 0)
            {
                rb.velocity = Vector2.zero;
                blockTime -= Time.deltaTime;
            }
            else if (blocking)
            {
                timeBtwBlocks = startTimeBtwBlocks;
                blocking = false;
                canFlip = true;
            }
        }
        else
        {
            timeBtwBlocks -= Time.deltaTime;
        }

    }

    public void DisableBlock()
    {
        shield.SetActive(false);
        blocking = false;
        canFlip = true;
        rb.gravityScale = realGravityScale;

        if (!isGrounded)
        {
            canAttack = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        daze = true;
    }

    void Daze()
    {
        if (daze)
        {
            if (dazedTime > 0)
            {
                if (facingRight)
                {
                    rb.velocity = new Vector2(-speed * Time.deltaTime/2,0);
                }
                else
                {
                    rb.velocity = new Vector2(speed * Time.deltaTime/2, 0);
                }
                sprite.color = new Color(.5f,.5f,.5f,1);
                dazedTime -= Time.deltaTime;
            }
            else
            {
                sprite.color = new Color(1, 1, 1, 1);
                daze = false;
                dazedTime = startDazedTime;
            }
        }
    }

    void Death()
    {
        if(health <= 0)
        {
            if (!dead)
            {
                animator.SetTrigger("death");
            }
            dead = true;
            deathPanel.SetActive(true);
            rb.velocity = Vector2.zero;
            
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
