using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrab : XRGrabInteractable
{
    private Vector3 interactorPosition = Vector3.zero;
    private Quaternion interactorRotation = Quaternion.identity;
    protected override void OnSelectEntering(XRBaseInteractor interactor) 
    {
        base.OnSelectEntering(interactor); 
        StoreInteractor(interactor); 
        MatchAttachmentPoints(interactor);
    }
    private void StoreInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = interactor.attachTransform.position;
        interactorRotation = interactor.attachTransform.rotation;
    }
    private void MatchAttachmentPoints (XRBaseInteractor interactor)
    {
        bool hasAttach = attachTransform != null;
        interactor.attachTransform.position = hasAttach ? attachTransform.position : transform.position;
        interactor.attachTransform.rotation = hasAttach ? attachTransform.rotation : transform.rotation;
    }
    protected override void OnSelectExiting(XRBaseInteractor interactor)
    {
        base.OnSelectExiting(interactor);
        resetAttachmentPoint(interactor);
        //ClearInteractor(interactor);
    }
    void resetAttachmentPoint (XRBaseInteractor interactor)
    {
        //interactor.attachTransform.position = interactorPosition;
        interactor.attachTransform.rotation = interactorRotation;
    }
    void ClearInteractor (XRBaseInteractor interactor)
    {
        interactorPosition = Vector3.zero;
        interactorRotation = Quaternion.identity;
    }
}
