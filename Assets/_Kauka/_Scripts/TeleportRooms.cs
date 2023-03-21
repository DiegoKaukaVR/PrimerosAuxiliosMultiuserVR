using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Tools;

public class TeleportRooms : MonoBehaviour
{
    public bool isAvaible = true;
    
    TeleportRoomsManager teleportManager;

    XRController leftController;
    XRController rightController;

    [SerializeField]
    [Space(10)] LayerMask playerLayer;
  

    [Tooltip("El ID identificativo del teleport")]
    public int idTeleport;

    [Tooltip("A donde va a teletransportarse")]
    public int idTarget;

    public UnityEvent OnTeleport;

    public void MakeAvaible()
    {
        isAvaible = true;
    }

    public void MakeDisable()
    {
        isAvaible = false;
    }

    private void Start()
    {
        teleportManager = GetComponentInParent<TeleportRoomsManager>();
        leftController = GameManager.instance.leftController;
        rightController = GameManager.instance.rightController;
    }

    public GameObject UITextPreshA;

    private void OnTriggerEnter(Collider other)
    {
        if (!isAvaible)
        {
            return;
        }
        if (DetectLayer.LayerInLayerMask(other.transform.gameObject.layer, playerLayer))
        {
            UITextPreshA.SetActive(true);
        }
       
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isAvaible)
        {
            return;
        }
        //if (DetectLayer.LayerInLayerMask(other.transform.gameObject.layer, playerLayer))
        //{
        //    if (ConditionInputA())
        //    {
        //        Teleport();
        //    }
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isAvaible)
        {
            return;
        }
        if (DetectLayer.LayerInLayerMask(other.transform.gameObject.layer, playerLayer))
        {
            UITextPreshA.SetActive(false);
        }
     
    }

    public void Teleport()
    {
        Debug.Log("Teleport");
        OnTeleport.Invoke();
        teleportManager.TeleportPlayer(idTarget);
    }

    /// <summary>
    /// Cuando el botón principal del mando izquierdo es pulsado el trigger ejecutará el evento
    /// </summary>
    /// <returns></returns>
    public bool ConditionInputA()
    {
        if (rightController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryRight) && leftController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryLeft))
        {
            if (primaryRight || primaryLeft)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }

    Vector3 targetPos;
    private void OnDrawGizmosSelected()
    {
        if (teleportManager == null)
        {
            teleportManager = GetComponentInParent<TeleportRoomsManager>();
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one* 0.3f);
        if (idTarget<teleportManager.teleportRooms.Length)
        {
            targetPos = teleportManager.teleportRooms[idTarget].transform.position;
            Gizmos.DrawCube(targetPos, Vector3.one * 0.3f);
            Gizmos.DrawLine(transform.position, targetPos);
        }
       

       
    }


}
