using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class IndicePiezaColocada : MonoBehaviour
{
    [Range(0, 10)]
    public int indicePieza;

    public GameObject piezaObjetivo;
    public GameObject[] objetosAdicionales;

    public void SoltarPieza()
    {
        StartCoroutine(ComprobarPosicionPieza());
    }
    public IEnumerator ComprobarPosicionPieza()
    {
        //yield return new WaitForSeconds(.5f);
        if (piezaObjetivo.GetComponent<ImanObjeto>().volviendoAPosicion)
        {
            piezaObjetivo.GetComponent<XRGrabInteractable>().enabled = false;

            if (indicePieza == 0 && GameControllerOdontologia.Instance.stepActual < 8)
            {
                GameObject piezaSecundaria = piezaObjetivo.GetComponentInChildren<XRSimpleInteractable>().gameObject;
                piezaSecundaria.GetComponent<XRSimpleInteractable>().enabled = false;
                piezaSecundaria.GetComponent<XRGrabInteractable>().enabled = true;
                piezaObjetivo.GetComponent<ImanObjeto>().posIman = piezaObjetivo.GetComponent<ImanObjeto>().transformInicial;
                //XRGrabInteractable grabSecundario = piezaSecundaria.AddComponent<XRGrabInteractable>();
                //grabSecundario.colliders[0] = piezaSecundaria.GetComponent<Collider>();
                //grabSecundario.selectExited(piezaSecundaria.GetComponent<ImanObjeto>().CheckPosition());

            }
            else if(indicePieza == 0 && GameControllerOdontologia.Instance.stepActual > 8)
            {
                GameControllerOdontologia.Instance.servilletaDentalQuitada = true;
                GameControllerOdontologia.Instance.CheckConditions();
            }

            if (indicePieza == 1)
            {
                GameControllerOdontologia.Instance.bandejaEnSillon = true;
                GameControllerOdontologia.Instance.CheckConditions();
            }
            if (indicePieza == 2)
            {
                //foreach(GameObject go in objetosAdicionales)
                //{
                //    go.GetComponent<Collider>().enabled = true;
                //    go.GetComponent<XRGrabInteractable>().enabled = true;
                //}
                piezaObjetivo.GetComponent<MeshRenderer>().enabled = false;
                piezaObjetivo.transform.parent = piezaObjetivo.GetComponent<ImanObjeto>().posIman;
                GetComponent<BolsaHerramientas>().ColocarHerramientas();
                piezaObjetivo.GetComponent<XRGrabInteractable>().enabled = false;
                GameControllerOdontologia.Instance.instrumentalEnBandeja = true;
                GameControllerOdontologia.Instance.CheckConditions();
            }
            if (indicePieza == 3 && GameControllerOdontologia.Instance.stepActual < 8)
            {
                GameControllerOdontologia.Instance.servilletaDentalColocada = true;
                GameControllerOdontologia.Instance.CheckConditions();
                print("k paso");
            }
            if(indicePieza == 4)
            {
                foreach (GameObject go in objetosAdicionales)
                {
                    go.GetComponent<Collider>().enabled = true;
                    go.GetComponent<XRGrabInteractable>().enabled = true;
                }
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
