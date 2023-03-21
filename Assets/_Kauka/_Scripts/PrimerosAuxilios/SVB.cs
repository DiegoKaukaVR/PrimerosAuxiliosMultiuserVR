using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controls all logic of SVB module II from "Primeros Auxilios"
/// </summary>
public class SVB : Process
{

    [SerializeField] Animator animator;
    [SerializeField] Animator animatorMadre;

   

    public UnityEvent LloroEvent;

   
    public void Lloro()
    {
        LloroEvent.Invoke();
        Debug.Log("Niña llorando");
        //Audio
        
    }

}
