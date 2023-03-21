using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateCondition : MonoBehaviour
{
 
    [SerializeField] ConditionBase[] conditionArray;
    [SerializeField] int[] indexEvaluation;
    EvaluationBase evaluationMaster;

    private void Start()
    {
        conditionArray = GetComponents<ConditionBase>();
        evaluationMaster = GetComponentInParent<EvaluationBase>();
    }

    public void EvaluateConditions()
    {
        for (int i = 0; i < conditionArray.Length; i++)
        {
            if (conditionArray[i]._value == true)
            {
                evaluationMaster.evaluationList[indexEvaluation[i]] = false;
            }
            else
            {
                evaluationMaster.evaluationList[indexEvaluation[i]] = true;
            }
        }
    }

    private void OnValidate()
    {
        conditionArray = GetComponents<ConditionBase>();
        
    }
}
