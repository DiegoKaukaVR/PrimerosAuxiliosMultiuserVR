using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MyGrabWith2HandsCondition : MyGrabInteractable
{
    public bool otherGrabbed;



    public void OtherGrab(bool value)
    {
        otherGrabbed = value;
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!otherGrabbed)
        {
            return;
        }
        base.OnSelectEntered(args);
   
    }



}
