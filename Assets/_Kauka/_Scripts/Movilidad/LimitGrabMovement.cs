using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.Events;

public class LimitGrabMovement : MonoBehaviour
{
    Vector3 posInicial;
    Vector3 posBase;
    bool selected;
    public bool limitParentRotation;
    public bool volverPosicion;
    public bool referenciaPosicionInicial;
    public Transform transformParent;
    Vector3 posInicialMando;
    Vector3 posCorrecta;
    public float movimientoPermitidoX;
    public float movimientoPermitidoY;
    public float movimientoPermitidoZ;
    public UnityEvent funcionCerca;
    public float distanciaMinima;
    // Start is called before the first frame update
    void Start()
    {
        transformParent = transform.parent;
        posBase = transform.localPosition;

        if (referenciaPosicionInicial)
        {
            posInicial = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            CogerObj();
        }
    }

    public void CogerObj()
    {
        transform.SetParent(transformParent);
        if (selected == false && !referenciaPosicionInicial)
        {

            posInicial = transform.localPosition;
        }
        //GetComponent<XRGrabInteractable>().trackPosition = true;

        //if (limitParentRotation && transformParent!=null)
        //{
        //    if (transformParent.gameObject.GetComponentInChildren<XRGrabInteractable>())
        //    {
        //        transformParent.gameObject.GetComponentInChildren<XRGrabInteractable>().trackRotation = false;
        //    }
        //    if (transformParent.gameObject.GetComponentInChildren<CustomMultiInteractable>())
        //    {
        //        transformParent.gameObject.GetComponentInChildren<CustomMultiInteractable>().trackRotation = false;
        //    }
        //}
        //else if(transformParent != null)
        //{
        //    if (transformParent.gameObject.GetComponentInChildren<XRGrabInteractable>())
        //    {
        //        transformParent.gameObject.GetComponentInChildren<XRGrabInteractable>().trackRotation = true;
        //    }
        //    if (transformParent.gameObject.GetComponentInChildren<CustomMultiInteractable>())
        //    {
        //        transformParent.gameObject.GetComponentInChildren<CustomMultiInteractable>().trackRotation = true;
        //    }
        //}
        


        if (selected == false)
        {
            posInicialMando = GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position;

        }

        selected = true;

        float diferenciaActualX = posInicialMando.x - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position.x;
        float diferenciaActualY = posInicialMando.y - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position.y;
        float diferenciaActualZ = posInicialMando.z - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position.z;

          

        transform.position = new Vector3(posInicial.x - diferenciaActualX, posInicial.y - diferenciaActualY, posInicial.z - diferenciaActualZ);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, posInicial.x - movimientoPermitidoX, posInicial.x + movimientoPermitidoX), Mathf.Clamp(transform.position.y, posInicial.y - movimientoPermitidoY, posInicial.y + movimientoPermitidoY), Mathf.Clamp(transform.position.z, posInicial.z - movimientoPermitidoZ, posInicial.z + movimientoPermitidoZ));

        posCorrecta = transform.position;
        
    }
    public void SoltarObjeto()
    {
        selected = false;
        
        if (volverPosicion)
        {
            transform.localPosition = posBase;
        }
        if (Vector3.Distance(transform.position, posInicial + new Vector3(movimientoPermitidoX, movimientoPermitidoY, movimientoPermitidoZ)) < distanciaMinima)
        {
            funcionCerca.Invoke();
        }

        //StartCoroutine(CorreccionPos());
        CorreccionPos2();
        

    }
    void CorreccionPos2()
    {
        Vector3 result = posCorrecta;
        if (movimientoPermitidoX == 0) { result = new Vector3(posInicial.x, result.y, result.z); }
        if (movimientoPermitidoY == 0) { result = new Vector3(result.x, posInicial.y, result.z); };
        if (movimientoPermitidoZ == 0) { result = new Vector3(result.x, result.y, posInicial.z); };


        transform.position = result;

    }
}
