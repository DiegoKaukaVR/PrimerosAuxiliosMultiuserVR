using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using UnityEngine.XR.Interaction.Toolkit;
public class BodyInventory : XRSocketInteractor
{
    [SerializeField] LayerMask handsLayermask;
    XRGrabInteractable interactable;
    bool recoger;



    //protected override void OnSelectEntered(SelectEnterEventArgs args)
    //{
    //    base.OnSelectEntered(args);
        
    //    Debug.Log("On Select Entered _BodyInventory");


    //}
    //protected void OnTriggerEnter(Collider other)
    //{
    //    if (DetectLayer.LayerInLayerMask(other.gameObject.layer, handsLayermask))
    //    {
           
    //    }
    //}





}
