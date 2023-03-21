using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : Singleton<Fader>
{
    [SerializeField]
    public float tiempoFadeNormal = 2.0f;

    [SerializeField]
    Image fadeImg;

    Coroutine corFadeOut;
    Coroutine corFadeIn;

    public void FadeOut()
    {
        if(corFadeOut != null)
        {
            StopCoroutine(corFadeOut);
            corFadeOut = null;
        }

        corFadeOut = StartCoroutine(FadeOutNormal());
    }
    public void FadeIn()
    {
        if (corFadeIn != null)
        {
            StopCoroutine(corFadeIn);
            corFadeIn = null;
        }

        corFadeIn = StartCoroutine(FadeInNormal());
    }
    IEnumerator FadeOutNormal()
    {
        float tiempo = 0;

        Color color = new Color(0, 0, 0, 1);
        Color newColor = new Color(0, 0, 0, 0);
        
        while(tiempo <= tiempoFadeNormal)
        {
            fadeImg.color = Color.Lerp(color, newColor, tiempo/tiempoFadeNormal);
            //actualizar tiempo
            tiempo += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator FadeInNormal()
    {
        float tiempo = 0;

        Color color = new Color(0, 0, 0, 0);
        Color newColor = new Color(0, 0, 0, 1);

        while (tiempo <= tiempoFadeNormal)
        {
            fadeImg.color = Color.Lerp(color, newColor, tiempo / tiempoFadeNormal);
            //actualizar tiempo
            tiempo += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        fadeImg.color = newColor;


    }
}
