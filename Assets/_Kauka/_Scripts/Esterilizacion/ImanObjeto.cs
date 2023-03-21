using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.Events;

public class ImanObjeto : MonoBehaviour
{
    public Transform posIman;
    public Transform transformInicial;
    Vector3 posInicial;
    Quaternion rotInicial;
    public bool dosPosicionesDisponibles;
    public bool conOffset;
    public float offset;
    public bool volverRotacion;
    public bool rotacionDeDestino;
    public float distanciaIman;
    public float tiempoVolver;
    public bool destruir;
    public bool desactivarEnObjetivo;
    public bool volviendoAPosicion;
    public bool convertirKinematico;

    public UnityEvent funcionAlTerminar;

    // Start is called before the first frame update
    void Start()
    {

        if (posIman != null && !dosPosicionesDisponibles)
        {
            posInicial = posIman.position;
        }
        else
        {
            posInicial = transform.position;
        }
        rotInicial = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ForzarVolver()
    {
        StartCoroutine(VolverPosicionInicial(posIman.position));
    }
    public void CheckPosition()
    {
        if(((!dosPosicionesDisponibles && posIman != null) && Vector3.Distance(transform.position, posIman.position) < distanciaIman) || (dosPosicionesDisponibles && Vector3.Distance(transform.position, posIman.position) < distanciaIman))
        {
            if (conOffset)
            {
                StartCoroutine(IrAPosicionConOffset(posIman.position));
                volviendoAPosicion = true;
            }
            else
            {
                StartCoroutine(VolverPosicionInicial(posIman.position));
                volviendoAPosicion = true;
            }
        }
        else if (((!dosPosicionesDisponibles && posIman == null) && Vector3.Distance(transform.position, posInicial) < distanciaIman) || (dosPosicionesDisponibles && Vector3.Distance(transform.position, posInicial) < distanciaIman))
        {
            if (conOffset)
            {
                StartCoroutine(IrAPosicionConOffset(posInicial));
                volviendoAPosicion = true;
            }
            else
            {
                StartCoroutine(VolverPosicionInicial(posInicial));
                volviendoAPosicion = true;
            }
        }
        else
        {
            volviendoAPosicion = false;
        }
    }
    IEnumerator VolverPosicionInicial(Vector3 posicionObj)
    {
        float tiempo = 0;
        
        while (tiempo < tiempoVolver)
        {
            tiempo += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, posicionObj, tiempo / tiempoVolver);
            if (volverRotacion)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotInicial, tiempo / tiempoVolver);
            }
            else if (rotacionDeDestino)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, posIman.transform.rotation, tiempo / tiempoVolver);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(rotInicial.eulerAngles.x,transform.eulerAngles.y,rotInicial.eulerAngles.z,1), tiempo / tiempoVolver);
            }
            yield return null;
        }
        funcionAlTerminar.Invoke();
        if(posIman != null && desactivarEnObjetivo)
        {

            //GetComponent<Collider>().enabled = false;
            //if (GetComponent<ValidarModulo1>())
            //{
            //    GetComponent<ValidarModulo1>().FuncionObjetivo();
            //}
        }
        if (destruir)
        {
            Destroy(this.gameObject);
        }
        if (convertirKinematico)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
        }
    }

    IEnumerator IrAPosicionConOffset(Vector3 posicionObj)
    {
        float tiempo = 0;

        while (tiempo < tiempoVolver)
        {
            tiempo += Time.deltaTime;
            transform.position = Vector3.Lerp(new Vector3(posicionObj.x, posicionObj.y + offset, posicionObj.z), posicionObj, tiempo / tiempoVolver);
            if (volverRotacion)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotInicial, tiempo / tiempoVolver);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(rotInicial.eulerAngles.x, transform.eulerAngles.y, rotInicial.eulerAngles.z, 1), tiempo / tiempoVolver);
            }
            yield return null;
        }
        funcionAlTerminar.Invoke();
        if (desactivarEnObjetivo)
        {
            GetComponent<XRGrabInteractable>().enabled = false;
        }
    }
    public void AsignarObjetivo(Transform transObjetivo)
    {
        posIman = transObjetivo;
    }
}
