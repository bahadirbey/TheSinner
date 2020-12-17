using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    public GameObject item;
    private UsedItemsTest useditemtest;
    private InventoryTest inventoryTest;

    public string name;
    public int level;
    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        useditemtest = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
    }

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
                    PlayerPrefs.SetInt(name + level, 0);
                    Instantiate(item, inventoryTest.slots[i].transform, false);
                    Destroy(useditemtest.sslots[i].transform.GetChild(0).gameObject);
                }
               
                break;
            }
        }  
    }
}
