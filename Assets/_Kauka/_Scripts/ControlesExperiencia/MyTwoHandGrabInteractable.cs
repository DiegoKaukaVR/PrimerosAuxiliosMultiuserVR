using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MyTwoHandGrabInteractable : MyGrabInteractable
{
    //Se lleva mal con el socket zone (Snap) el twoHandGrab

    public List<XRSimpleInteractable> secondGrabPoints = new List<XRSimpleInteractable>();

    [SerializeField] protected XRBaseInteractor secondInteractor;
    protected Quaternion attachInitialRotiation;
    public enum TwoHandRotationType
    {
        None,
        First,
        Second
    }
    public TwoHandRotationType twoHandRotationType;
    public bool snapToSecondHand = true;
    protected Quaternion firstRotationOffset;

    [SerializeField] bool moveOnlyWith2Hands;
    bool firstHand;
    bool secondHand;

    protected void Start()
    {
        foreach (var item in secondGrabPoints)
        {
            item.onSelectEntered.AddListener(OnSecondHandGrab);
            item.onSelectExited.AddListener(OnSecondHandRelease);
        }
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(secondInteractor && selectingInteractor)
        {
            //Rotacion
            if (snapToSecondHand)
            {
                selectingInteractor.attachTransform.rotation = GetTwoHandedRotation();
            }
            else
            {
                selectingInteractor.attachTransform.rotation = GetTwoHandedRotation() * firstRotationOffset;
            }
        }

        base.ProcessInteractable(updatePhase);
    }

    protected Quaternion GetTwoHandedRotation()
    {
       
        Quaternion targetRotation;

        if (selectingInteractor == null || secondInteractor == null)
        {
            return Quaternion.identity;
        }
       
        if (twoHandRotationType == TwoHandRotationType.None)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
        }
        else if(twoHandRotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.transform.up);
        }
        return targetRotation;
    }
    public virtual void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        Debug.Log("Second Hand Enter");

        secondHand = true;

        if (moveOnlyWith2Hands)
        {
            TrackPositionAndRotation();
        }
       
       
        secondInteractor = interactor;
        if (selectingInteractor)
        {
            firstRotationOffset = Quaternion.Inverse(GetTwoHandedRotation()) * selectingInteractor.attachTransform.rotation;
        }
       
    }
    public virtual void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        Debug.Log("Second Hand Exit");
        secondHand = false;
        secondInteractor = null;
    }
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        //Debug.Log("First Hand Enter");
        base.OnSelectEntered(interactor);
        //trackRotation = true;
        firstHand = true;

        if (moveOnlyWith2Hands)
        {
            TrackPositionAndRotation();
        }
        attachInitialRotiation = interactor.attachTransform.localRotation;
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        Debug.Log("First Hand Exit");
        base.OnSelectExited(interactor);
        firstHand = false;
        secondInteractor = null;
        interactor.attachTransform.localRotation = attachInitialRotiation;
    }

    void TrackPositionAndRotation()
    {
        //if (CheckBothHands())
        //{
        //    trackPosition = true;
            
        //}
    }

    bool CheckBothHands()
    {
        if (firstHand && secondHand)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isGrabbed;
    }
}
