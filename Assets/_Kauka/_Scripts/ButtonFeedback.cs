using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonFeedback : MonoBehaviour
{
    public Button[] buttonArray;

    public bool preshedSound = true;

    private void Start()
    {
        SetButtonSound();
    }
    void RegisterSound()
    {
        if (preshedSound)
        {
            GameManager.instance.audioManager.PlayPreshedUI();
        }
   
        GameManager.instance.hapticController.SendHaptics(0.5f,0.2f, HapticController.sendtoControllers.both);
    }

    void RegisterSoundSelected(PointerEventData data)
    {
        GameManager.instance.audioManager.PlaySelectedUI();
        GameManager.instance.hapticController.SendHaptics(0.25f, 0.2f, HapticController.sendtoControllers.both);
    }

    void SetButtonSound()
    {
        for (int i = 0; i < buttonArray.Length; i++)
        {
            EventTrigger trigger = buttonArray[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { RegisterSoundSelected((PointerEventData)data); });
            trigger.triggers.Add(entry);

            buttonArray[i].onClick.AddListener(RegisterSound);
        }


    }
}
