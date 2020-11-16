using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponionPickUp : MonoBehaviour
{
    private InventoryTest inventoryTest;
    public GameObject itemButton;
    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PickUp()
    {
        for (int i = 0; i < inventoryTest.componionslots.Length; i++)
        {
            if (inventoryTest.cisFull[i] == false)
            {
                Destroy(gameObject);
                Instantiate(itemButton, inventoryTest.componionslots[i].transform, false);
                inventoryTest.cisFull[i] = true;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PickUp();
        }
    }
}