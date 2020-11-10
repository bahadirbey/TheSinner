using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private Patrol patrol;

    public float health;
    internal float currentHealth;
    private Animator animator;

    public Transform bar;
    public Transform healthBar;
    float barHidingTime;
    float startBarHidingTime;

    bool dazed;
    float dazedTime;
    bool dazeTimeEqual;
    public float startDazedTime;
    private SpriteRenderer sprite;

    void Start()
    {
        patrol = GetComponent<Patrol>();
        startBarHidingTime = 2f;
        animator = GetComponent<Animator>();
        currentHealth = health;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        SetSize();
        ShowHealthBar();
        Daze();
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetBool("getHitBool", true);
        animator.SetTrigger("getHit");
        healthBar.gameObject.SetActive(true);
        barHidingTime = startBarHidingTime;
        dazed = true;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetHitAnimation()
    {
        animator.SetBool("getHitBool", false);
    }

    void Daze()
    {
        if (dazed)
        {
            patrol.canPatrol = false;
            sprite.color = new Color(.5f, .5f, .5f, 1);
        }
    }

    public void EndDaze()
    {
        patrol.canPatrol = true;
        sprite.color = new Color(1, 1, 1, 1);
        dazed = false;
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
