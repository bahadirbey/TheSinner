using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransaction : MonoBehaviour
{
    public int gecilceksahne;
    private GameObject sceneTransactionPanel;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("SceneTransactionPanel");
    }

    public void TransactScene()
    {
        PlayerPrefs.SetInt("ActiveScene", gecilceksahne);
        SceneManager.LoadScene(gecilceksahne);
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sceneTransactionPanel.SetActive(true);
        }
    }
}
