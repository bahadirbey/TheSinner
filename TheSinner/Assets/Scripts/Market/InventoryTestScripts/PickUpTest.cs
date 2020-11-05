using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTest : MonoBehaviour
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
        for (int i = 0; i < inventoryTest.slots.Length ;i++)
        {
            if (inventoryTest.isFull[i] == false)
            {
                Destroy(gameObject);
                Instantiate(itemButton, inventoryTest.slots[i].transform, false);
                inventoryTest.isFull[i] = true;
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
