using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponionSlotTest : MonoBehaviour
{
    private InventoryTest inventory;
    public int i;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
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
            child.GetComponent<SpawnTest>().SpawnDroppedItem();
            Destroy(child.gameObject);
        }
    }
}
