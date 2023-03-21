using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConditionControler : MonoBehaviour
{
    public List<ConditionBase> conditionList;
    public UnityEvent onSuccesEvent;

    public void ChangeConditionStateTrue(int index)
    {
        conditionList[index]._value = true;

        if (CheckAllConditions())
        {
            onSuccesEvent.Invoke();
        }
    }


    /// <summary>
    /// Returns true if ALL conditions are success, if only 1 condition is false will return negative
    /// </summary>
    bool CheckAllConditions()
    {
        for (int i = 0; i < conditionList.Count; i++)
        {
            if (conditionList[i]._value == false)
            {
                return false;
            }
        }

        Debug.Log("All conditions are satisfied");
        return true;
    }




}
