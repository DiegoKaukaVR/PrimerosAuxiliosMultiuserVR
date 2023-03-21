using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.UI;


public class AccumulativeVelocity : MonoBehaviour
{
    [Tooltip("Shake object")]
    [SerializeField] MyGrabInteractable interactuable;
    [SerializeField] ControllerVelocity controllerVelocity;

    float accumulativeVelocity;
    [SerializeField] float maxAccumulativeVelocity;

    float validDistance;

    float porcentaje;
    [SerializeField] Slider slider;


    public UnityEvent onComplete;
    bool completed;

   
    Controller currentController;
    enum Controller
    {
        left,
        right
    }

    private void Start()
    {
        controllerVelocity = GameManager.instance.controllerVelocity;
    }

    float CalculatePorcentage(float accumulativeVelocity, float maxAccumulativeVelocity)
    {
        return (accumulativeVelocity / maxAccumulativeVelocity) * 100;
    }

    void SetSlider()
    {
        slider.value = CalculatePorcentage(accumulativeVelocity, maxAccumulativeVelocity);
    }
  

    public void StartTracking()
    {
        if (interactuable.firstInteractorSelecting == GameManager.instance.leftController)
        {
            currentController = Controller.left;
        }
        else
        {
            currentController = Controller.right;
        }

        StartCoroutine(CoroutineTracking());
    }

    public void StopTracking()
    {
        StopCoroutine(CoroutineTracking());
    }

    IEnumerator CoroutineTracking()
    {
        while (!completed)
        {
            SetSlider();
            if (Vector3.Distance(interactuable.transform.position, transform.position) > validDistance)
            {
                StopTracking();
                yield return null;
            }

            if (accumulativeVelocity > maxAccumulativeVelocity)
            {
                Completed();
            }
            if (currentController == Controller.left)
            {
                accumulativeVelocity += controllerVelocity.GetLeftHandVelocity().magnitude;
            }
            else
            {
                accumulativeVelocity += controllerVelocity.GetRightHandVelocity().magnitude;
            }
        
            yield return null;
        }

        yield return 0;
    }

    void Completed()
    {
        completed = true;
        accumulativeVelocity = 0;
        onComplete.Invoke();
    }
    
}
