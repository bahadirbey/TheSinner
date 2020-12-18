using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadItemsToClick : MonoBehaviour
{
    public int i;
    public SaveSystem saveSystem;
    private UsedItemsTest usedItem;

    void Start()
    {
        usedItem = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
        CheckActiveItems();
    }

    public void CheckActiveItems()
    {
        Instantiate(saveSystem.sUsedSlotStones[PlayerPrefs.GetInt("slotUsedTestItem" + i)], usedItem.sslots[i].transform, false);
    }
}
