using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] Transform objetive;
    public float rangeAttach = 1f;

    public UnityEvent attachEvent;

    public UnityEvent registerAttachEvent;

    public bool attached = true;
    bool canAttach;

    public bool registerAttach;

    public Vector3 rotationOffset;

    DebugZone debugZone;

    XRController xrControllerRight;
    XRController xrControllerLeft;
    public bool right;

    XRSimpleInteractuableMovil movilInteractuable;

    private void Awake()
    {
        if (objetive == null)
        {
            objetive = GameManager.instance.SocketTransformSimulation;
        }


        xrControllerRight = GameManager.instance.rightController;
        xrControllerLeft = GameManager.instance.leftController;
        movilInteractuable = GetComponent<XRSimpleInteractuableMovil>();

    }

    public void SetAvaibleDebug(bool value)
    {
        debugZone.SetMeshDisplay(value);
    }


    private void Start()
    {
        debugZone = GameManager.instance.xrRig.GetComponentInChildren<DebugZone>();
        debugZone.SetScale(rangeAttach);
        debugZone.SetMeshDisplay(false);
    }
    public void Take()
    {
        attached = false;
        canAttach = false;
    }

    public void CanAttach()
    {
        canAttach = true;
    }

    public void RegisterAttach()
    {
        registerAttach = true;
    }
    private void Update()
    {
        if (attached)
        {
            if (registerAttach)
            {
                registerAttach = false;
                registerAttachEvent.Invoke();
               
            }
            transform.position = objetive.position;
            transform.eulerAngles = rotationOffset;
        }
        else
        {
            if (!canAttach)
            {
                return;
            }
            if (right)
            {
                if (Vector3.Distance(xrControllerRight.transform.position, objetive.position) < rangeAttach)
                {
                    transform.position = objetive.position;
                    transform.eulerAngles = rotationOffset;
                    attachEvent.Invoke();
                    attached = true;
                    movilInteractuable.MovilOff();
                }
            }
            else
            {
                if (Vector3.Distance(xrControllerLeft.transform.position, objetive.position) < rangeAttach)
                {
                    transform.position = objetive.position;
                    transform.eulerAngles = rotationOffset;
                    attachEvent.Invoke();
                    attached = true;
                    movilInteractuable.MovilOff();
                }
            }
           
            
        }
      
    }
}
