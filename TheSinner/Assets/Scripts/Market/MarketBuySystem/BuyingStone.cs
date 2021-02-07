using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingStone : MonoBehaviour
{
    private GoldScore gold;
    public int cost;
    public GameObject notenoughmoney;
   
    // Start is called before the first frame update
    void Start()
    {
        gold = GameObject.FindGameObjectWithTag("Player").GetComponent<GoldScore>();
    }

    
    // Update is called once per frame
    public void Buy()
    {
        if(GoldScore.gold >= cost )
        {
            notenoughmoney.gameObject.SetActive(false);
            transform.parent.parent.parent.GetComponent<PickUpTest>().enabled = true;
            GoldScore.gold -= cost; 
        }

        else
        {
            notenoughmoney.gameObject.SetActive(true);
        }
    }
}
