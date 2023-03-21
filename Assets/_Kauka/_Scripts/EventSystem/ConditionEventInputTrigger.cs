using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class ConditionEventInputTrigger : ConditionEvent
{
    XRController leftController;
    XRController rightController;

    private void Start()
    {
        leftController = GameManager.instance.leftController;
        rightController = GameManager.instance.rightController;
    }

    /// <summary>
    /// Cuando el botón principal del mando izquierdo es pulsado el trigger ejecutará el evento
    /// </summary>
    /// <returns></returns>
    public override bool Condition(GameObject go)
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
}
