using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

[CanSelectMultiple(true)]
public class CustomMultiInteractable : XRGrabInteractable
{
    public bool oneHand;
    public bool twoHand;
    public Transform[] attachPoints;
    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractor interactor = selectingInteractor;

        IXRSelectInteractor newInteractor = firstInteractorSelecting;

        List<IXRSelectInteractor> morInteractors = interactorsSelecting;
    }

    
  
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        //transform.rotation = new Quaternion(0, Camera.main.transform.eulerAngles.y, 0,1);
        if (HasMultipleInteractors())
        {
            twoHand = true;
            oneHand = false;
        }
        else
        {
            twoHand = false;
            oneHand = true;
            if (RightHand())
            {
                attachTransform = attachPoints[0];
            }
            else
            {
                attachTransform = attachPoints[1];
            }

            //trackRotation = true;
        }

        if (GetComponent<ValidarModulo1>())
        {
            GetComponent<ValidarModulo1>().FuncionObjetivo();
        }
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        twoHand = false;
        if (HasNoInteractors())
        {
            oneHand = false;
        }
        else
        {
            if (RightHand())
            {
                attachTransform = attachPoints[0];
            }
            else
            {
                attachTransform = attachPoints[1];
            }
        }
        if (GetComponent<ValidarModulo1>())
        {
            GetComponent<ValidarModulo1>().FuncionObjetivo();
        }
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
}
