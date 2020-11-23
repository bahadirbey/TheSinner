using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlashRight : MonoBehaviour
{
    float timer = 2f;
    public float speed;
    public GameObject explosionEffect;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }else
        {
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsEnemy);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<PlayerMovement>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
