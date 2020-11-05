using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorController : MonoBehaviour
{
    public GameObject destroyingEffect;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    void Update()
    {
        Facing();
    }

    public void Attack()
    {

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<TakeDamage>().GetDamage(damage);
        }
        Instantiate(destroyingEffect, new Vector2(transform.position.x, transform.position.y + .5f), Quaternion.identity);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void Facing()
    {
        if (ThorManager.facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
