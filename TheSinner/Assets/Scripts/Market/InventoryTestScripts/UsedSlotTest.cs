﻿using UnityEngine;

public class UsedSlotTest : MonoBehaviour
{
    private InventoryTest inventoryTest;
    private UsedItemsTest usedItem;
    public int i;
    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        usedItem = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= 0)
        {
            usedItem.isFull[i] = false;
        }
    }

    public void RemoveItem()
    {
        for (int i = 0; i < inventoryTest.slots.Length; i++)
        {
            if (inventoryTest.isFull[i] == false)
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<SpawnTest>().RemoveItem();
                    Instantiate(child.gameObject, inventoryTest.slots[i].transform, false);
                    Destroy(child.gameObject);
                    inventoryTest.isFull[i] = true;
                }
                break;
            }
        }       
    }
}
