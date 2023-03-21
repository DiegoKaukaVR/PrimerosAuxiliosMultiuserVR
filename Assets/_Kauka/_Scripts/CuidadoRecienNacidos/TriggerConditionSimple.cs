using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TriggerConditionSimple : MonoBehaviour
{
    public TriggerEnter triggerEnter;
    public TriggerStay triggerStay;
    public TriggerExit triggerExit;

   
    [Serializable]
    public struct TriggerEnter
    {
        [Header("ENTER")]
        public bool enterEnable;
        public bool enterCondition;
        public UnityEvent onTriggerEnter;

        public List<ConditionEvent> conditionEventListEnter;
    }

    [Serializable]
    public struct TriggerStay
    {
        [Header("STAY")]
        public bool stayEnable;
        public bool stayCondition;
        public UnityEvent onTriggerStay;
        public List<ConditionEvent> conditionEventListStay;
    }

    [Serializable]
    public struct TriggerExit
    {
        [Header("Exit")]
        public bool exitEnable;
        public bool exitCondition;
        public UnityEvent onTriggerExit;
        public List<ConditionEvent> conditionEventListExit;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!triggerEnter.enterEnable)
        {
            return;
        }
        if (triggerEnter.enterCondition)
        {
            if (!CheckAllConditionEnter(other.gameObject))
            {
                return;
            }
        }
       
        triggerEnter.onTriggerEnter.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!triggerStay.stayEnable)
        {
            return;
        }

        if (triggerStay.stayCondition)
        {
            if (!CheckAllConditionEnter(other.gameObject))
            {
                return;
            }
        }

        triggerStay.onTriggerStay.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!triggerExit.exitEnable)
        {
            return;
        }
        if (triggerExit.exitEnable)
        {
            if (!CheckAllConditionEnter(other.gameObject))
            {
                return;
            }
        }

        triggerExit.onTriggerExit.Invoke();

    }

    protected bool CheckAllConditionEnter(GameObject go)
    {

        for (int i = 0; i < triggerEnter.conditionEventListEnter.Count; i++)
        {
            if (triggerEnter.conditionEventListEnter[i].Condition(go))
            {
                return true;
            }
        }

        //No condition triggered
        return false;
    }
    protected bool CheckAllConditionStay(GameObject go)
    {

        for (int i = 0; i < triggerStay.conditionEventListStay.Count; i++)
        {
            if (triggerStay.conditionEventListStay[i].Condition(go))
            {
                return true;
            }
        }

        //No condition triggered
        return false;
    }
    protected bool CheckAllConditionExit(GameObject go)
    {

        for (int i = 0; i < triggerExit.conditionEventListExit.Count; i++)
        {
            if (triggerExit.conditionEventListExit[i].Condition(go))
            {
                return true;
            }
        }

        //No condition triggered
        return false;
    }
}
