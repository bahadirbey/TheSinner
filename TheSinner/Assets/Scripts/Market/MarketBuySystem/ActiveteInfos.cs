using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveteInfos : MonoBehaviour
{
    public GameObject infos;
    bool canclose;
    float timer = 0.2f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0f && canclose == false)
        {
            canclose = true;
            GetComponent<PickUpTest>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            infos.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            infos.gameObject.SetActive(false);
        }
    }
}
