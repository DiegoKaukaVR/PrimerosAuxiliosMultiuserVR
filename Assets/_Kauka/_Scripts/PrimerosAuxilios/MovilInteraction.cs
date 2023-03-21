using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

/// <summary>
/// Para detectar si has pulsado el botón A , tienes que encender y apagar el script. 
/// </summary>
public class MovilInteraction : MonoBehaviour
{
    [Header("Movil Configuration")]

    PreguntasRespuestasMovil prMovil;
   
    [SerializeField] XRController rightController;

    public UnityEvent OnStartCall;
    bool onlyTime;

    private void Start()
    {
        prMovil = GetComponent<PreguntasRespuestasMovil>();
        rightController = GameManager.instance.rightController;
        this.enabled = false;
    }
    void Update()
    {
        //if (rightController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryRight))
        //{
        //    if (primaryRight)
        //    {
        //        prMovil.StartCall();
        //        OnStartCall.Invoke();
        //        Debug.Log("Calling emergency");
        //        onlyTime = true;
        //        this.enabled = false;
        //    }
        //}
    }

    public void MovilCall()
    {
        if (!onlyTime)
        {
            prMovil.StartCall();
            OnStartCall.Invoke();
            Debug.Log("Calling emergency");
            onlyTime = true;
            this.enabled = false;
        }
    }

    private void OnEnable()
    {
        if (onlyTime)
        {
            this.enabled = false;
        }
    }


}
