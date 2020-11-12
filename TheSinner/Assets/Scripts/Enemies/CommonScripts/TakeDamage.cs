using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float health;
    internal float currentHealth;
    private Animator animator;

    public Transform bar;
    public Transform healthBar;
    float barHidingTime;
    float startBarHidingTime;

    internal bool dazed;
    internal float dazedTime;
    public float startDazedTime;
    internal bool dead;

    internal int hitCounter;
    internal bool hit;
    void Start()
    {
        startBarHidingTime = 2f;
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    void Update()
    {
        SetSize();
        ShowHealthBar();
        
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetBool("getHitBool", true);
        animator.SetTrigger("getHit");
        healthBar.gameObject.SetActive(true);
        barHidingTime = startBarHidingTime;
        dazed = true;
        dazedTime = startDazedTime;
        hit = true;
        if (hitCounter < 3)
        {
            hitCounter++;
        }

        if (currentHealth <= 0)
        {
            Destroyed();
        }
    }

    public void GetHitAnimation()
    {
        animator.SetBool("getHitBool", false);
        hit = false;
    }    

    public void Destroyed()
    {
        dead = true;
        PlayerMovement.killCounter += 1;
    }

    void ShowHealthBar()
    {
        if (barHidingTime > 0)
        {
            barHidingTime -= Time.deltaTime;
        }
        else
        {
            healthBar.gameObject.SetActive(false);
        }
    }

    void SetSize()
    {
        float size;
        size = currentHealth / health;
        bar.localScale = new Vector3(size, 1f);
    }
}
