﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponionSlotTest : MonoBehaviour
{
    private InventoryTest inventory;
    public int i;

    private InventoryTest inventoryTest;

    public SaveSystem saveSystem;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.cisFull[i] = false;
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            //PlayerPrefs.SetInt("inventoryTest" + i, 0);
            inventory.isFull[i] = false;
        }
    }

    public void CheckItem()
    {
        if (PlayerPrefs.GetInt("inventoryTest" + i) == 1)
        {
            inventory.isFull[i] = true;
            Instantiate(saveSystem.slotStones[PlayerPrefs.GetInt("slotTestItem" + i)], inventoryTest.slots[i].transform, false);
        }
    }
}
