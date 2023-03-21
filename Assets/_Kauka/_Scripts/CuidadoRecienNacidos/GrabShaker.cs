using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;


// 1. Filtrar si el objeto está siendo cogido por la mano izquierda o derecha
// 2. Suma acumulativa de la velocidad del objeto 
// 3. Comprobar si ha alcanzado un límite 
public class GrabShaker : MyGrabInteractable
{
    ControllerVelocity controllerVelocity;

    float accumulativeVelocity;
    float maxAccumulativeVelocity = 100f;

    Controller currentController;

    public UnityEvent onCompleted;
    enum Controller
    {
        left, 
        right,
        none
    }

    private void Start()
    {
        controllerVelocity = GameManager.instance.controllerVelocity;
    }

    
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (interactor == GameManager.instance.leftController)
        {
            currentController = Controller.left;
        }
        if (interactor == GameManager.instance.rightController)
        {
            currentController = Controller.right;
        }
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        currentController = Controller.none;
    }
  


    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        Debug.Log("Process");

        if (currentController == Controller.none)
        {
            return;
        }

        AccumulativeVelocity();

        if (accumulativeVelocity > maxAccumulativeVelocity)
        {
            Debug.Log("Finished");
            onCompleted.Invoke();
        }
    }

    private void AccumulativeVelocity()
    {
        if (currentController == Controller.left)
        {
            accumulativeVelocity = controllerVelocity.GetLeftHandVelocity().magnitude;
        }
        if (currentController == Controller.right)
        {
            accumulativeVelocity = controllerVelocity.GetRightHandVelocity().magnitude;
        }

        Debug.Log("AccumulativeVelocity: " + accumulativeVelocity);
    }
}
