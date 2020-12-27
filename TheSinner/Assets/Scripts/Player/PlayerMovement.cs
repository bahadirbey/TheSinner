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
    bool canMove;
    //Move End

    //Flip Begin
    internal static bool facingRight;
    bool canFlip;
    //Flip End

    //Jump Begin
    bool canJump;
    bool isGrounded;
    public Transform groundCheck;
    private float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    private bool extraJump;

    public GameObject jumpEffect;
    //Jump End

    //Roll Begin
    bool canRoll;
    private float rollingSpeed;
    public float startRollingSpeed;
    public float endRollingSpeed;
    public float slindingTime;

    float rollingCoolDown;
    public float startRollingCoolDown;
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
    public float currentHealth;
    public float maxHealth;
    
    public bool hittable;
    public static bool blocking;

    bool daze;
    float dazedTime;
    public float startDazedTime;
    internal static bool dazeRight;

    public int def;
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

    //Kill Count Begin
    internal static int killCounter;
    internal static bool canRegenHealth;
    //Kill Count End

    //States Begin
    private State state;
    enum State
    {
        Normal,
        DodgeRollSliding,
        Death
    }
    //States End

    //STONES BEGIN-------------------

    //def stone
    public GameObject barrier;

    //star stone
    public GameObject star;

    //crit stone
    public static float critChance;

    //power stone
    public static float powerCd;
    public static float powerLastCd;
    Color color;

    //heal stone
    public static int stoneKillCounter;
    public GameObject healEffect;

    //STONES END---------------------
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
        maxHealth = 100;
        canMove = true;
        canJump = true;
        canRoll = true;
        canBlock = true;
        canAttack = true;
        powerLastCd = -1f;
        color = sprite.color;
    }

    void Update()
    {
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        horizontalMove = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            canAttack = true;
            if (!blocking)
            {
                canBlock = true;
            }           
            rb.gravityScale = realGravityScale;
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
                    Daze();
                }
                Roll();
                Block();
                CountKilling();
                MeleeAttack();
                CheckStar();
                ActivatePower();
                HealStoneCounter();
                Death();
                break;
            case State.DodgeRollSliding:
                RollSliding();
                break;
            case State.Death:
                Animate();
                Death();
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
        if (canMove)
        {
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        }
    }

    void CountKilling()
    {
        if (killCounter >= 10)
        {
            canRegenHealth = true;
            killCounter = 0;
        }
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
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                extraJump = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && extraJump)
            {
                Instantiate(jumpEffect, transform.position, Quaternion.identity);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                extraJump = false;
            }
        }     
    }

    void Roll()
    {
        if (rollingCoolDown <= 0 && canRoll)
        {
            if (Input.GetKeyDown(KeyCode.C) && isGrounded && !blocking)
            {
                state = State.DodgeRollSliding;
                rollingSpeed = startRollingSpeed;
                animator.SetTrigger("roll");
                rollingCoolDown = startRollingCoolDown;
            }
        }
        else
        {
            rollingCoolDown -= Time.deltaTime;
        }   
    }

    void RollSliding()
    {
            if (facingRight)
            {
                rb.velocity = new Vector2(rollingSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-rollingSpeed, rb.velocity.y);
            }

            hittable = false;
            rollingSpeed -= rollingSpeed * slindingTime;

            if (rollingSpeed < endRollingSpeed)
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
                #region CritStone
                critChance = Random.Range(0, 50);
                if (PlayerPrefs.GetInt("crit3") == 1)
                {
                    critChance += 30;
                }else if (PlayerPrefs.GetInt("crit2") == 1)
                {
                    critChance += 20;
                }else if (PlayerPrefs.GetInt("crit1") == 1)
                {
                    critChance += 10;
                }
                #endregion

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

    public void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (critChance > 50)
            {
                enemiesToDamage[i].GetComponent<TakeDamage>().GetDamage(damage*2);
            }
            else
            {
                enemiesToDamage[i].GetComponent<TakeDamage>().GetDamage(damage);
            }
            
        }

        if (facingRight)
        {
            rb.velocity = new Vector2(speed / 2, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed / 2, 0);
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

                #region Def Stone
                if (PlayerPrefs.GetInt("def3") == 1)
                {
                    hittable = false;
                    def = 4;
                    Instantiate(barrier, transform.position, Quaternion.identity);
                }
                else
                if (PlayerPrefs.GetInt("def2") == 1)
                {
                    hittable = false;
                    def = 2;
                    Instantiate(barrier, transform.position, Quaternion.identity);
                }
                else if (PlayerPrefs.GetInt("def1") == 1)
                {
                    hittable = false;
                    Instantiate(barrier, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
                }
                else
                {
                    hittable = true;
                }
                #endregion
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
        hittable = true;

        if (!isGrounded)
        {
            canAttack = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (hittable)
        {
            stoneKillCounter = 0;

            if (currentHealth == 1 || currentHealth - (damage - def) < 0)
            {
                currentHealth = 0;
            }
            else
            {
                if (damage - def <= 0)
                {
                    currentHealth -= 1;
                }
                else
                {
                    currentHealth -= (damage - def);
                }

                daze = true;
                hittable = false;
            }
        }       
    }

    void Daze()
    {
        if (daze)
        {
            if (dazedTime > 0)
            {
                if (!dazeRight)
                {
                    rb.velocity = new Vector2(-speed /2,0);
                }
                else
                {
                    rb.velocity = new Vector2(speed /2, 0);
                }
                sprite.color = new Color(.5f,.5f,.5f,1);
                dazedTime -= Time.deltaTime;
            }
            else
            {
                hittable = true;
                sprite.color = new Color(1, 1, 1, 1);
                daze = false;
                dazedTime = startDazedTime;
            }
        }
    }

    void Death()
    {
        if(currentHealth <= 0)
        {
            if (!dead)
            {
                animator.SetTrigger("death");
                animator.SetBool("dead", true);
            }

            state = State.Death;
            dead = true;
            deathPanel.SetActive(true);
            canFlip = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }

    public void GetHeal(int heal)
    {
        Instantiate(healEffect, transform.position, Quaternion.identity);
        if (currentHealth + heal > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += heal;
        }
    }

    public void CheckStar()
    {
        if (PlayerPrefs.GetInt("star3") == 1)
        {
            star.SetActive(true);
            star.GetComponent<Renderer>().material.color = new Color(0,0,0);
            StarDamage.damage = 10;
           
        }else if (PlayerPrefs.GetInt("star2") == 1)
        {
            star.SetActive(true);
            star.GetComponent<Renderer>().material.color = new Color(255,255,255);
            StarDamage.damage = 7;
        }
        else if (PlayerPrefs.GetInt("star1") == 1)
        {
            star.SetActive(true);
            star.GetComponent<Renderer>().material.color = new Color(255,255,0);
            StarDamage.damage = 4;
        }
        else
        {
            star.SetActive(false);
        }
    }

    public void ActivatePower()
    {
        if (powerLastCd >= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.yellow;
            damage = 20;

            if (def > 3)
            {
                def *= 2;
            }
            else
            {
                def = 5;
            }

            powerLastCd -= Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = color;
            powerCd -= Time.deltaTime;
        }
    }

    public void HealStoneCounter()
    {
        if (PlayerPrefs.GetInt("heal3") == 1)
        {
            if (stoneKillCounter >= 3)
            {
                GetHeal(stoneKillCounter);
                stoneKillCounter = 0;
            }
        }
        else if (PlayerPrefs.GetInt("heal2") == 1)
        {
            if (stoneKillCounter >= 6)
            {
                GetHeal(stoneKillCounter);
                stoneKillCounter = 0;
            }
        }
        else if (PlayerPrefs.GetInt("heal1") == 1)
        {
            if (stoneKillCounter >= 9)
            {
                GetHeal(stoneKillCounter);
                stoneKillCounter = 0;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
