using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MarketSahnesiYukleme : MonoBehaviour
{

    public string geldigimizsahne;

    // Start is called before the first frame update
    
    public void marketegecis()
    {
        SceneManager.LoadScene("Market");
    }
}
