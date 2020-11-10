using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private Animator animator;
    private Patrol patrol;

    float timeBtwAttack;
    public float startTimeBtwAttack;
    float readyAttackTime;
    public float startReadyAttackTime;

    Collider2D playerToDamage;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;

    public int damage;

    void Start()
    {
        animator = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
        readyAttackTime = startReadyAttackTime;
    }


    void Update()
    {
        playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemies);
        MeleeAttackPrep();
    }

    void MeleeAttackPrep()
    {
        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            if (playerToDamage != null)
            {
                patrol.canPatrol = false;
                patrol.patrolMovement = false;

                if (readyAttackTime <= 0)
                {
                    readyAttackTime = startReadyAttackTime;
                    animator.SetBool("attacking", true);
                }
                else
                {
                    readyAttackTime -= Time.deltaTime;
                }

            }
            else
            {
                patrol.canPatrol = true;
                patrol.patrolMovement = true;
            }

            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (playerToDamage != null)
        {
            playerToDamage.GetComponent<PlayerMovement>().TakeDamage(damage);
        }
    }

    public void AttackEnd()
    {
        animator.SetBool("attacking", false);
        patrol.canPatrol = true;
        patrol.patrolMovement = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
