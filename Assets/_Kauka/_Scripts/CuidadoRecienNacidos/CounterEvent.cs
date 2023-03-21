using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterEvent : MonoBehaviour
{
    public int maxNumber;
    public int currentCount;
    public UnityEvent OnCompleted;

    public void Count()
    {
        currentCount++;

        if (currentCount >= maxNumber)
        {
            currentCount = 0;
            OnCompleted.Invoke();
        }
    }

}
