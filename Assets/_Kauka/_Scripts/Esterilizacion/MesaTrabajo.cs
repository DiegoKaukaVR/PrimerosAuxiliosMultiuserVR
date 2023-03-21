using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class MesaTrabajo : MonoBehaviour
{
    public Transform[] huecosMesa;
    int huecoSiguiente = 0;
    List<GameObject> herramientasFinales = new List<GameObject>();
    public Transform posicionObj;
    public float tiempoAPosicion;
    public float offsetColocar;
    public GameObject slots;
    GameObject objetoFeedback;
    public bool huecosDisponibles;
    public bool objetoErroneo;
    bool herramientaSucia;
    bool herramientaSinSecar;
    bool herramientaSinEnjuagar;
    public int indiceMesa;
    
    public void AsignarHueco(GameObject obj)
    {
        if (objetoFeedback) { Destroy(objetoFeedback); }
        //obj.GetComponent<Rigidbody>().detectCollisions = false;
        obj.GetComponent<Rigidbody>().isKinematic = true;

        if(!obj.GetComponent<HerramientaBandeja>().objetoDefectuoso || GameControllerEsterilizacion.Instance.modoFormativo)
        {
            obj.GetComponent<XRGrabInteractable>().enabled = false;
        }

        obj.transform.rotation = huecosMesa[huecoSiguiente].rotation;

        StartCoroutine(IrAPosicion(obj));
        herramientasFinales.Add(obj);
        if (GameControllerEsterilizacion.Instance.modulo1)
        {
            if (obj.GetComponent<HerramientaBandeja>().objetoDefectuoso)
            {
                objetoErroneo = true;
            }
            if (huecoSiguiente + 1 >= huecosMesa.Length)
            {
                huecosDisponibles = false;
                if (!objetoErroneo)
                {
                    GameControllerEsterilizacion.Instance.materialInspeccionado = true;
                    GameControllerEsterilizacion.Instance.CheckConditions();
                }

                foreach (GameObject herramienta in herramientasFinales)
                {
                    if (!herramienta.GetComponent<HerramientaBandeja>().piezaSecada && GameControllerEsterilizacion.Instance.modoFormativo) { herramientaSucia = true; }
                    if (!herramienta.GetComponent<HerramientaBandeja>().piezaLimpia && GameControllerEsterilizacion.Instance.modoEvaluativo) { herramientaSucia = true; }
                    if (!herramienta.GetComponent<HerramientaBandeja>().piezaEnjuagada && GameControllerEsterilizacion.Instance.modoEvaluativo) { herramientaSinEnjuagar = true; }
                    if (!herramienta.GetComponent<HerramientaBandeja>().piezaSecada && GameControllerEsterilizacion.Instance.modoEvaluativo) { herramientaSinSecar = true; }
                }
                if (!herramientaSucia && GameControllerEsterilizacion.Instance.modoFormativo)
                {
                    GameControllerEsterilizacion.Instance.materialSecado = true;
                    GameControllerEsterilizacion.Instance.materialAMesaTrabajo2 = true;
                    GameControllerEsterilizacion.Instance.CheckConditions();
                }
                if (GameControllerEsterilizacion.Instance.modoEvaluativo)
                {
                    GameControllerEsterilizacion.Instance.materialAMesaTrabajo2 = true;
                    if (herramientaSucia)
                    {
                        GameControllerEsterilizacion.Instance.materialLimpio = false;
                    }
                    if (herramientaSinEnjuagar)
                    {
                        GameControllerEsterilizacion.Instance.materialEnjuagado = false;
                    }
                    if (herramientaSinSecar)
                    {
                        GameControllerEsterilizacion.Instance.materialSecado = false;
                    }
                    GameControllerEsterilizacion.Instance.CheckConditions();
                }
            }
        }

        if (GameControllerEsterilizacion.Instance.modulo2)
        {
            if(obj.GetComponent<HerramientaBandeja>().indiceClasificacion != indiceMesa)
            {
                obj.GetComponent<HerramientaBandeja>().objetoCorrectoColocado = false;
            }
            else
            {
                obj.GetComponent<HerramientaBandeja>().objetoCorrectoColocado = true;
            }
            obj.GetComponent<HerramientaBandeja>().objetoColocado = true;
        }
        

    }
    public IEnumerator IrAPosicion(GameObject obj)
    {

        float tiempo = 0;
        while(tiempo < tiempoAPosicion)
        {
            tiempo += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(huecosMesa[huecoSiguiente].position + new Vector3(0,offsetColocar,0), huecosMesa[huecoSiguiente].position, tiempo/tiempoAPosicion);
            yield return null;
        }
        huecoSiguiente++;
    }

    public void PasarHerramientasAlFregadero()
    {
        if (huecoSiguiente + 1 < huecosMesa.Length)
        {
            GameControllerEsterilizacion.Instance.materialInspeccionado = false;
            GameControllerEsterilizacion.Instance.CheckConditions();
        }
        StartCoroutine(ColocarHerramientasFregadero());

    }

    IEnumerator ColocarHerramientasFregadero()
    {
        foreach(GameObject obj in herramientasFinales)
        {
            obj.transform.parent = null;
            obj.transform.position = posicionObj.position;
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<Rigidbody>().useGravity = true;
            obj.GetComponent<XRGrabInteractable>().enabled = true;
            Destroy(obj.GetComponent<HerramientaBandeja>().indicadorEstado);
            obj.GetComponent<HerramientaBandeja>().puedeSegundoAgarre = false;
            
            obj.GetComponent<HerramientaBandeja>().puedeLimpiar = true;
            yield return new WaitForSeconds(.35f);
        }
        GameControllerEsterilizacion.Instance.materialEnFregadero = true;
        GameControllerEsterilizacion.Instance.CheckConditions();
    }

    public void PasarHerramientasATalla()
    {
        foreach(HerramientaBandeja hb in FindObjectsOfType<HerramientaBandeja>())
        {
            if (!hb.objetoCorrectoColocado)
            {
                objetoErroneo = true;
            }
        }
        if (!objetoErroneo)
        {
            GameControllerEsterilizacion.Instance.materialClasificado = true;
        }
        foreach (GameObject obj in herramientasFinales)
        {
            obj.transform.position = posicionObj.position;
            obj.GetComponent<XRGrabInteractable>().enabled = true;
            obj.GetComponent<HerramientaBandeja>().mesaActual = 2;
        }
        GameControllerEsterilizacion.Instance.materialEnTalla = true;
        GameControllerEsterilizacion.Instance.CheckConditions();
    }
    public void ActivarFeedBack(GameObject feedbackPrefab)
    {
        objetoFeedback = Instantiate(feedbackPrefab, huecosMesa[huecoSiguiente].position, huecosMesa[huecoSiguiente].rotation);

    }
    public void DestroyFeedback(HerramientaBandeja hb)
    {
        Destroy(objetoFeedback);
        hb.feedbackBool = false;

    }
    //public void MaterialClasificado()
    //{
    //    GameControllerEsterilizacion.Instance.materialSecado = true;
    //    GameControllerEsterilizacion.Instance.materialAMesaTrabajo2 = true;
    //    GameControllerEsterilizacion.Instance.materialClasificado = true;
    //    GameControllerEsterilizacion.Instance.CheckConditions();
    //}

}
