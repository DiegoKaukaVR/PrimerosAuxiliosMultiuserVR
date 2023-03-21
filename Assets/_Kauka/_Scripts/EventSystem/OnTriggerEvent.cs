using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using Tools;

[RequireComponent(typeof(BoxCollider))]
public class OnTriggerEvent : MonoBehaviour
{
    [SerializeField] protected LayerMask targetLayer;
 
    protected BoxCollider boxCollider;

    public UnityEvent onEnterTrigger;

    public UnityEvent onExitTrigger;

    [Tooltip("This is called when no condition is trigger (ie, wrong tag")]
    public UnityEvent onEnterTriggerFail;


    [SerializeField] protected LayerMask secondaryTargetLayer;
    [Tooltip("This is called when secondary layer")]
    public UnityEvent onSecondaryEnterTrigger;


    [SerializeField] bool timer;
    bool triggered;
    [SerializeField] float currentTimeInside;
    [SerializeField] float timeToTrigger;
    [SerializeField] float maxTimeFail;
    [SerializeField] bool notResetTimer;
    public UnityEvent onStayTrigger;
    public UnityEvent onTimerTrigger;
 
    [Tooltip("This is called when timmer is over (ie, stay in a trigger more than X time)")]
    public UnityEvent onStayTriggerFail;

    public List<ConditionEvent> conditionEventList;
    bool hasCondition;

    public bool disable;
  
    private void Start()
    {
        if (conditionEventList.Count > 0)
        {
            hasCondition = true;
        }

        if (maxTimeFail > 0)
        {
            hasExceedFailTime = true;
        }
    }

    protected bool CheckAllCondition(GameObject go)
    {
        for (int i = 0; i < conditionEventList.Count; i++)
        {
            if (conditionEventList[i].Condition(go))
            {
                return true;
            }
        }

        //No condition triggered
        return false;
    }
    
 
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!DetectLayer.LayerInLayerMask(other.gameObject.layer, targetLayer))
        {
            if (DetectLayer.LayerInLayerMask(other.gameObject.layer, secondaryTargetLayer))
            {
                onSecondaryEnterTrigger.Invoke();
            }
            else
            {
                onEnterTriggerFail.Invoke();
                return;
            }
          
        }

        if (hasCondition && !CheckAllCondition(other.gameObject))
        {
            onEnterTriggerFail.Invoke();
            return;
        }

        onEnterTrigger.Invoke();
    }

    bool triggerStayInvoke;
    [SerializeField] bool useStayTrigger;
    bool hasExceedFailTime;
    bool completed;

    protected virtual void OnTriggerStay(Collider other)
    {
        if (completed)
        {
            return;
        }
        if (!DetectLayer.LayerInLayerMask(other.gameObject.layer, targetLayer))
        {
            return;
        }

        if (useStayTrigger==false)
        {
            return;
        }

        if (!hasCondition)
        {
            onStayTrigger.Invoke();
        }     


        if (timer)
        {
            currentTimeInside += Time.deltaTime;

            if (currentTimeInside > timeToTrigger && !triggered)
            {

                triggered = true;
                onTimerTrigger.Invoke();
            }

            if (hasExceedFailTime)
            {
                if (currentTimeInside > maxTimeFail)
                {
                    Debug.Log("OnstayTriggerFail");
                    currentTimeInside = 0;
                    onStayTriggerFail.Invoke();
                }
            }
           
        }
        else
        {
            if (hasCondition && CheckAllCondition(other.gameObject))
            {
                if (triggerStayInvoke)
                {
                    return;
                }
                onStayTrigger.Invoke();
                completed = true;
                triggerStayInvoke = true;
                this.enabled = false;
                return;
            }
        }


    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (DetectLayer.LayerInLayerMask(other.gameObject.layer, targetLayer))
        {
            onExitTrigger.Invoke();
        }

        ResetLogic();
    }

    void ResetLogic()
    {
        triggerStayInvoke = false;
        triggered = false;

        if (!notResetTimer)
        {
            currentTimeInside = 0;
        }
      
    }



    public void OnEnterTrigger()
    {
        onEnterTrigger.Invoke();
    }

    public void OnExitTrigger()
    {
        onExitTrigger.Invoke();
    }

    protected void OnValidate()
    {
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();
      
        if (boxCollider.isTrigger == false)
            boxCollider.isTrigger = true;
    }

    [SerializeField] bool debug = false;

    public void SetDisable()
    {
        disable = true;
    }

    private void OnDisable()
    {
        //if (DetectLayer.LayerInLayerMask(8, targetLayer))
        //{
        //    GameManager.instance.SetDummyPlayer(false);
        //}
    }
    private void OnEnable()
    {
        if (disable)
        {
            this.enabled = false;
        }

        //if (DetectLayer.LayerInLayerMask(8, targetLayer))
        //{
        //    GameManager.instance.SetDummyPlayer(true);
        //}

            completed = false;
       
    }

    private void OnDrawGizmos()
    {
        if (!debug)
        {
            return;
        }
        Gizmos.color = new Color(0, 1, 0, 0.1f);
        Gizmos.DrawCube(transform.position, boxCollider.size);
    }

}
