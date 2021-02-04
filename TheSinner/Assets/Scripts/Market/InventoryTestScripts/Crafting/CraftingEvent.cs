using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingEvent : MonoBehaviour
{
    public GameObject[] slots;
    private InventoryTest inventoryTest;
    int slot;
    void Start()
    {
        inventoryTest = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryTest>();
    }

    public void Craft()
    {
        if (slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone ==
        slots[1].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone
        && slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone ==
        slots[2].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().whichStone)
        {
            for (int i = 0; i < inventoryTest.slots.Length; i++)
            {
                if (inventoryTest.isFull[i] == false)
                {
                    Instantiate(slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().nextStone,
                        inventoryTest.slots[i].transform, false);
                    slot = i;
                    inventoryTest.isFull[i] = true;
                    CheckStones();
                    Destroy(slots[0].gameObject.transform.GetChild(0).gameObject);
                    Destroy(slots[1].gameObject.transform.GetChild(0).gameObject);
                    Destroy(slots[2].gameObject.transform.GetChild(0).gameObject);
                    break;
                }
            }
        }
    }

    public void CheckStones()
    {
        PlayerPrefs.SetInt("inventoryTest" + slot, 1);
        PlayerPrefs.SetInt("slotTestItem" + slot, slots[0].gameObject.transform.GetChild(0).GetComponent<SpawnCrafting>().nextStoneInt);

        for (int i = 0; i < inventoryTest.slots.Length; i++)
        {
            if (inventoryTest.isFull[i] == false)
            {
                PlayerPrefs.SetInt("inventoryTest" + i, 0);
            }    
        }
    }
}

