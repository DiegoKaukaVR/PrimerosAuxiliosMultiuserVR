using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSequence : MonoBehaviour
{
    [SerializeField] EvaluationBase.Actions actionType;
    EvaluationBase evaluationBase;

    private void Start()
    {
        evaluationBase = GetComponentInParent<EvaluationBase>();
    }

    public void AddActionToBuffer()
    {
        evaluationBase.AddActionToBuffer(actionType);
    }
}
