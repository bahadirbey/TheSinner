using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStoneToClick : MonoBehaviour
{
    public int level;

    public void click()
    {
        if (PlayerMovement.powerCd <= 0)
        {
            if (level == 3)
            {
                PlayerMovement.powerCd = 8f;
                PlayerMovement.powerLastCd = 12f;
            }
            else if (level == 2)
            {
                PlayerMovement.powerCd = 11f;
                PlayerMovement.powerLastCd = 9f;
            }
            else
            {
                PlayerMovement.powerCd = 14f;
                PlayerMovement.powerLastCd = 6f;
            }
        }
    }
}
