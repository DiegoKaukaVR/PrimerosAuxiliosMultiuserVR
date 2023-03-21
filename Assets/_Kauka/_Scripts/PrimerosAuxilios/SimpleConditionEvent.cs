using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class SimpleConditionEvent : MonoBehaviour
{

    public bool Condition;
    public UnityEvent unityEvents;

    public bool off;

    public void ChangeCondition(bool value)
    {
        Condition = value;
    }

    public void EvaluateCondition()
    {
        if (Condition && !off)
        {
            off = true;
            unityEvents.Invoke();
        }
    }


}
