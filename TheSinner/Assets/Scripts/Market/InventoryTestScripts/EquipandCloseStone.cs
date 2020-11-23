using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipandCloseStone : MonoBehaviour
{
    private UsedItemsTest usedItems;
    public GameObject itemButton;
    public GameObject info1;
    public GameObject item;
    void Start()
    {
        usedItems = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
    }

    public void Equip()
    {
        Debug.Log("used");
        Close();

        for (int i = 0; i < usedItems.slots.Length; i++)
        {
            if (usedItems.isFull[i] == false)
            {
                Instantiate(itemButton, usedItems.slots[i].transform, false);
                Destroy(item);
                usedItems.isFull[i] = true;
                break;
                
            }
            
        }
       
        

    }

    public void Close()
    {
        info1.gameObject.SetActive(false);
    }
}
