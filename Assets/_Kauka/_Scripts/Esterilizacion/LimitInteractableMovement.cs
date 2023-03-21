using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class LimitInteractableMovement : MonoBehaviour
{
    public bool limitX;
    public bool limitY;
    public bool limitZ;
    public float movimientoPermitido= 1;
    Vector3 posInicial;
    Vector3 posBase;
    bool selected;
    public bool limitParentRotation;
    public bool volverPosicion;
    public bool referenciaPosicionInicial;
    public Transform transformParent;


    public float movimientoPermitidoX;
    public float movimientoPermitidoY;
    public float movimientoPermitidoZ;
    public bool conValores;

    // Start is called before the first frame update
    void Start()
    {
        transformParent = transform.parent;
        posBase = transform.localPosition;

        if (referenciaPosicionInicial)
        {
            posInicial = transform.localPosition;
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
        GetComponent<XRGrabInteractable>().trackPosition = true;
        selected = true;
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
        if (!conValores)
        {

            if (limitY && !limitX && !limitZ)
            {
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, posInicial.x - movimientoPermitido, posInicial.x + movimientoPermitido), posInicial.y, Mathf.Clamp(transform.localPosition.z, posInicial.z - movimientoPermitido, posInicial.z + movimientoPermitido));
                if (GetComponent<MedidoresJabon>())
                {
                    GetComponent<MedidoresJabon>().ComprobarLimites(posInicial, movimientoPermitido);
                }
            }
            else if (limitX && !limitY && !limitZ)
            {
                transform.position = new Vector3(posInicial.x, Mathf.Clamp(transform.position.y, posInicial.y - movimientoPermitido, posInicial.y + movimientoPermitido), Mathf.Clamp(transform.position.z, posInicial.z - movimientoPermitido, posInicial.z + movimientoPermitido));
            }
            else if (limitZ && !limitX && !limitY)
            {

                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, posInicial.x - movimientoPermitido, posInicial.x + movimientoPermitido), Mathf.Clamp(transform.localPosition.y, posInicial.y - movimientoPermitido, posInicial.y + movimientoPermitido), posInicial.z);
            }
            else if (limitX && limitY && !limitZ)
            {
                transform.position = new Vector3(posInicial.x, posInicial.y, Mathf.Clamp(transform.localPosition.z, posInicial.z - movimientoPermitido, posInicial.z + movimientoPermitido));
            }
            else if (limitZ && limitY && !limitX)
            {

                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, posInicial.x - movimientoPermitido, posInicial.x + movimientoPermitido), posInicial.y, posInicial.z);
                
            }
        }
        else
        {
            
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, posInicial.x - movimientoPermitidoX, posInicial.x + movimientoPermitidoX),
            Mathf.Clamp(transform.localPosition.y, posInicial.y - movimientoPermitidoY, posInicial.y + movimientoPermitidoY),
            Mathf.Clamp(transform.localPosition.z, posInicial.z - movimientoPermitidoY, posInicial.z + movimientoPermitidoZ));
        }
    }
    public void SoltarObjeto()
    {
        selected = false;
        GetComponent<XRGrabInteractable>().trackPosition = false;
        //if (limitParentRotation && transformParent!=null)
        //{
        //    transformParent.gameObject.GetComponentInChildren<XRGrabInteractable>().trackRotation = true;
        //}
        //if (limitZ)
        //{
        //    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, posInicial.z);
        //}
        //if (limitZ)
        //{
        //    transform.localPosition = new Vector3(transform.localPosition.x, posInicial.y, transform.localPosition.y);
        //}
        if (volverPosicion)
        {
            transform.localPosition = posBase;
        }
    }
}
