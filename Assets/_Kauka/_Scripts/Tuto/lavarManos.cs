using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class lavarManos : MonoBehaviour
{
    bool manoDer, manoIzq;
    Coroutine lavarseLasManosCor;

    public Slider sliderAgua;

    public scr_StepController stepCtrl;
    bool nextStepControl;

    public Transform posGrifoIdle;
    public XRSimpleInteractable pivotManivela;

    public GameObject soundCheck;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("playerRightHand"))
        {
            manoDer = true;

            if (lavarseLasManosCor != null)
            {
                lavarseLasManosCor = null;
                StopCoroutine(lavarseLasManos());
            }
            lavarseLasManosCor = StartCoroutine(lavarseLasManos());
        }
        if (other.gameObject.CompareTag("playerLeftHand"))
        {
            manoIzq = true;

            if (lavarseLasManosCor != null)
            {
                lavarseLasManosCor = null;
                StopCoroutine(lavarseLasManos());
            }
            lavarseLasManosCor = StartCoroutine(lavarseLasManos());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("playerRightHand"))
        {
            manoDer = false;

            if (lavarseLasManosCor != null)
            {
                lavarseLasManosCor = null;
                StopCoroutine(lavarseLasManos());
            }
        }
        if (other.gameObject.CompareTag("playerLeftHand"))
        {
            manoIzq = false;

            if (lavarseLasManosCor != null)
            {
                lavarseLasManosCor = null;
                StopCoroutine(lavarseLasManos());
            }
        }
    }

    IEnumerator lavarseLasManos()
    {
        while (true)
        {
            if (manoDer && manoIzq)
            {
                //Debug.Log("Me lavo las manos");
                sliderAgua.value += Time.deltaTime * 0.25f;

                if (sliderAgua.value >= 1)
                {
                    //Debug.Log("¡Manos Limpias!");
                    if (!nextStepControl)
                    {
                        stepCtrl.NextStep();
                        soundCheck.GetComponent<AudioSource>().Play();
                        nextStepControl = true;
                    }
                    pivotManivela.gameObject.transform.rotation = posGrifoIdle.rotation;
                    pivotManivela.enabled = false;
                    gameObject.SetActive(false);
                    break;
                }
            }
            else
            {
                //Debug.Log("falta la otra mano");
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
