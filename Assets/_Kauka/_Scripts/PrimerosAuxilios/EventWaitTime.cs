using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventWaitTime : MonoBehaviour
{

    public float time;
    public UnityEvent onComplete;

    public bool onStart;

    public void OnEnable()
    {
        if (onStart)
        {
            ExecuteEvent();
        }
    }

    
    public void ExecuteEvent()
    {
        StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(time);
        onComplete.Invoke();
    }
  
}
