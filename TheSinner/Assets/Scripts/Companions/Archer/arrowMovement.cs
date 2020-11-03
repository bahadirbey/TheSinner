using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowMovement : MonoBehaviour
{
    public float speed;
    public GameObject destroyAnimation;
    public int damage;
    
    void Update()
    {
        Move();
        
    }

    void Move()
    {
        if (WarriorManager.facingRight)
        {
            transform.eulerAngles = new Vector3(0,0,-135);
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<TakeDamage>().GetDamage(damage);
            Instantiate(destroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
