using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardManager : MonoBehaviour
{
    public string correctID;

    public UnityEvent onCorrectSelection;
    public UnityEvent onFailedSelection;

    public bool CheckCorrectValidation(string validationId)
    {
        if (string.Equals(correctID, validationId))
        {
            onCorrectSelection.Invoke();
            return true;
        }
        else
        {
            onFailedSelection.Invoke();
            return false;
        }
    }

}
