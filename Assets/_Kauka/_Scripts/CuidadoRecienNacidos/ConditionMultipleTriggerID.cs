using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionMultipleTriggerID : ConditionEvent
{
    public List<TriggerID.IDTrigger> multipleTriggerID;

    public override bool Condition(GameObject go)
    {
        TriggerID triggerID = go.GetComponent<TriggerID>();
        if (triggerID == null)
        {
           
            triggerID = go.GetComponentInChildren<TriggerID>();
            if (triggerID == null)
            {
                triggerID = go.GetComponentInParent<TriggerID>();
                if (triggerID == null)
                {
                    Debug.LogError("No Trigger ID");
                    return false;
                }
            }
            
        }
        if (triggerID)
        {
            for (int i = 0; i < multipleTriggerID.Count; i++)
            {
                if (triggerID.currentID == multipleTriggerID[i])
                    return true;
            }
            return false;
            
        }

        return false;
    }



}
