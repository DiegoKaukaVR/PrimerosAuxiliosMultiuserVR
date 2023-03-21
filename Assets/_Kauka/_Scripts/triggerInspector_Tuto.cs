using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;

public class triggerInspector_Tuto : MonoBehaviour
{

    //public instrumentacion instrumentacion;

    public bool usoTag;
    public string Tag;
    public bool usoTagSegundo;
    public string TagSegundo;
    public scr_StepController stp_ctrl;
    [SerializeField]
    UnityEvent onTriggerEnterEvent, onTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (usoTag && usoTagSegundo)
        {
            if (other.tag == Tag || other.tag == TagSegundo)
            {
                onTriggerEnterEvent.Invoke();
            }
        }
        else if (usoTag && !usoTagSegundo)
        {
            if (other.tag == Tag)
            {
                onTriggerEnterEvent.Invoke();    
            }
        }
        else
        {
            onTriggerEnterEvent.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (usoTag && usoTagSegundo)
        {
            if(other.tag == Tag || other.tag == TagSegundo)
            {
                onTriggerExitEvent.Invoke();
            }
        }
        else if (usoTag && !usoTagSegundo)
        {
            if (other.tag == Tag)
            {
                onTriggerExitEvent.Invoke();
            }
        }
        else
        {
            onTriggerExitEvent.Invoke();
        }
    }
}
