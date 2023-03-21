using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField] string _id;
    [SerializeField] string _name;
    CardManager cardManager;

    public UnityEvent onCorrectSelection;
    public UnityEvent onFailedSelection;

    private void Start()
    {
        cardManager = GetComponentInParent<CardManager>();
    }

    public void SelectId()
    {
        if (cardManager.CheckCorrectValidation(_id))
        {
            onCorrectSelection.Invoke();
        }
        else
        {
            onFailedSelection.Invoke();
        }
    }
}
