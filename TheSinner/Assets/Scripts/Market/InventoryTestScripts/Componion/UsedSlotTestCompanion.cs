using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedSlotTestCompanion : MonoBehaviour
{
    private InventoryTest inventoryTest;
    private UsedItemsTest usedItem;
    public int i;

    public SaveSystem saveSystem;
    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
        usedItem = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
        //CheckItem();
    }

    void Update()
    {
        if (transform.childCount <= 0)
        {
            usedItem.cisFull[i] = false;
        }
    }

    public void RemoveItem()
    {
        for (int i = 0; i < inventoryTest.companionslots.Length; i++)
        {
            if (inventoryTest.cisFull[i] == false)
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<SpawnTest>().RemoveItem();

                    //PlayerPrefs.SetInt("inventoryUsedTest" + this.i, 0);
                    //
                    //PlayerPrefs.SetInt("inventoryTest" + i, 1);
                    //PlayerPrefs.SetInt("slotTestItem" + i, child.GetComponent<SpawnTest>().whichStone);

                    Destroy(child.gameObject);
                    inventoryTest.cisFull[i] = true;
                }
                break;
            }
        }
    }

    public void CheckItem()
    {
        if (PlayerPrefs.GetInt("inventoryUsedTest" + i) == 1)
        {
            usedItem.isFull[i] = true;
            Instantiate(saveSystem.usedSlotStones[PlayerPrefs.GetInt("slotUsedTestItem" + i)], usedItem.slots[i].transform, false);
        }
    }
}
