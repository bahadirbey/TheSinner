using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SahneGecis : MonoBehaviour
{
    
    public int gecilceksahne;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt("ActiveScene", gecilceksahne);
            SceneManager.LoadScene(gecilceksahne);
        }
        
    }
    
       
    
}
