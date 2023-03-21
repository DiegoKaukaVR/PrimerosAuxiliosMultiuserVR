using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof (GrabID))]
public class MyGrabInteractable : XRGrabInteractable
{

    protected GrabID grabID;

    protected override void Awake()
    {
        base.Awake();
        grabID = GetComponent<GrabID>();
    }

    /// <summary>
    /// Comprueba si el objeto es seleccionable y no ha sido cogido
    /// </summary>
    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isGrabbed;
    }
}
