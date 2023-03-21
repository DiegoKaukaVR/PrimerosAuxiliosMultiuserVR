using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Process : MonoBehaviour
{
    public int moduleID;
    public Modo currentModo;
    public enum Modo
    {
        formativo,
        evaluativo
    }



    int currentEvent;
    public List<StepEvent> eventList;

    EvaluationBase evaluation;
    [Serializable]
    public struct StepEvent
    {
        public string name;
        public UnityEvent unityStartEvent;
        public UnityEvent unityFinishEvent;
        public ConditionEvent conditionEvent;
    }

    public static Process instance;
    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        GameManager.instance.currentProcess = this;
        GameManager.instance.moduleIndex = moduleID;

    }

    
    protected virtual void Start()
    {
        if (currentModo == Modo.formativo)
        {
            GameManager.instance.currentModulo = GameManager.Modulo.formativo;
        }
        if (currentModo == Modo.evaluativo)
        {
            GameManager.instance.currentModulo = GameManager.Modulo.evaluativo;
            evaluation = GetComponent<EvaluationBase>();
        }



        
        StartLogic();
    }
    public virtual void StartLogic()
    {
        if (eventList.Count == 0)
        {
            Debug.LogWarning("No elements in event list.");
            return;
        }
        currentEvent = 0;
        StartStepEvent();
    }

    public virtual void ResetLogic()
    {
        currentEvent = 0;

    }

    void StartStepEvent()
    {
        eventList[currentEvent].unityStartEvent.Invoke();
    }

    /// <summary>
    /// When is completed and need to start next step
    /// </summary>
    public virtual void FinishStepEvent()
    {
        if (currentEvent>=eventList.Count)
        {
            OnCompleted();
                return;
        }


        Debug.Log(eventList[currentEvent].name + " has being completed, next step event incoming... ");
        eventList[currentEvent].unityFinishEvent.Invoke();
        NextStepEvent();
    }

    /// <summary>
    /// Go to next step event in X time
    /// </summary>
    bool coroutineStart;
    public virtual void FinishStepEventInRealSeconds(float time)
    {
        if (coroutineStart)
        {
            return;
        }

        StopCoroutine(CoroutineNextState(time));
        StartCoroutine(CoroutineNextState(time));
    }

    IEnumerator CoroutineNextState(float time)
    {
        coroutineStart = true;
        yield return new WaitForSecondsRealtime(time);
        coroutineStart = false;
        FinishStepEvent();

    }

    /// <summary>
    /// Cambias el event al siguiente
    /// </summary>
    protected void NextStepEvent()
    {
        currentEvent++;
        if (currentEvent >= eventList.Count)
        {
            OnCompleted();
            return;
        }
        else
        {
            StartStepEvent();
        }
    }

    public void GoToEvent(string name)
    {
        for (int i = 0; i < eventList.Count; i++)
        {
            if (eventList[i].name == name)
            {
                currentEvent = i;
                eventList[i].unityStartEvent.Invoke();
            }
        }
    }

    public virtual void OnCompleted()
    {
        Debug.Log("All step events completed");
        
        if (currentModo == Modo.evaluativo)
        {
            evaluation.FinalEvaluation();
        }

        StopAllCoroutines();

        GameManager.instance.FinishExperience();
        this.enabled = false;
    }

  
}
