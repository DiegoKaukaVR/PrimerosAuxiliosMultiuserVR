using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using System.Linq;
using UnityEngine.Events;

public class ControllerTriggerEvent : MonoBehaviour
{
    XRNode xrNode;
    protected List<InputDevice> devices = new List<InputDevice>();
    protected InputDevice device;
    bool interactuando;
    bool triggerM;

    bool puedeHacerAccion;
    public UnityEvent accionARealizar;
    private void Start()
    {
        puedeHacerAccion = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (interactuando)
        {
            GetDevice();
            device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerM);
            if(triggerM && puedeHacerAccion)
            {
                accionARealizar.Invoke();
                puedeHacerAccion = false;

            }
            if(!triggerM && !puedeHacerAccion)
            {
                puedeHacerAccion = true;
            }
        }
    }

    public void Soltar()
    {
        interactuando = false;
        puedeHacerAccion = true;
    }

    public void Interactuar()
    {
        interactuando = true;
    }

    public void GetDevice()
    {
        if (GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.name == "LeftHand") { xrNode = XRNode.LeftHand; }
        if (GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.name == "RightHand") { xrNode = XRNode.RightHand; }
        InputDevices.GetDevicesAtXRNode(xrNode, devices);
        device = devices.FirstOrDefault();
    }
}
