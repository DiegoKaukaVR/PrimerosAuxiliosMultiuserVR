using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class CheckInput : MonoBehaviour
{
    XRController leftController;
    XRController rightController;

    public UnityEvent onSuccess;

    private void Start()
    {
        leftController = GameManager.instance.leftController;
        rightController = GameManager.instance.rightController;
    }

    public InputType inputType;
    public enum InputType
    {
        triggerBotton,
        secondaryBotton,
        menuBotton

    }

    void Update()
    {
        InputCheck();
    }

    public void InputCheck()
    {
        bool value = false;
        switch (inputType)
        {
            case InputType.triggerBotton:
                if (InputCheckPrimary())
                {
                    value = true;
                }
                
                break;

            case InputType.secondaryBotton:

                if (InputCheckSecondary())
                {
                    value = true;
                }
                break;

            case InputType.menuBotton:

                if (InputCheckMenu())
                {
                    value = true;
                }
                break;


            default:
                break;
        }

        if (value)
        {
            onSuccess.Invoke();
        }
    }

  
    public bool InputCheckPrimary()
    {
        if (rightController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool primaryRight) && leftController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool primaryLeft))
        {
            if (primaryRight || primaryLeft)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }

    }

    public bool InputCheckSecondary()
    {
        if (rightController.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryBotton) && leftController.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryLeft))
        {
            if (secondaryBotton || secondaryLeft)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }

    }

    bool avaible = true;

    
    public bool InputCheckMenu()
    {
        if (leftController.inputDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuLeft))
        {
            if (menuLeft)
            {
                if (!avaible)
                {
                    return false;
                }
                else
                {
                    avaible = false;
                }
                return true;
            }
            else
            {
                avaible = true;
                return false;
            }

        }
        else
        {
            avaible = true;
            return false;
        }

    }
}
