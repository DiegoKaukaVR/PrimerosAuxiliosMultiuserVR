using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionEvent : MonoBehaviour
{
    

    public virtual bool Condition(GameObject GO)
    {
        Debug.LogError("Need to override condition");
        return false;
    }
}
