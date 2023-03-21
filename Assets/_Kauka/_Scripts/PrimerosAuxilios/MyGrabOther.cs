using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MyGrabOther : MyGrabInteractable
{
    public MyGrabWith2HandsCondition mainGrab;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        mainGrab.otherGrabbed = true;
        base.OnSelectEntered(args);

    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        mainGrab.otherGrabbed = false;
        base.OnSelectExited(args);

    }
}
