using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellCompanionControl : MonoBehaviour
{
    public static int marketNum;
    public SaveSystem saveSystem;
    int randomNum;

    void Start()
    {
        marketNum = PlayerPrefs.GetInt("marketNum");

        if (PlayerPrefs.GetInt("NewInMarket" + marketNum) == 0)
        {
            RefreshCompanion();
            PlayerPrefs.SetInt("NewInMarket" + marketNum, 1);
        }
        else if (PlayerPrefs.GetInt("refreshMarket") == 1)
        {
            RefreshCompanion();           
        }
        else
        {
            Instantiate(saveSystem.marketCompanions[PlayerPrefs.GetInt("SellCompanion")], transform.position, Quaternion.identity, transform);
        }
    }

    public void RefreshCompanion()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        randomNum = Random.Range(0, 7);
        Instantiate(saveSystem.marketCompanions[randomNum], transform.position, Quaternion.identity, transform);
        PlayerPrefs.SetInt("SellCompanion", randomNum);
        PlayerPrefs.SetInt("refreshMarket", 0);
    }
}
