using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class XRSimpleInteractuableMovil : XRSimpleInteractable
{
    MovilInteraction movilInteraction;
    ControlMeshes controMeshes;
    FollowTransform followTransform;
    Rigidbody rb;

    XRBaseInteractor xrInteractor;

    public bool canBeGrabbed = true;
 
    protected override void Awake()
    {
        base.Awake();
        movilInteraction = GetComponent<MovilInteraction>();
        controMeshes = GetComponentInChildren<ControlMeshes>();
        followTransform = GetComponent<FollowTransform>();
        rb = GetComponent<Rigidbody>();

    }
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        if (!canBeGrabbed)
        {
            return;
        }

        canBeGrabbed = false;
        movilInteraction.MovilCall();
        controMeshes.SetOff();
        followTransform.Take();
        rb.isKinematic = true;

        xrInteractor = interactor;
        XRController xrController = interactor.GetComponent<XRController>();

        if (xrController == GameManager.instance.rightController)
        {
            ChangeHands.instance.ChangeRightHand(1);
            followTransform.right = true;
        }

        if (xrController == GameManager.instance.leftController)
        {
            ChangeHands.instance.ChangeLeftHand(1);
            followTransform.right = false;
        }

    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
    }

    public void MovilOff()
    {
        controMeshes.SetOn();

        ///CAN ATTACH??

        ChangeHands.instance.ChangeRightHand(0);
        ChangeHands.instance.ChangeLeftHand(0);
    }
}
