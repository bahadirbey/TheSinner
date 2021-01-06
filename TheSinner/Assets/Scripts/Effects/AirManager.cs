using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirManager : MonoBehaviour
{
    private GameObject player;
    public float speed;
    float lifetime;

    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        lifetime = 8f;
    }

    
    void Update()
    {
        if (!player.GetComponent<PlayerMovement>().daze)
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                PlayerMovement.dazeRight = true;
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                PlayerMovement.dazeRight = false;
            }
        }

        if (lifetime <= 0)
        {
            animator.SetBool("end", true);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
    }
}
