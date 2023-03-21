using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerTracker : MonoBehaviour
{

    [SerializeField] Queue<bool> checklist = new Queue<bool>();
    [SerializeField] List<TriggerSimple> triggerList = new List<TriggerSimple>();

    public UnityEvent onComplete;

    private void Start()
    {
        InitiateChecklist();
    }
    void InitiateChecklist()
    {
        for (int i = 0; i < triggerList.Count; i++)
        {
            checklist.Enqueue(false);
        }
    }

    /// <summary>
    /// Se llama desde los triggers cuando son ejecutados
    /// </summary>
    public void CompleteTrigger()
    {
        checklist.Dequeue();
        if (CheckComplete())
            onComplete.Invoke();    
    }

    bool CheckComplete()
    {
        if(checklist.Count == 0) { return true; }
        else
            return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (triggerList == null || triggerList.Count == 0)
        {
            return;
        }
        for (int i = 0; i < triggerList.Count; i++)
        {
            if (triggerList[i] == null)
                continue;
           
            Gizmos.DrawCube(triggerList[i].transform.position, Vector3.one * 0.1f);
        }
    }
}
