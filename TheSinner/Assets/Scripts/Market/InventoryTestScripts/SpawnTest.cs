using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    
    public GameObject item;
    private UsedItemsTest useditemtest;
    private InventoryTest inventoryTest;

    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        useditemtest = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDroppedItem()
    {
        Debug.Log("dropped");
    }

    public void RemoveItem()
    {
        Debug.Log("removed");
        for (int i = 0; i<inventoryTest.slots.Length; i++)
        {
            if (inventoryTest.isFull[i] == false)
            {
                foreach (Transform child in transform)
                {
                    Instantiate(item, inventoryTest.slots[i].transform, false);
                    Destroy(useditemtest.sslots[i].transform.GetChild(0).gameObject);
                    
                }
               
                break;
            }
        }
        
        
       
    }
}
