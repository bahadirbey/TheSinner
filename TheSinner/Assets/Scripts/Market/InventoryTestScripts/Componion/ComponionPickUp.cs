using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponionPickUp : MonoBehaviour
{
    private InventoryTest inventoryTest;
    public GameObject itemButton;

    public int whichCompanion;
    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
    }

    void PickUp()
    {
        for (int i = 0; i < inventoryTest.companionslots.Length; i++)
        {
            if (inventoryTest.cisFull[i] == false)
            {
                Destroy(gameObject);
                Instantiate(itemButton, inventoryTest.companionslots[i].transform, false);
                inventoryTest.cisFull[i] = true;
                //PlayerPrefs.SetInt("inventoryTest" + i, 1);
                //PlayerPrefs.SetInt("slotTestItem" + i, whichStone);
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