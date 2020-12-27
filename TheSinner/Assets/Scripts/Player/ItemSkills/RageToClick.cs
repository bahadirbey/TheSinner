using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageToClick : MonoBehaviour
{
    public int level;

    public void click()
    {
        if (PlayerMovement.rageCd <= 0)
        {
            PlayerMovement.canBeDamaged = false;

            if (level == 3)
            {
                PlayerMovement.rageCd = 8f;
                PlayerMovement.rageLastCd = 12f;
            }else if(level == 2)
            {
                PlayerMovement.rageCd = 11f;
                PlayerMovement.rageLastCd = 9f;
            }
            else if(level == 1)
            {
                PlayerMovement.rageCd = 14f;
                PlayerMovement.rageLastCd = 6f;
            }
        }
    }
}
