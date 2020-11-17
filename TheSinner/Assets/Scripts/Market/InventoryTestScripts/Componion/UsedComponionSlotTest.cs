﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedComponionSlotTest : MonoBehaviour
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
            usedItem.cisFull[i] = false;
        }
    }

    public void RemoveItem()
    {
        for (int i = 0; i < inventoryTest.componionslots.Length; i++)
        {
            if (inventoryTest.cisFull[i] == false)
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<SpawnTest>().RemoveItem();
                    Instantiate(child.gameObject, inventoryTest.componionslots[i].transform, false);
                    Destroy(child.gameObject);
                    inventoryTest.cisFull[i] = true;
                }
                break;
            }
        }
    }
}