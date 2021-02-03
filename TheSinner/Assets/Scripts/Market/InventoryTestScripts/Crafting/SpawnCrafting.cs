using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrafting : MonoBehaviour
{
    public GameObject item;
    private InventoryTest inventoryTest;

    public int whichStone;

    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
    }

    public void RemoveItem()
    {
        
        for (int i = 0; i < inventoryTest.slots.Length; i++)
        {
           
            if (inventoryTest.isFull[i] == false)
            {
              

              
                    
                    Instantiate(item, inventoryTest.slots[i].transform, false);
                

                break;
            }
        }
    }
}
