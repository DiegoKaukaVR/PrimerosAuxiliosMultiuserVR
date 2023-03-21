using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EvaluationBase : MonoBehaviour
{
    [Header("Evaluation Data")]
    public List<Evaluation> evaluation;
    public List<bool> evaluationList;
 
    protected void Start()
    {
        for (int i = 0; i < evaluation.Count; i++)
        {
            evaluationList.Add(false);
        }
       
    }


    [Serializable]
    public struct Evaluation
    {
        public string name;
        
    }

    public void FinalEvaluation()
    {
        Debug.Log("Final evaluation on this module is: " + SumEvaluation() + "/ " + evaluation.Count);
    }

    public void EvaluateSucess(int index)
    {
        if (index > evaluation.Count)
        {
            Debug.LogError("Index por encima... revisar");
            return;
        }

        evaluationList[index] = true;
    }
    public void EvaluateFail(int index)
    {
        if (index > evaluation.Count)
        {
            Debug.LogError("Index por encima... revisar");
            return;
        }

        evaluationList[index] = false;
    }

    [Header("Sequencial Logic")]
    [Space(10)]


    public List<int> actionIndexEvaluation;
    public List<Actions> actionOrder;
    public enum Actions
    {
        Rama,
        Valla,
        Movil
    }

    /// <summary>
    /// El orden de ejecución de las acciones
    /// </summary>
    int indexOrder = 0;



    /// <summary>
    /// 1. Recorre la lista de ActionOrder introducida por el usuario y compara la acción con el input. 
    /// 2. Compara el order de ejecución actuaL con el que debería tener la acción.
    /// 3. Si es exacto marca como acto el acto que corresponde.
    /// 4. Si está por encima invalida todos los anteriores porque significa que 
    /// </summary>
    /// 

    /// Ejemplo: Si introduces en la primera acción el Movil ¿Qué ocurre? 

    public void AddActionToBuffer(Actions addedAction)
    {
        for (int i = 0; i < actionOrder.Count; i++)
        {
            if (addedAction == actionOrder[i])
            {
                if (i == indexOrder)
                {
                    indexOrder++;
                    EvaluateSucess(actionIndexEvaluation[i]);
                    return;
                }

                if (i > indexOrder)
                {
                    Debug.Log("Estás haciendo la acción después de tiempo, anula toda las anteriores");
                    EvaluateSucess(actionIndexEvaluation[i]);
                    for (int j = 0; j < i; j++)
                    {
                        if (evaluationList[j] == true)
                            continue;
                        else
                        {
                            EvaluateFail(j);
                        }
                    }
                            
                }

                if (i < indexOrder)
                {
                    Debug.Log("Estás haciendo la acción antes de tiempo");
                    EvaluateFail(i);
                }

                break;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual int SumEvaluation()
    {
        int currentMark = 0;
        for (int i = 0; i < evaluation.Count; i++)
        {
            if (evaluationList[i] == true)
            {
                currentMark++;
            }
        }

        return currentMark;
    }
}
