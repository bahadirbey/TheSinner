using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverRestart : MonoBehaviour
{
    public string bolumadi;
    public void restart()
    {
        SceneManager.LoadScene(bolumadi);
    }

    public void anamenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
