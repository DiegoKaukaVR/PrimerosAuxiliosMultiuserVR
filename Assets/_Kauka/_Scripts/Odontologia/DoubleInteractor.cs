using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using System.Linq;

public class DoubleInteractor : MonoBehaviour
{
    XRNode xrNode;
    protected List<InputDevice> devices = new List<InputDevice>();
    protected InputDevice device;
    bool triggerM;
    bool gripM;
    IXRSelectInteractor currentInteractor;
    Transform socket;
    Transform socket2;


     bool desactivado;
     public bool seleccionando;

     Transform origTrans;

     bool onHand1;
     bool onHand2;

     bool onPosition;

    DoubleInteractor[] otherInteractables;

    public Transform attach1Right;
    public Transform attach2Right; 
    public Transform attach1Left;
    public Transform attach2Left;
    bool attachRight;
    bool attachLeft;

    bool otherDetected;
    // Start is called before the first frame update
    void Start()
    {
        origTrans = transform.parent;
        otherInteractables = FindObjectsOfType<DoubleInteractor>();
        GetComponent<XRGrabInteractable>().attachTransform = attach2Right;
    }

    // Update is called once per frame
    void Update()
    {
        if (seleccionando)
        {
            SaveInteractorID();
            CheckSecondInteraction();
            
            if(onHand1 && desactivado && !gripM && GetComponent<XRGrabInteractable>().enabled == false)
            {
                GetComponent<XRGrabInteractable>().enabled = true;
                DisableDeactivate();
            }
            if(onHand2 && desactivado && !triggerM && GetComponent<XRGrabInteractable>().enabled == false)
            {
                GetComponent<XRGrabInteractable>().enabled = true;
                DisableDeactivate();
            }

            if (onHand2 && !gripM)
            {
                if (GetComponent<ImanObjeto>()) { GetComponent<ImanObjeto>().CheckPosition(); }
                if (!desactivado)
                {
                    seleccionando = false;
                    onHand2 = false;
                }
                else if (desactivado)
                {
                    StartCoroutine(TiempoControl());
                    DisableDeactivate();
                    seleccionando = false;
                    onHand2 = false;
                    GetComponent<XRGrabInteractable>().enabled = true;
                }
            }
            if (onHand2 && desactivado && !triggerM)
            {
                GetComponent<XRGrabInteractable>().enabled = true;
                DisableDeactivate();
            }
            if (onHand1 && !triggerM)
            {
                if (GetComponent<ImanObjeto>()) { GetComponent<ImanObjeto>().CheckPosition(); }
                if (!desactivado)
                {
                    StartCoroutine(TiempoControl());
                    seleccionando = false;
                    onHand1 = false;
                }
                else if (desactivado)
                {
                    
                    DisableDeactivate();
                    StartCoroutine(TiempoControl());
                    seleccionando = false;
                }
            }
        }
        if(!seleccionando && onPosition)
        {
            DisableDeactivate();
        }
    }

    public void DisableDeactivate()
    {
        transform.parent = origTrans;
        desactivado = false;
        onPosition = false;
    }

    IEnumerator TiempoControl()
    {
        GetComponent<XRGrabInteractable>().enabled = false;
        onHand1 = false;
        onHand2 = false;
        yield return new WaitForSeconds(1);
        GetComponent<XRGrabInteractable>().enabled = true;
        onHand2 = false;
        onHand1 = false;
    }

    public void SaveInteractorID()
    {
        if (GetComponent<XRGrabInteractable>().enabled && seleccionando == false)
        {
            GetDevice();
            seleccionando = true;
        }
        
        device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerM);
        device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripM);
    }

    public void CheckSecondInteraction()
    {
        foreach (DoubleInteractor di in otherInteractables)
        {
            if ((di.onHand1 || di.onHand2) && di != this)
            {
                otherDetected = true;
            }
            else
            {
                otherDetected = false;
            }
        }
        foreach (DoubleInteractor di in otherInteractables)
        {
            
            if(triggerM && gripM)
            {
                if (di.onHand1 && di != this && !onHand2 && !onHand1)
                {
                    onHand2 = true;
                }
                else if (di.onHand2 && di != this && !onHand1 && !onHand2)
                {
                    onHand1 = true;
                }
            }
            else if(triggerM && !gripM && !otherDetected)
            {
                onHand1 = true;
            }
            else if(!triggerM && gripM && !otherDetected)
            {
                onHand2 = true;
            }
        }

        if (onHand1)
        {
            
            if (attachRight) { GetComponent<XRGrabInteractable>().attachTransform = attach1Right; }
            if(attachLeft) { GetComponent<XRGrabInteractable>().attachTransform = attach1Left; }
        }
        if (onHand2)
        {
            if (attachRight) { GetComponent<XRGrabInteractable>().attachTransform = attach2Right; }
            if (attachLeft) { GetComponent<XRGrabInteractable>().attachTransform = attach2Left; }
        }

         //Primer Socket
        if (triggerM && onHand2)
        {
            GetComponent<XRGrabInteractable>().enabled = false;
            transform.parent = socket;
            transform.position = socket.position;
            transform.rotation = socket.rotation;
            onPosition = true;
            desactivado = true;
        }

        //SegundoSocket
        if(onHand1 && gripM)
        {
            GetComponent<XRGrabInteractable>().enabled = false;
            transform.parent = socket2;
            transform.position = socket2.position;
            transform.rotation = socket2.rotation;
            onPosition = true;
            desactivado = true;
        }

    }

    public void GetDevice()
    {
        if (GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.name == "LeftHand") { xrNode = XRNode.LeftHand; socket = GameObject.Find("LeftHandSocket").transform; socket2 = GameObject.Find("LeftHandSocketSecond").transform; attachLeft = true; attachRight = false; }
        if (GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.name == "RightHand") { xrNode = XRNode.RightHand; socket = GameObject.Find("RightHandSocket").transform; socket2 = GameObject.Find("RightHandSocketSecond").transform; attachRight = true; attachLeft = false; }
        currentInteractor = GetComponent<XRGrabInteractable>().interactorsSelecting[0];
        InputDevices.GetDevicesAtXRNode(xrNode, devices);
        device = devices.FirstOrDefault();
    }

}
