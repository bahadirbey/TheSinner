using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAlpha : MonoBehaviour
{
    public Text gamename;
    public string newstring;
    public Color newcolor;

    public void Update()
    {
        gamename.text = newstring;
        newstring = "SINNER";
        gamename.color = newcolor;
        
        newcolor.a  += 0.002f;
    }
}
