using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerManager : MonoBehaviour
{
    public GameObject healerPet;
    public GameObject player;

    public float coolDown;
    float coolDownTimer;
    Vector2 whereToSpawn;

    //internal static bool facingRight;

    void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }

        if (player.GetComponent<PlayerMovement>().health < player.GetComponent<PlayerMovement>().maxHealth)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        if (coolDownTimer <= 0)
        {
            FindSpawningPlace();
            Instantiate(healerPet, whereToSpawn, Quaternion.identity);
            coolDownTimer = coolDown;
        }
    }

    void FindSpawningPlace()
    {
        whereToSpawn = new Vector2(player.transform.position.x - .5f, player.transform.position.y + 1f);
    }
}
