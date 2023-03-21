using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public GameObject objetoObjetivo;
    public string tagObjetivo;
    public float distanciaActivacion;
    bool activado;

    public bool conAccion;
    [SerializeField]
    UnityEvent accionARealizar;


    private void Start()
    {
        activado = false;
        if (objetoObjetivo)
        {
            objetoObjetivo.SetActive(false);
        }
    }

    private void Update()
    {
        //if (activado)
        //{
        //    if (Vector3.Distance(transform.position, player.transform.position) < distanciaActivacion && GetComponent<Collider>().enabled)
        //    {
        //        funcionObjetivo.SetActive(true);
        //    }
        //    else
        //    {
        //        funcionObjetivo.SetActive(false);
        //    }
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == tagObjetivo || other.gameObject.layer == 8)
        {
            if (objetoObjetivo)
            {
                objetoObjetivo.SetActive(true);
            }
            if (conAccion)
            {
                accionARealizar.Invoke();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == tagObjetivo || other.gameObject.layer == 8)
        {
            if (objetoObjetivo && !conAccion)
            {
                objetoObjetivo.SetActive(false);
            }
        }
    }

    public void ActivarUpdate()
    {
        //if (!activado)
        //{
        //    activado = true;
        //}
        //else
        //{
        //    activado = false;
        //}
    }
}
