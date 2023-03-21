using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class SlotsBandeja : MonoBehaviour
{
    public GameObject[] herramientas;
    public Transform[] huecosBandeja;
    public int numObjetosDefectuosos;

    public Transform mesaTrabajo;
    public Transform mesaTrabajo2;
    public Transform mesaClasificacion1;
    public Transform mesaClasificacion2;
    public Transform mesaClasificacion3;

    public GameObject bandeja;
    Vector3 posInicialBandeja;
    public GameObject papelera;
    public AnimationCurve curvaV;

    List<GameObject> herramientasColocadas = new List<GameObject>();
    List<GameObject> objetosDefectuosos = new List<GameObject>();
    List<Transform> huecosDisponibles = new List<Transform>();
    int huecosAux;
    public int herramientasAbiertasInspeccion;
    // Start is called before the first frame update
    void Start()
    {

        if (bandeja) { posInicialBandeja = bandeja.transform.position; }
        
        foreach (Transform tr in huecosBandeja)
        {
            huecosDisponibles.Add(tr);
        }
        
        huecosAux = huecosDisponibles.Count;    }

    public void ColocarHerramientas()
    {
        for (int i = 0; i < huecosAux; i++)
        {
            Transform posA = AsignarPosiciones();
            GameObject go = Instantiate(herramientas[i], posA);
            herramientasColocadas.Add(go);
            huecosDisponibles.Remove(posA);
        }
        if (GameControllerEsterilizacion.Instance.modulo1)
        {
            
            for (int j = 0; j < numObjetosDefectuosos; j++)
            {
                int numAleatorio = Random.Range(0, herramientasColocadas.Count - 1);
                if (objetosDefectuosos.Contains(herramientasColocadas[numAleatorio]))
                {
                    j--;
                }
                else
                {
                    objetosDefectuosos.Add(herramientasColocadas[numAleatorio]);
                    herramientasColocadas[numAleatorio].GetComponent<HerramientaBandeja>().objetoDefectuoso = true;
                }
            }
            foreach (GameObject go in herramientasColocadas)
            {
                if (go.GetComponent<HerramientaBandeja>().objetoDefectuoso)
                {
                    go.GetComponent<HerramientaBandeja>().ConvertirDefectuoso();
                }
                else
                {
                    go.GetComponent<HerramientaBandeja>().ObjetoCorrecto();
                }
                go.GetComponent<CustomMultiInteractable>().enabled = false;
                go.GetComponent<HerramientaBandeja>().bandejaCC = this;
            }
            foreach (GameObject go in herramientas)
            {
                go.GetComponent<XRGrabInteractable>().enabled = false;
                go.GetComponent<HerramientaBandeja>().puedeSegundoAgarre = true;
                go.GetComponent<HerramientaBandeja>().mesaTrabajo = mesaTrabajo;
                go.GetComponent<HerramientaBandeja>().mesaTrabajo2 = mesaTrabajo2;
            }
        }
        if (GameControllerEsterilizacion.Instance.modulo2)
        {
            
            foreach (GameObject go in herramientasColocadas)
            {
                go.GetComponent<HerramientaBandeja>().EliminarIndicador();
            }
            foreach (GameObject go in herramientas)
            {
                go.GetComponent<XRGrabInteractable>().enabled = true;
                go.GetComponent<HerramientaBandeja>().puedeSegundoAgarre = false;
                go.GetComponent<HerramientaBandeja>().mesaClasificacion1 = mesaClasificacion1;
                go.GetComponent<HerramientaBandeja>().mesaClasificacion2 = mesaClasificacion2;
                go.GetComponent<HerramientaBandeja>().mesaClasificacion3 = mesaClasificacion3;
            }
        }
    }

    Transform AsignarPosiciones()
    {
        int numActual = Random.Range(0, huecosDisponibles.Count - 1);
        return huecosDisponibles[numActual]; ;
    }

    public void Validar()
    {
        foreach (GameObject go in herramientasColocadas)
        {
            go.GetComponent<Collider>().enabled = true;
            go.GetComponent<Rigidbody>().isKinematic = false;
            
            go.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }
    public void PasarAMesaTrabajo(Transform posObj)
    {
        papelera.GetComponent<Collider>().enabled = true;
        bandeja.GetComponent<Collider>().enabled = true;
        StartCoroutine(PasaraMesaLerp(posObj.position));
    }

    IEnumerator PasaraMesaLerp(Vector3 posObj)
    {

        GameControllerEsterilizacion.Instance.materialAMesaTrabajo = true;
        GameControllerEsterilizacion.Instance.CheckConditions();
        
        float tiempo = 0;
        while (tiempo < 1)
        {
            tiempo += Time.deltaTime;
            bandeja.transform.position = Vector3.Lerp(posInicialBandeja, posObj, curvaV.Evaluate(tiempo));
            yield return null;
        }

        Validar();
    }

    public void SumarHerramientasInspeccionadas()
    {
        herramientasAbiertasInspeccion++;
        if(herramientasAbiertasInspeccion >= herramientas.Length)
        {
            GameControllerEsterilizacion.Instance.materialInteractuado = true;
        }
    }
}
