using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwinScale : MonoBehaviour
{
    TextMeshProUGUI text;

    [SerializeField] float currentFontSide;
    [SerializeField] float initialFontSide;
    [SerializeField] float maxInitialFontSide;

    [SerializeField] float speed = 5f;

  
    void Start()
    {
        Initialize();
        initialFontSide = text.fontSize;
        currentFontSide = initialFontSide;

        StartCoroutine(LerpScaleUp());
    }

    void Initialize()
    {
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
    }

    public void StopTwinScale()
    {
        StopCoroutine(LerpScaleUp());
        StopCoroutine(LerpScaleDown());
    }

    IEnumerator LerpScaleUp()
    {
        while (currentFontSide < maxInitialFontSide)
        {
            text.fontSize += Time.deltaTime * speed;
            currentFontSide += Time.deltaTime * speed;

            yield return null;
        }
        Debug.Log("SUCESS LERP");
        StartCoroutine(LerpScaleDown());
    }

    IEnumerator LerpScaleDown()
    {
        while (currentFontSide > initialFontSide)
        {
            text.fontSize -= Time.deltaTime * speed;
            currentFontSide -= Time.deltaTime * speed;

            yield return null;
        }
        StartCoroutine(LerpScaleUp());
    }


}
