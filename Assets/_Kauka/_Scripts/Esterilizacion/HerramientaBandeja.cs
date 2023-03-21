using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class HerramientaBandeja : MonoBehaviour
{
    public GameObject indicadorEstado;
    public int indiceClasificacion;
    public bool objetoDefectuoso;
    public GameObject segundoAgarre;
    bool seleccionado;
    bool interaccionGeneral;
    bool puedeSumarInteraccion;
    Transform parentTransform;
    public GameObject[] partesSucias;
    public GameObject contenedorCepilloTubular;

    public Transform mesaTrabajo;
    public Transform mesaTrabajo2;
    public Transform mesaClasificacion1;
    public Transform mesaClasificacion2;
    public Transform mesaClasificacion3;

    public float distanciaALaMesa = 1;
    public Transform posicionCepilloTubular;
    int partesSuciasRestantes;
    int partesEnjuagadasRestantes;
    int partesSecadasRestantes;
    public bool piezaLimpia;
    public bool piezaEnjuagada;
    public bool piezaSecada;
    public bool puedeSegundoAgarre;
    public bool puedeLimpiar;
    public bool herramientaColocada;
    public GameObject feedbackPrefab;
    public bool feedbackBool;
    public int mesaActual = 1;

    public SlotsBandeja bandejaCC;


    public bool objetoCorrectoColocado;
    public bool objetoColocado;
    private void Start()
    {
        mesaActual = 1;
        segundoAgarre.GetComponent<Collider>().enabled = false;
        partesSuciasRestantes = partesSucias.Length;
        partesEnjuagadasRestantes = partesSuciasRestantes;
        partesSecadasRestantes = partesSuciasRestantes;
        puedeSumarInteraccion = true;
        if(transform.parent!= null)
        {
            parentTransform = transform.parent;
        }
        foreach(GameObject go in partesSucias)
        {
            go.GetComponent<Collider>().isTrigger = true;
        }
    }
    private void Update()
    {
        if (seleccionado)
        {
            InteractuarHerramienta();
        }
        if (interaccionGeneral)
        {
            InteraccionGeneral();
        }
    }
    public void EliminarIndicador()
    {
        Destroy(indicadorEstado);
    }

    public void ConvertirDefectuoso()
    {
        indicadorEstado.GetComponent<MeshRenderer>().materials[0].color = Color.red;
        indicadorEstado.SetActive(false);
    }
    public void ObjetoCorrecto()
    {
        indicadorEstado.GetComponent<MeshRenderer>().materials[0].color = Color.green;
        indicadorEstado.SetActive(false);
    }
    public void InteractuarHerramienta()
    {
        if (puedeSegundoAgarre)
        {
            seleccionado = true;
            indicadorEstado.SetActive(true);
            segundoAgarre.transform.SetParent(this.transform);
            if (puedeSumarInteraccion)
            {
                bandejaCC.SumarHerramientasInspeccionadas();
                puedeSumarInteraccion = false;
            }
        }
    }
    public void SoltarObjeto()
    {
        seleccionado = false;
        puedeSumarInteraccion = true;
        if (indicadorEstado) { indicadorEstado.SetActive(false); }

        if(mesaTrabajo && Vector3.Distance(transform.position,mesaTrabajo.position) < distanciaALaMesa && mesaTrabajo.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            if((GameControllerEsterilizacion.Instance.modoFormativo && !objetoDefectuoso) || GameControllerEsterilizacion.Instance.modoEvaluativo)
            {
                mesaTrabajo.gameObject.GetComponent<MesaTrabajo>().AsignarHueco(this.gameObject);
            }
        }
        if (mesaTrabajo2 && Vector3.Distance(transform.position, mesaTrabajo2.position) < distanciaALaMesa && mesaTrabajo2.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
                mesaTrabajo2.gameObject.GetComponent<MesaTrabajo>().AsignarHueco(this.gameObject);
        }
        if (mesaClasificacion1 && Vector3.Distance(transform.position, mesaClasificacion1.position) < distanciaALaMesa && mesaClasificacion1.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaClasificacion1.gameObject.GetComponent<MesaTrabajo>().AsignarHueco(this.gameObject);
            herramientaColocada = true;
        }
        if (mesaClasificacion2 && Vector3.Distance(transform.position, mesaClasificacion2.position) < distanciaALaMesa && mesaClasificacion2.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaClasificacion2.gameObject.GetComponent<MesaTrabajo>().AsignarHueco(this.gameObject);
            herramientaColocada = true;
        }
        if (mesaClasificacion3 && Vector3.Distance(transform.position, mesaClasificacion3.position) < distanciaALaMesa && mesaClasificacion3.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaClasificacion3.gameObject.GetComponent<MesaTrabajo>().AsignarHueco(this.gameObject);
            herramientaColocada = true;
        }
        if (puedeLimpiar)
        {
            
            foreach (GameObject go in partesSucias)
            {
                go.GetComponent<Collider>().isTrigger = true;
            }
        }
    }

    public void BloquearFeedback()
    {

        interaccionGeneral = false;
    }

    public void InteraccionGeneral()
    {
        interaccionGeneral = true;

        //Mesa1
        if (mesaTrabajo && Vector3.Distance(transform.position, mesaTrabajo.position) < distanciaALaMesa && !feedbackBool && mesaActual == 1 && mesaTrabajo.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaTrabajo.gameObject.GetComponent<MesaTrabajo>().ActivarFeedBack(feedbackPrefab);
            feedbackBool = true;
        }
        else if (feedbackBool && mesaTrabajo && Vector3.Distance(transform.position, mesaTrabajo.position) > distanciaALaMesa && mesaActual == 1 && mesaTrabajo.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaTrabajo.gameObject.GetComponent<MesaTrabajo>().DestroyFeedback(this);
        }
        //Mesa2
        if (mesaTrabajo2 && Vector3.Distance(transform.position, mesaTrabajo2.position) < distanciaALaMesa && !feedbackBool && mesaActual == 2 && mesaTrabajo2.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaTrabajo2.gameObject.GetComponent<MesaTrabajo>().ActivarFeedBack(feedbackPrefab);
            feedbackBool = true;
        }
        else if (feedbackBool && mesaTrabajo2 && Vector3.Distance(transform.position, mesaTrabajo2.position) > distanciaALaMesa && mesaActual == 2 && mesaTrabajo2.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaTrabajo2.gameObject.GetComponent<MesaTrabajo>().DestroyFeedback(this);
        }

        //MesaClasificacion1
        if (mesaClasificacion1 && Vector3.Distance(transform.position, mesaClasificacion1.position) < distanciaALaMesa && !feedbackBool && mesaClasificacion1.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaClasificacion1.gameObject.GetComponent<MesaTrabajo>().ActivarFeedBack(feedbackPrefab);
            feedbackBool = true;
        }
        else if (feedbackBool && mesaClasificacion1 && Vector3.Distance(transform.position, mesaClasificacion1.position) > distanciaALaMesa && mesaClasificacion1.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaClasificacion1.gameObject.GetComponent<MesaTrabajo>().DestroyFeedback(this);
        }

        //MesaClasificacion2
        if (mesaClasificacion2 && Vector3.Distance(transform.position, mesaClasificacion2.position) < distanciaALaMesa && !feedbackBool && mesaClasificacion2.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaClasificacion2.gameObject.GetComponent<MesaTrabajo>().ActivarFeedBack(feedbackPrefab);
            feedbackBool = true;
        }
        else if (feedbackBool && mesaClasificacion2 && Vector3.Distance(transform.position, mesaClasificacion2.position) > distanciaALaMesa && mesaClasificacion2.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaClasificacion2.gameObject.GetComponent<MesaTrabajo>().DestroyFeedback(this);
        }

        //MesaClasificacion3
        if (mesaClasificacion3 && Vector3.Distance(transform.position, mesaClasificacion3.position) < distanciaALaMesa && !feedbackBool && mesaClasificacion3.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaClasificacion3.gameObject.GetComponent<MesaTrabajo>().ActivarFeedBack(feedbackPrefab);
            feedbackBool = true;
        }
        else if (feedbackBool && mesaClasificacion3 && Vector3.Distance(transform.position, mesaClasificacion3.position) > distanciaALaMesa && mesaClasificacion3.gameObject.GetComponent<MesaTrabajo>().huecosDisponibles)
        {
            mesaClasificacion3.gameObject.GetComponent<MesaTrabajo>().DestroyFeedback(this);
        }
    }


    public void ActivarSegundoAgarre()
    {
        if (puedeSegundoAgarre)
        {
            transform.SetParent(parentTransform);
            segundoAgarre.GetComponent<Collider>().enabled = true;
            segundoAgarre.GetComponent<XRGrabInteractable>().enabled = true;
        }
        if (puedeLimpiar)
        {
            GetComponent<Rigidbody>().useGravity = true;
            if(contenedorCepilloTubular){
                contenedorCepilloTubular.GetComponent<Collider>().enabled = true;
            }
            foreach (GameObject go in partesSucias)
            {
                go.GetComponent<Collider>().enabled = true;
                go.GetComponent<Collider>().isTrigger = false;
            }
        }
        
    }
    public void DesactivarSegundoAgarre()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        segundoAgarre.GetComponent<Collider>().enabled = false;
        segundoAgarre.GetComponent<XRGrabInteractable>().enabled = false;
    }

    public void ComprobarSuciedadActual()
    {
        partesSuciasRestantes--;
        GameControllerEsterilizacion.Instance.herramientaLimpia = true;
        GameControllerEsterilizacion.Instance.CheckConditions();
        if(partesSuciasRestantes == 0)
        {
            piezaLimpia = true;
            print("herramienta limpia");

        }
    }

    public void ComprobarEnjuagado()
    {
        partesEnjuagadasRestantes--;
        if(partesEnjuagadasRestantes == 0)
        {
            piezaEnjuagada = true;
            print("herramienta enujuagada");
        }
    }

    public void ComprobarSecado()
    {
        partesSecadasRestantes--;
        if (partesSecadasRestantes == 0)
        {
            piezaSecada = true;
            print("herramienta secada");
        }
    }
}
