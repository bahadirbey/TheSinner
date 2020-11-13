using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowManager : MonoBehaviour
{
    public float speed;
    public GameObject destroyAnimation;
    public int damage;
    internal Vector2 target;
    private GameObject player;
    Vector3 direction;
    bool reachedToTarget;
    Vector3 directionToMove;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = new Vector2(player.transform.position.x, player.transform.position.y + .5f);
        direction = transform.position - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, Time.deltaTime * 50);
        directionToMove = transform.position - player.transform.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        direction.Normalize();
        if (!reachedToTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), target) < 0.2f)
            {
                Destroy(gameObject);
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage);

            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                PlayerMovement.dazeRight = true;
            }
            else if (collision.gameObject.transform.position.x < transform.position.x)
            {
                PlayerMovement.dazeRight = false;
            }
            
            Instantiate(destroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
