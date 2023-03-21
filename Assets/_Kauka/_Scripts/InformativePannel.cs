using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;
public class InformativePannel : SimplePannel
{
    [SerializeField] bool hasButton = true;
    [SerializeField] GameObject buttonGO;

    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] int currentIndex;

    public TextInfo[] textInfoArray;

    protected void Start()
    {
        if (!hasButton)
        {
            buttonGO.SetActive(false);
        }
    }
    [Serializable]
    public class TextInfo
    {
        [Multiline]
        public string text;
        public Events textEvents;
        public bool hasButton = true;
    }

    [Serializable]
    public struct Events
    {
        public UnityEvent OnEnterText;
        public UnityEvent OnExitText;
    }


    public void NextText()
    {
        textInfoArray[currentIndex].textEvents.OnExitText.Invoke();
        currentIndex++;

        if (currentIndex>=textInfoArray.Length)
        {
            ClosePannel();
            return;
        }

        if (textInfoArray[currentIndex].hasButton)
        {
            buttonGO.SetActive(true);
        }
        else
        {
            buttonGO.SetActive(false);
        }

        _text.text = textInfoArray[currentIndex].text;
        textInfoArray[currentIndex].textEvents.OnEnterText.Invoke();
    }

    private void OnValidate()
    {
        if (textInfoArray.Length > 0 && _text != null)
        {
            _text.text = textInfoArray[currentIndex].text;
        }

        if (buttonGO != null)
        {
            if (hasButton)
            {
                buttonGO.SetActive(true);
            }
            else
            {
                buttonGO.SetActive(false);
            }
           
        }

    }
}
