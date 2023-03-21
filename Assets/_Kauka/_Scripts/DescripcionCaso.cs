using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DescripcionCaso : MonoBehaviour
{
    [Multiline]
    public string[] descriptionText;


    [SerializeField] TextMeshProUGUI textDescription;


    [SerializeField] GameObject[] buttonGO;

    [SerializeField] GameObject[] descriptionButtonsGO;

    private void Awake()
    {
        SetButtonSound();
        EnableButtons(true);
        EnableButtonsDescription(false);
    }
    void EnableButtons(bool value)
    {
        for (int i = 0; i < buttonGO.Length; i++)
        {
            buttonGO[i].SetActive(value);
        }
    }

    void EnableButtonsDescription(bool value)
    {
        for (int i = 0; i < descriptionButtonsGO.Length; i++)
        {
            descriptionButtonsGO[i].SetActive(value);
        }
    }

    public void SetDescription(int index)
    {
        EnableButtons(false);
        EnableButtonsDescription(true);
        SetCaso(index);
        textDescription.text = descriptionText[index];
    }

    public void BackMenu()
    {
        EnableButtons(true);
        EnableButtonsDescription(false);
        ResetCaso();
    }

    public void SetCaso(int value)
    {
        indexCaso = value;
    }
    public void ResetCaso()
    {
        indexCaso = 0;
    }

    public int indexCaso;

    public void SeleccionarCaso()
    {
        GameManager.instance.SelectModule(indexCaso);
    }

    public Button[] buttonArray;

    void RegisterSound()
    {
        GameManager.instance.audioManager.PlayPreshedUI();
        GameManager.instance.hapticController.SendHaptics(0.5f, 0.2f, HapticController.sendtoControllers.both);
    }

    void RegisterSoundSelected(PointerEventData data)
    {
        GameManager.instance.audioManager.PlaySelectedUI();
        GameManager.instance.hapticController.SendHaptics(0.1f, 0.2f, HapticController.sendtoControllers.both);
    }

    void SetButtonSound()
    {
        for (int i = 0; i < buttonArray.Length; i++)
        {
            EventTrigger trigger = buttonArray[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { RegisterSoundSelected((PointerEventData)data); });

            if (trigger != null)
            {
                trigger.triggers.Add(entry);
            }
       

            buttonArray[i].onClick.AddListener(RegisterSound);
        }
    }

}
