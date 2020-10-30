using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCreature : MonoBehaviour
{
    public int damage;
    public PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player.hittable)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
