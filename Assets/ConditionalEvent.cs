using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConditionalEvent : MonoBehaviour
{
    public bool condition = true;
    public UnityEvent conditionalEvent;

    public void StartEvent()
    {
        if (!condition)
        {
            return;
        }

        conditionalEvent.Invoke();
        condition = false;
    }

    public void SetConditionTrue()
    {
        condition = true;
    }
    public void SetConditionFalse()
    {
        condition = false;
    }
}
