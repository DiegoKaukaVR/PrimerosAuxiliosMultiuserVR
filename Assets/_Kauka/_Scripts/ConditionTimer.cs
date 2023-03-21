using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionTimer : ConditionEvent
{
    public float timerAvaible = 0.5f;
    bool avaible = true;
   
    public override bool Condition(GameObject GO)
    {
        if (avaible)
        {
            avaible = false;
            StartCoroutine(ResetAvaible());
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator ResetAvaible()
    {
        avaible = false;
        yield return new WaitForSeconds(timerAvaible);
        avaible = true;
    }
}
