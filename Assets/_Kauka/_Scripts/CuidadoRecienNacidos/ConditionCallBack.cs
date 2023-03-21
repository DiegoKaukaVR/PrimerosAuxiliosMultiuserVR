using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConditionCallBack : MonoBehaviour
{

    public bool isAvaible;

    public UnityEvent onSucces;
    public UnityEvent onFailed;

    public void MakeAvaible(bool value)
    {
        isAvaible = value;
    }

    public void CheckCondition()
    {
        if (isAvaible)
        {
            onSucces.Invoke();
        }
        else
        {
            onFailed.Invoke();
        }
    }


}
