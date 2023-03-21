using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class XRInteractionFeedback : MonoBehaviour
{
    public XRSimpleInteractable[] simpleInteractableArray;

    public bool preshedSound = true;

    private void Start()
    {
        SetButtonSound();
    }
    void RegisterSound(SelectExitEventArgs args)
    {
        if (preshedSound)
        {
            GameManager.instance.audioManager.PlayPreshedUI();
        }

        GameManager.instance.hapticController.SendHaptics(0.5f, 0.2f, HapticController.sendtoControllers.both);
    }

    void RegisterSoundSelected(HoverEnterEventArgs args)
    {
        GameManager.instance.audioManager.PlaySelectedUI();
        GameManager.instance.hapticController.SendHaptics(0.1f, 0.2f, HapticController.sendtoControllers.both);
    }

    void SetButtonSound()
    {
        for (int i = 0; i < simpleInteractableArray.Length; i++)
        {
            simpleInteractableArray[i].hoverEntered.AddListener(RegisterSoundSelected);
            simpleInteractableArray[i].selectExited.AddListener(RegisterSound);
        }


    }
}
