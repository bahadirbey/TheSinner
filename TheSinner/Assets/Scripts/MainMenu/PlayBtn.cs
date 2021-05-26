using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayBtn : MonoBehaviour
{
    public GameObject options;
  
    public void play()
    { 
        Time.timeScale = 1f;
        options.gameObject.SetActive(true);
        SceneManager.LoadScene(PlayerPrefs.GetInt("ActiveScene"));
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target1" )
        {
            Time.timeScale = 1f;
            PlayerPrefs.SetInt("ResetGame", 1);
            options.gameObject.SetActive(true);
        }
    }
}
