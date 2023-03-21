using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class MenuInteractivo : MonoBehaviour
{
    public GameObject AVDN;
    public UnityEvent AVDNEvent;

    public GameObject FrenteMenton;
    public UnityEvent FrenteMentonEvent;

    public GameObject ComprobarRespiracion;
    public UnityEvent ComprobarRespiracionEvent;

    public GameObject DesnudarPecho;
    public UnityEvent DesnudarPechoEvent;

    public GameObject RCP;
    public UnityEvent RCPEvent;


    public void SetUpAVDN()
    {
        CloseAllInteraction();
        AVDN.SetActive(true);
        AVDNEvent.Invoke();
    }

    public void SetUpFrenteMenton()
    {
        CloseAllInteraction();
        FrenteMenton.SetActive(true);
        FrenteMentonEvent.Invoke();
    }

    public void SetUpComprobarRespiracion()
    {
        CloseAllInteraction();
        ComprobarRespiracion.SetActive(true);
        ComprobarRespiracionEvent.Invoke();
    }

    public void SetUpDesnudarPecho()
    {
        CloseAllInteraction();
        DesnudarPecho.SetActive(true);
        DesnudarPechoEvent.Invoke();
    }

    public void SetUpRCP()
    {
        CloseAllInteraction();
        RCP.SetActive(true);
        RCPEvent.Invoke();
    }

    void CloseAllInteraction()
    {
        AVDN.SetActive(false);
        FrenteMenton.SetActive(false);
        ComprobarRespiracion.SetActive(false);
        DesnudarPecho.SetActive(false);
        RCP.SetActive(false);
    }






}
