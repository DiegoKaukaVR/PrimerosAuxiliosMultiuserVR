using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class OnInputEvent : MonoBehaviour
{

    XRController leftController;
    XRController rightController;

    [SerializeField] input inputEvent;


    public UnityEvent onInputPresh;
    public UnityEvent onInputRelease;

    public enum input
    {
        primaryBotton,
        secondaryBotton,
        trigger,
        grip,
    }

    private void Start()
    {
        leftController = GameManager.instance.leftController;
        rightController = GameManager.instance.rightController;
    }


    void Update()
    {
        InputDetection(inputEvent);
    }

    void InputDetection(input input)
    {
        switch (input)
        {
            case input.primaryBotton:

                if (rightController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryRight))
                {
                    if (primaryRight)
                    {
                        onInputPresh.Invoke();
                        Debug.Log("Primary botton A preshed");
                        this.enabled = false;
                    }
                }
                break;
            case input.secondaryBotton:
                break;
            case input.trigger:
                break;
            case input.grip:
                break;
            default:
                break;
        }
    }


}
