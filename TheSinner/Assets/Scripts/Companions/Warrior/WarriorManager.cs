using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorManager : MonoBehaviour
{
    public GameObject warrior;
    public GameObject player;

    public float coolDown;
    float coolDownTimer;
    Vector2 whereToSpawn;

    GameObject[] enemies;
    GameObject closestEnemy;
    bool spawnRight;
    bool spawnLeft;
    public float checkRadius;
    public LayerMask whatIsGround;

    void Start()
    {

    }

    void Update()
    {
        if(coolDownTimer > 0)
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
                Instantiate(warrior, whereToSpawn, Quaternion.identity);
                coolDownTimer = coolDown;
                spawnRight = false;
                spawnLeft = false;
                closestEnemy = null;
            }       
        }
    }

    void FindSpawningPlace()
    {
        FindClosestEnemy();
        if (closestEnemy != null) {
            spawnRight = Physics2D.OverlapCircle(new Vector2(closestEnemy.transform.position.x + .5f, closestEnemy.transform.position.y), checkRadius, whatIsGround);
            spawnLeft = Physics2D.OverlapCircle(new Vector2(closestEnemy.transform.position.x - .5f, closestEnemy.transform.position.y), checkRadius, whatIsGround);
        }
        if (spawnRight)
        {
            whereToSpawn = new Vector2(closestEnemy.transform.position.x + .5f, closestEnemy.transform.position.y);
            warrior.transform.eulerAngles = new Vector3(0,180,0);
        }else if (spawnLeft)
        {
            whereToSpawn = new Vector2(closestEnemy.transform.position.x - .5f, closestEnemy.transform.position.y);
            warrior.transform.eulerAngles = new Vector3(0, 0 , 0);
        }
    }

    void FindClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        closestEnemy = null;
        for (int i = 0; i < enemies.Length; i++)
        {
            
            if (closestEnemy == null)
            {
                closestEnemy = enemies[i];
            }
            else if(Vector2.Distance(closestEnemy.transform.position, player.transform.position) < Vector2.Distance(player.transform.position, enemies[i].transform.position))
            {
                closestEnemy = enemies[i];
            }
        }
    }
}
