using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCraftStone : MonoBehaviour
{
    private CraftingSlots craftItems;
    public GameObject itemButton;
    public GameObject item;

    public string stoneName;
    public int level;

    void Start()
    {
        craftItems = GameObject.FindGameObjectWithTag("Player").GetComponent<CraftingSlots>();
    }

    public void Craft()
    {
        for (int i = 0; i < craftItems.slots.Length; i++)
        {
            if (craftItems.isFull[i] == false)
            {
                Instantiate(itemButton, craftItems.slots[i].transform, false);
                craftItems.isFull[i] = true;
                Destroy(item);
                break;
            }
        }
    }
}
