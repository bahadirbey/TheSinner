using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGamePlayBtn : MonoBehaviour
{
    public GameObject options;

    public void play()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("ResetGame", 1);
        PlayerPrefs.SetInt("refreshMarket", 1);
        options.gameObject.SetActive(true);
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt(("ActiveScene"), 2);
        SceneManager.LoadScene(2);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target1")
        {
            Time.timeScale = 1f;
            PlayerPrefs.SetInt("ResetGame", 1);
            options.gameObject.SetActive(true);
        }
    }
}
