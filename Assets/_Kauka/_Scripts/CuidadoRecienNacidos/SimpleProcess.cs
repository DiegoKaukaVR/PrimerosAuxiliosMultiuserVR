using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


public class SimpleProcess : MonoBehaviour
{
    public UnityEvent OnCompleteProcess;
    public List<StepEvent> ListEvents = new List<StepEvent>();
    int currentIndex;

    [Serializable]
    public struct StepEvent
    {
        public string name;
        public UnityEvent unityStartEvent;
        public UnityEvent unityFinishEvent;
        public ConditionEvent conditionEvent;
    }

    public void StartProcess()
    {
        if (ListEvents.Count == 0)
        {
            Debug.LogWarning("No events in list");
            return;
        }

        currentIndex = 0;
        ListEvents[0].unityStartEvent.Invoke();
    }

    public void NextEvent()
    {
        ListEvents[currentIndex].unityFinishEvent.Invoke();
        currentIndex++;

        if (currentIndex > ListEvents.Count)
        {
            OnCompleteProcess.Invoke();
            return;
        }
        
        ListEvents[currentIndex].unityStartEvent.Invoke();
    }



}
