using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionGrabID : ConditionEvent
{
    [Tooltip("Que ID de objeto nos interesa para que se active el trigger")]
    public GrabID.ID targetConditionID;

    /// <summary>
    /// Comprueba si la id del objeto es la que nos interesa para activar el evento del trigger o no
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public override bool Condition(GameObject go)
    {
        if (go.GetComponent<GrabID>())
        {
            if (go.GetComponent<GrabID>().idObject == targetConditionID)
                return true;
            else
                return false;   
        }
        
        return false;
    }
}
