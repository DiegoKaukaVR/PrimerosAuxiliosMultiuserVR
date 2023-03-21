using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiquiConfig : MonoBehaviour
{
    [SerializeField] List<OnTriggerEvent> triggerEventList = new List<OnTriggerEvent>();
    OnTriggerEvent[] arraytriggers;

    [SerializeField] List<OnGrabAnimation> grabList = new List<OnGrabAnimation>();
    OnGrabAnimation[] OnGrabAnimations;

    private void Start()
    {
        Invoke("LateStart", 0.1f);
    }
    void LateStart()
    {
        RefreshList();
    }
    void RefreshList()
    {
        arraytriggers = GetComponentsInChildren<OnTriggerEvent>();
        triggerEventList.Clear();

        foreach (OnTriggerEvent trigger in arraytriggers)
        {
            if (triggerEventList.Contains(trigger))
                 continue;
            
            triggerEventList.Add(trigger);
        }

       
        OnGrabAnimations = GetComponentsInChildren<OnGrabAnimation>();
        grabList.Clear();
        foreach (OnGrabAnimation grabs in OnGrabAnimations)
        {
            if (grabList.Contains(grabs))
                continue;

            grabList.Add(grabs);
        }

    }

    private void OnValidate()
    {
        RefreshList();
    }
}