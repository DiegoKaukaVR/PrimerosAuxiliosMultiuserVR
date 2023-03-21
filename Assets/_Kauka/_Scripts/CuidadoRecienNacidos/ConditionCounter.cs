using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ConditionCounter : ConditionBase
{
    [Header("Counter Configuration")]
    [SerializeField] int currentCounter;
    [SerializeField] int maxCounter;

    public UnityEvent onCompleteCondition;

    public void AddCounter()
    {
        currentCounter++;

        if (currentCounter >= maxCounter)
        {
            currentCounter = 0;
            onCompleteCondition.Invoke();
        }

    }




    
}
