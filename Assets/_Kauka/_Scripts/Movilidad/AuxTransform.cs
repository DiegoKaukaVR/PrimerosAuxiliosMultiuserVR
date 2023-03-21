using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class AuxTransform : MonoBehaviour
{
    public Transform dependencia;
    public float distanciaMinima;
    public float cantidadDesplazamiento;

    public Collider limiteDesplazamiento;

    public bool desplazado;
    Vector3 posInit;
    bool seleccionado;
    private void Start()
    {
        posInit = transform.position;
        SetPosition(posInit);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(transform.position, dependencia.position) < distanciaMinima && desplazado==false)
        //{
        //    Vector3 direccionMov = referencia.position - transform.position;
        //    transform.position = direccionMov * cantidadDesplazamiento;
        //    desplazado = true;
        //}
        //if (Vector3.Distance(transform.position, dependencia.position) > distanciaMinima && desplazado == true)
        //{
        //    desplazado = false;
        //}

        //if (!limiteDesplazamiento.bounds.Contains(dependencia.position) && desplazado == false)
        //{
        //    //transform.position = new Vector3(dependencia.position.x, dependencia.position.y, transform.position.z);
        //    //transform.position = new Vector3(transform.position.x, transform.position.y + cantidadDesplazamiento, transform.position.z);
        //    transform.position = posInit + dependencia.transform.forward * cantidadDesplazamiento; desplazado = true;
        //}

        //if (limiteDesplazamiento.bounds.Contains(dependencia.position) && desplazado == true)
        //{
        //    transform.position = new Vector3(transform.position.x , transform.position.y - cantidadDesplazamiento, transform.position.z );
        //    desplazado = false;
        //}
        if (seleccionado == true)
        {
            SetPosition(GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position);
        }
    }

    public void SetPosition(Vector3 pos)
    {
        
        if (!limiteDesplazamiento.bounds.Contains(dependencia.position))
        {
            //transform.position = new Vector3(dependencia.position.x, dependencia.position.y, transform.position.z);
            //transform.position = new Vector3(transform.position.x, transform.position.y + cantidadDesplazamiento, transform.position.z);
            transform.position = pos + dependencia.transform.forward * cantidadDesplazamiento;
            posInit = transform.position;
        }
    }
    public void Seleccion()
    {
        seleccionado = true;
    }
    public void Soltar()
    {
        SetPosition(posInit);
        seleccionado = false;
    }
}
