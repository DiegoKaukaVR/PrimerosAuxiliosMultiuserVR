using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPos_Grabables : MonoBehaviour
{
    public float tiempoParaReset = 1;
    public Transform origenPos;

    float segundos = 0;
    bool colisionoGround;
    Coroutine recogidaCor;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            colisionoGround = true;
            if (recogidaCor != null)
            {
                recogidaCor = null;
                StopCoroutine(startRecogidaObjeto());
            }
            recogidaCor = StartCoroutine(startRecogidaObjeto());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            colisionoGround = false;

            if (recogidaCor != null)
            {
                recogidaCor = null;
                StopCoroutine(startRecogidaObjeto());
            }

            Debug.Log("ya no colisiono");

        }
    }
    IEnumerator startRecogidaObjeto()
    {
        while (colisionoGround)
        {
            if (tiempoParaReset >= segundos)
            {
                segundos += Time.deltaTime;
                Debug.Log(segundos);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();

            if (tiempoParaReset <= segundos)
            {
                transform.SetPositionAndRotation(origenPos.position, origenPos.rotation);
            }
        }
        segundos = 0;
    }

    public scr_StepController stp_ctrl;
    bool step;
    public void colocarMascarilla()
    {
        if (!step) { stp_ctrl.NextStep(); step = true; gameObject.SetActive(false); }
    }
}
