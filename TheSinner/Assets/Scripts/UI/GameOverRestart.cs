using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverRestart : MonoBehaviour
{
    
    public void restart()
    {
        SceneManager.LoadScene("Mert'sTestScene");
    }

}
