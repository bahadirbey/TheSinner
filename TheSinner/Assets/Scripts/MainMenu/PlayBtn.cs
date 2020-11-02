using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayBtn : MonoBehaviour
{
    public GameObject options;
  
    public void play()
    {
        SceneManager.LoadScene("Mert'sTestScene");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target1" )
        {
            options.gameObject.SetActive(true);
        }
    }

}
