using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class XRGrabMovil : MyGrabInteractable
{
    public bool canBeGrabbed;


    XRBaseInteractor xrInteractor;
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (!canBeGrabbed)
        {
            return;
        }

        xrInteractor = interactor;
        XRController xrController = interactor.GetComponent<XRController>();

        if (xrController == GameManager.instance.rightController)
        {
            ChangeHands.instance.ChangeRightHand(1);
        }

        if (xrController == GameManager.instance.leftController)
        {
            ChangeHands.instance.ChangeLeftHand(1);
        }

        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        ChangeHands.instance.ChangeRightHand(0);
        ChangeHands.instance.ChangeLeftHand(0);

    }
    public void CanBeGrabbed()
    {
        canBeGrabbed = true;
    }

}
