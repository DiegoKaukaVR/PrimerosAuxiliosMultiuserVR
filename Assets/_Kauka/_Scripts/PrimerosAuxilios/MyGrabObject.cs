using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;


/// <summary>
/// Objecto que se añade a una collección si está lo suficientemente cerca de alguna posición asignada (partes del cuerpo del maniqui)
/// </summary>
public class MyGrabObject : MyGrabInteractable
{
    public GrabCollection grabCollection;

    public int id;


    public bool grabActivated;

    public bool bigSlot;
    private void Start()
    {
       grabCollection = GetComponentInParent<GrabCollection>();
       trackPosition = false; trackRotation = false;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (!grabActivated){ trackPosition = false; trackRotation = false; return;}
        else
        {
            trackPosition = true; trackRotation = true;
        }

        OnEnterSelected();
    }

    void OnEnterSelected()
    {
       
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        
        base.OnSelectExited(interactor);
        OnExitSelected();
    }
    

    void OnExitSelected()
    {
        for (int i = 0; i < grabCollection.transformsCollection.Length; i++)
        {
            if (Vector3.Distance(transform.position, grabCollection.transformsCollection[i].position) < 0.1f)
            {
                if (id != i)
                {
                    return;
                }
                grabCollection.AddObjectToCollection(this, i);
                return;
            }
        }
    }

}
