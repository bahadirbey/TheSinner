using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritToClick : MonoBehaviour
{
    public int level;

    public void click()
    {
        PlayerMovement.canBeDamaged = false;
        PlayerMovement.canDestroySpirit = false;

        if (PlayerMovement.spiritCd <= 0)
        {
            if (level == 3)
            {
                PlayerMovement.spiritCd = 8f;
                PlayerMovement.spiritLastCd = 12f;
                PlayerMovement.spiritLife = 100;
            }
            else if (level == 2)
            {
                PlayerMovement.spiritCd = 11f;
                PlayerMovement.spiritLastCd = 9f;
                PlayerMovement.spiritLife = 70;
            }
            else if (level == 1)
            {
                PlayerMovement.spiritCd = 14f;
                PlayerMovement.spiritLastCd = 6f;
                PlayerMovement.spiritLife = 40;
            }
        }
    }
}
