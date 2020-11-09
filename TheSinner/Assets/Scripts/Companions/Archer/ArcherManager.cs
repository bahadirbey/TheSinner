﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherManager : MonoBehaviour
{
    public GameObject spawningEffect;
    public GameObject archer;
    public GameObject player;

    public float coolDown;
    float coolDownTimer;
    Vector2 whereToSpawn;

    Collider2D[] enemies;
    public float viewRadius;
    public LayerMask whatIsEnemies;
    Transform closestEnemy;

    internal static bool facingRight;
    bool playerLeft;

    private void Start()
    {
        coolDownTimer = coolDown;
    }

    void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }

        Spawn();
    }

    void Spawn()
    {
        if (coolDownTimer <= 0)
        {
            FindSpawningPlace();
            if (closestEnemy != null)
            {
                Vector2 whereToSpawnEffect = new Vector2(whereToSpawn.x - .4f, whereToSpawn.y + 1.4f);
                Instantiate(spawningEffect, whereToSpawnEffect, Quaternion.identity);
                Instantiate(archer, whereToSpawn, Quaternion.identity);
                coolDownTimer = coolDown;
                closestEnemy = null;
            }
        }
    }


    void FindSpawningPlace()
    {
        FindClosestEnemy();
        if (closestEnemy != null)
        {
            if (player.transform.position.x - closestEnemy.transform.position.x < 0)
            {
                playerLeft = true;
            }

            if (playerLeft)
            {
                whereToSpawn = new Vector2(closestEnemy.transform.position.x + 5f, closestEnemy.transform.position.y - .5f);
                facingRight = false;
            }
            else
            {
                whereToSpawn = new Vector2(closestEnemy.transform.position.x - 5f, closestEnemy.transform.position.y - .5f);
                facingRight = true;
            }
        }

    }

    void FindClosestEnemy()
    {
        enemies = Physics2D.OverlapCircleAll(transform.position, viewRadius, whatIsEnemies);
        closestEnemy = null;
        for (int i = 0; i < enemies.Length; i++)
        {

            if (closestEnemy == null)
            {
                closestEnemy = enemies[i].transform;
            }
            else if (Vector2.Distance(closestEnemy.transform.position, player.transform.position) > Vector2.Distance(player.transform.position, enemies[i].transform.position))
            {
                closestEnemy = enemies[i].transform;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.transform.position, viewRadius);
    }
}
