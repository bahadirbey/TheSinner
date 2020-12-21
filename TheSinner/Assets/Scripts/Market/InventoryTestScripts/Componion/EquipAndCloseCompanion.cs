using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAndCloseCompanion : MonoBehaviour
{
    private UsedItemsTest usedItems;
    public GameObject companiontousedBtn;
    public GameObject companionButton;
    public GameObject info1;
    public GameObject companion;

    public string stoneName;
    public int level;

    public int whichStone;
    void Start()
    {
        usedItems = GameObject.FindGameObjectWithTag("Player").GetComponent<UsedItemsTest>();
    }

    public void Equip()
    {
        Debug.Log("used");
        Close();

        for (int i = 0; i < usedItems.cslots.Length; i++)
        {
            if (usedItems.cisFull[i] == false)
            {
                //PlayerPrefs.SetInt(stoneName + level, 1);
                Instantiate(companionButton, usedItems.cslots[i].transform, false);
                /**/
                Instantiate(companiontousedBtn, usedItems.csslots[i].transform, false);

                //PlayerPrefs.SetInt("inventoryUsedTest" + i, 1);
                //PlayerPrefs.SetInt("slotUsedTestItem" + i, whichStone);
                //PlayerPrefs.SetInt("inventoryTest" + transform.parent.parent.parent.GetComponent<SlotTest>().i, 0);

                Destroy(companion);
                usedItems.cisFull[i] = true;
                /**/
                usedItems.csisFull[i] = true;
                break;
            }
        }
    }

    public void Close()
    {
        info1.gameObject.SetActive(false);
    }
}
