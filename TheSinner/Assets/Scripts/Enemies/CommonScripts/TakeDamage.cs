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
        animator.SetTrigger("getHit");
        healthBar.gameObject.SetActive(true);
        barHidingTime = startBarHidingTime;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Destroyed()
    {
        Destroy(gameObject);
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
