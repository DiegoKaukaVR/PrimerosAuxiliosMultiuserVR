using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.SpatialTracking;
using UnityEngine.Events;
using TMPro;


/// <summary>
/// 1. Feedback UI: Valid push: Frecuency 
/// </summary>
[CanSelectMultiple(true)]
public class OnTwoGrabAnimationRCP : OnTwoGrabAnimation
{
    [Header("RCP Configuration")]

    [SerializeField] PannelRCP pannelRCP;
    [SerializeField] PannelInsuflacciones pannelInsuflacciones;
    public GameObject insulflaccionesInfo;
    [SerializeField] GameObject insuflacciones;

    bool start = false;


    [SerializeField] int indexInsuflaccion;
    int maxInsuflaccion = 2;


    [SerializeField] int cycles;

    public GameObject flechas;
    public GameObject manosPreview;

    protected bool insulflaccion;

    public GameObject pannelInfoRCP;

    protected override void OnEnable()
    {
        base.OnEnable();
       
    }

    protected override void OnExitSelected()
    {
        base.OnExitSelected();
        if (!insulflaccion)
        {
            manosPreview.SetActive(true);
        }
        else
        {
            manosPreview.SetActive(false);
        }
    }
    bool lastRound;
    protected override void SuccessPush()
    {
        //Check Frecuency with UI System

        if (!pannelRCP.MakePush())
        {
            return;
        }
      
        Debug.Log("SuccessPush");

        //Number Tracking
        actualSuccess++;
        pannelRCP.SetCounterRCP(actualSuccess);
        animator.SetTrigger("hit");
        ChangeTxtUISucess();
        hapticController.SendTotalHaptics();

        if (actualSuccess >= maxSuccess)
        {
            StopCoroutine(CoroutinePushCheck());
            StopAllCoroutines();
            actualSuccess = 0;

            if (cycles<2)
            {
                if (cycles>=1)
                {
                    lastRound = true;
                }
                cycles++;
                ChangeTxtUIInsuflacciones();
                ChangeToInsuflacciones();
            }
            else
            {
                animator.SetTrigger("Completed");
                CompletedTask();
                Debug.Log("Completed");
            }
        }
    }

    protected override void StartPush()
    {
        base.StartPush();
        

        if (!start)
        {
            start = true;
        }
    }

    void ChangeTxtUISucess()
    {
        pannelRCP.SetCounterRCP(actualSuccess);
    }
    


    void ChangeTxtUIInsuflacciones()
    {
        pannelInsuflacciones.SetTxtInsuflacciones(indexInsuflaccion);
       
    }

    void ChangeToInsuflacciones()
    {
        insulflaccion = true;
        CanBeGrabbed = false;
        flechas.SetActive(false);
        pannelInsuflacciones.gameObject.SetActive(true);
        insulflaccionesInfo.SetActive(true);
        insuflacciones.SetActive(true);
        pannelRCP.gameObject.SetActive(false);
        trackPosition = false;
        sphereCollider.enabled = false;
        sphereCollider.GetComponent<MeshRenderer>().enabled = false;
        flechas.SetActive(false);
        Debug.Log("Realice Insuflacciones: " + indexInsuflaccion.ToString() + " / 5");
        manosPreview.SetActive(false);
        pannelInfoRCP.SetActive(false);

    }
    void ChangetoRCP()
    {
        insulflaccion = false;
        pannelInsuflacciones.gameObject.SetActive(false);
        insulflaccionesInfo.SetActive(false);
        insuflacciones.SetActive(false);

        if (lastRound)
        {
            CompletedTask();
            return;
        }

        manosPreview.SetActive(true);
        CanBeGrabbed = true;
        trackPosition = true;
        Debug.Log("RCP Counter");
        ChangeTxtUISucess();
       
        pannelRCP.gameObject.SetActive(true);
        sphereCollider.enabled = true;
        sphereCollider.GetComponent<MeshRenderer>().enabled = true;
        flechas.SetActive(true);
        pannelInfoRCP.SetActive(true);
    }

    /// <summary>
    /// Called from trigger event
    /// </summary>
    public void AddInsuflaccion()
    {
        indexInsuflaccion++;
        pannelInsuflacciones.SetPorcentageImage(indexInsuflaccion, maxInsuflaccion);
        ChangeTxtUIInsuflacciones();
        if (indexInsuflaccion >= maxInsuflaccion)
        {
            indexInsuflaccion = 0;
            ChangetoRCP();
        }
    }
}