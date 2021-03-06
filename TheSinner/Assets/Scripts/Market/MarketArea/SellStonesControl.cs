using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellStonesControl : MonoBehaviour
{
    public int i;
    public SaveSystem saveSystem;
    int randomNum;

    void Start()
    {
        SellCompanionControl.marketNum = PlayerPrefs.GetInt("marketNum");

        if (PlayerPrefs.GetInt("NewInMarket" + SellCompanionControl.marketNum) == 0)
        {
            RefreshStones();
        }
        else if(PlayerPrefs.GetInt("refreshMarket") == 1)
        {
            RefreshStones();
        }
        else
        {
            Instantiate(saveSystem.marketStones[PlayerPrefs.GetInt("SellStone" + i)], transform.position, Quaternion.identity, transform);  
        }
    }

    public void RefreshStones()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        randomNum = Random.Range(0, 7);
        Instantiate(saveSystem.marketStones[randomNum], transform.position, Quaternion.identity, transform);
        PlayerPrefs.SetInt("SellStone" + i, randomNum);
        PlayerPrefs.SetInt("refreshMarket", 0);
    }
}
