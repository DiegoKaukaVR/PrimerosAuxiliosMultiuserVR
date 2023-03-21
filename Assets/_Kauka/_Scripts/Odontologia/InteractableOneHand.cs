using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using System.Linq;

[CanSelectMultiple(true)]
public class InteractableOneHand : XRGrabInteractable
{
    public bool rightHand;
    public bool leftHand;
    public Transform[] attachPoints;
    public bool usingTwoFingers;

    protected List<InputDevice> devices = new List<InputDevice>();
    protected InputDevice device;
    XRNode xrNode;
    bool triggerM;
    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractor interactor = selectingInteractor;

        IXRSelectInteractor newInteractor = firstInteractorSelecting;

        List<IXRSelectInteractor> morInteractors = interactorsSelecting;
    }

    private void Awake()
    {
        base.Awake();

    }
  
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (RightHand())
        {
            leftHand = false;
            rightHand = true;
            xrNode = XRNode.RightHand;
            InputDevices.GetDevicesAtXRNode(xrNode, devices);
            device = devices.FirstOrDefault();
        }
        if (LeftHand())
        {
            rightHand = false;
            leftHand = true;
            xrNode = XRNode.LeftHand;
            InputDevices.GetDevicesAtXRNode(xrNode, devices);
            device = devices.FirstOrDefault();
        }

        if(device!=null)
        {
            device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerM);
            if (triggerM) { usingTwoFingers = true; }
            else { usingTwoFingers = false; }
        }
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (HasNoInteractors())
        {
            rightHand = false;
            leftHand = false;
        }
        base.OnSelectExited(args);
        
    }
    private bool HasMultipleInteractors()
    {
        return interactorsSelecting.Count > 1;
    }

    private bool HasNoInteractors()
    {
        return interactorsSelecting.Count == 0;
    }

    private bool RightHand()
    {
        return interactorsSelecting[0].transform.gameObject.name == "RightHand";
    }
    private bool LeftHand()
    {
        return interactorsSelecting[0].transform.gameObject.name == "LeftHand";
    }
}
