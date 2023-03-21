using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEditor : MonoBehaviour
{
    TextMeshProUGUI _text;
    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Change actual txt of the component
    /// </summary>
   
    public void ChangeText(string newText)
    {
        _text.text = newText;
    }
}
