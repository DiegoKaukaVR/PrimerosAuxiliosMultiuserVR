using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnlyOnceEvent : MonoBehaviour
{
    public UnityEvent OnOnceEvent;
    bool once;
    public void OnceEvent()
    {
        if (!once)
        {
            once = true;
            OnOnceEvent.Invoke();
        }
     
    }
}
