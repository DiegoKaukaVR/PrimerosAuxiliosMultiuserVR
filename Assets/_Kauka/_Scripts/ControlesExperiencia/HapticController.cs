using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Configurado para que funcione con la mano derecha
/// </summary>
public class HapticController : MonoBehaviour
{
    public XRBaseController leftController, rightController;

    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 0.5f;

    public enum sendtoControllers
    {
        left,
        right,
        both
    }
    public void SendHaptics(float amplitude, float duration)
    {
        rightController.SendHapticImpulse(amplitude, duration);
    }

    public void SendHaptics(float amplitude, float duration, sendtoControllers sendDevices)
    {
        switch (sendDevices)
        {
            case sendtoControllers.left:
                leftController.SendHapticImpulse(amplitude, duration);
                break;
            case sendtoControllers.right:
                rightController.SendHapticImpulse(amplitude, duration);
                break;
            case sendtoControllers.both:
                leftController.SendHapticImpulse(amplitude, duration);
                rightController.SendHapticImpulse(amplitude, duration);
                break;
            default:
                break;
        }
      
    }

    public void SendTotalHaptics()
    {
        rightController.SendHapticImpulse(defaultAmplitude*2, defaultDuration);
        leftController.SendHapticImpulse(defaultAmplitude*2, defaultDuration);
    }
    public void SendLowHaptics()
    {
        rightController.SendHapticImpulse(defaultAmplitude/2, defaultDuration/2);
    }
   
    public void SendHaptics()
    {
        rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
    }


}
