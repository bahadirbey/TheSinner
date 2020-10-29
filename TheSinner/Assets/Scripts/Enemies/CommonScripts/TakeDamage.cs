using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public int health;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    } 

    public void GetDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("getHit");

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
