using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public int health;
    internal int currentHealth;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    void Update()
    {

    } 

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("getHit");

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Destroyed()
    {
        Destroy(gameObject);
    }
}
