using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerSimple : MonoBehaviour
{
    [SerializeField] LayerMask targetlayer;
    [Tooltip("Que ID de objeto nos interesa para que se active el trigger")]
    public GrabID.ID targetConditionID;

    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (!Tools.DetectLayer.LayerInLayerMask(other.gameObject.layer, targetlayer) || !ConditionID(other.gameObject))
        {
            return;
        }
        
        onTriggerEnter.Invoke();

    }

    private void OnTriggerExit(Collider other)
    {
        if (!Tools.DetectLayer.LayerInLayerMask(other.gameObject.layer, targetlayer) || !ConditionID(other.gameObject))
        {
            return;
        }

        onTriggerExit.Invoke();
    }

    /// <summary>
    /// Comprueba si la id del objeto es la que nos interesa para activar el evento del trigger o no
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public virtual bool ConditionID(GameObject go)
    {
        if (go.GetComponent<GrabID>())
        {
            if (go.GetComponent<GrabID>().idObject == targetConditionID)
                return true;
            else
                return false;
        }

        return false;
    }


}
