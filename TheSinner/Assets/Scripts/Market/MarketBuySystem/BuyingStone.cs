﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingStone : MonoBehaviour
{
    public int cost;
    public GameObject notenoughmoney;
    public PickUpTest pickUpTest;

    public void Buy()
    {
        if(GoldScore.gold >= cost )
        {
            notenoughmoney.gameObject.SetActive(false);
            transform.parent.parent.parent.GetComponent<PickUpTest>().enabled = true;
            pickUpTest.canPickUp = true;
            GoldScore.gold -= cost; 
        }

        else
        {
            notenoughmoney.gameObject.SetActive(true);
        }
    }
}
