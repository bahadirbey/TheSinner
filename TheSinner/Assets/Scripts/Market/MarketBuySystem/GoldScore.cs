using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldScore : MonoBehaviour
{
    public static int gold;
    public Text goldview;
    public Text notenoughmoney;

    private void Start()
    {
        gold = 0;
        notenoughmoney.gameObject.SetActive(false);
    }

    public void Update()
    {
        goldview.text = "Gold : " + gold;
    }
}
