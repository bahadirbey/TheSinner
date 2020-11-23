using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseBorn : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    public GameObject skeleton;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        BornController();
    }

    void BornController()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 2f)
        {
            animator.SetTrigger("born");
        }
    }

    public void Born()
    {
        Instantiate(skeleton, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
