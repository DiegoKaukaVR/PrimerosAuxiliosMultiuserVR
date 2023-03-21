using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TotalCondition : MonoBehaviour
{
    public List<ConditionBase> listConditions = new List<ConditionBase>();
    public UnityEvent OnCompletedEvent;

    public void SetActiveCondition(int index)
    {
        listConditions[index].ChangeToTrue();

        if (CheckCompleted())
        {
            OnCompletedEvent.Invoke();
        }
    }

    bool CheckCompleted()
    {
        bool completed = true;

        for (int i = 0; i < listConditions.Count; i++)
        {
            if (listConditions[i] == false)
            {
                completed = false;
            }
        }

        if (completed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
