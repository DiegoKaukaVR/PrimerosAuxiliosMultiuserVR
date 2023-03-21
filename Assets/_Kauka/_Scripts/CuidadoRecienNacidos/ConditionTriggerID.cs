using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionTriggerID : ConditionEvent
{
    public TriggerID.IDTrigger targetTrigger;

    /// <summary>
    /// Comprueba si la id del objeto es la que nos interesa para activar el evento del trigger o no
    /// </summary>
    public override bool Condition(GameObject go)
    {
        if (go.GetComponent<TriggerID>())
        {
            if (go.GetComponent<TriggerID>().currentID == targetTrigger)
                return true;
            else
                return false;
        }

        return false;
    }

}
