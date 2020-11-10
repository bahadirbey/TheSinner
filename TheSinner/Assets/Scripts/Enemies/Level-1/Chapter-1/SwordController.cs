using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    Animator animator;
    private Patrol patrol;

    void Start()
    {
        animator = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
    }

    void Update()
    {
        Animate();
    }

    void Animate()
    {
        animator.SetBool("walking", patrol.patrolMovement);
    }
}
